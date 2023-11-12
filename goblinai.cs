using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinai : MonoBehaviour {
   
    public Animator hammeranimator;
    public Animator goblinanimator;
    public GameObject detectedobject;
    public GameObject heroobject;
    public CameraShake camerashake;
    public bool hasdetected=false;
    public float speed = 3f;
    public float cooldowntime = 1f;
    public int goblinHealth = 100;
    public GameObject crossair;
    bool cooldown = false;
    public GameObject timemanager;
    public GameObject tube;
    bool benhedefim = false;
    public float renklitime = 4f;
    public bool renklendi = false;
    public float scale = 1.6f;
    bool olumsesi = false;
   
  
     



    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        detectedobject = heroobject;// default dusman hero
        
    }

	
	// Update is called once per frame
	void Update () {
        if(goblinHealth <= 0)
        {
            GoblinDie();
        }
        if (detectedobject.CompareTag("yesil"))//yesil birini gordu
        {
            if (!renklendi)// o an goblin yesilse hero dusman deil
            {
                hasdetected = false;
                goblinanimator.SetBool("isrunning", false);
            }
            else//o an yesil deil
            {
                hasdetected = true;
            }
        }
        if (detectedobject.CompareTag("dead"))//hero oldu
        {
            hasdetected = false;
            goblinanimator.SetBool("isrunning", false);
        }
        if (hasdetected)
        {
            if(detectedobject.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-scale, scale, 1);
            }
            else
                transform.localScale = new Vector3(scale, scale, 1);


            if (Vector3.Distance(transform.position, detectedobject.transform.position) > 2.4f)//move if distance from target is greater than 1
            {

                transform.position = Vector2.MoveTowards(transform.position, detectedobject.transform.position, speed * Time.deltaTime);
                goblinanimator.SetBool("isrunning", true);
            }
            
             else if (cooldown == false)
            {
                goblinanimator.SetBool("isrunning",false);
                GoblinAttack(); //saldir
                Invoke("GoblinCooldown", cooldowntime);//cooldownu 4f sonra baslat
                cooldown = true;
            }
            

        }
        if (benhedefim)
        {
            tube.GetComponent<tubescript>().hedef = this.transform;
        }
	
	}
    public void Detect(GameObject gorulen)
    {
        hasdetected = true;
        detectedobject = gorulen;
    }
    public void GoblinAttack()
    {
        soundmanager.PlaySound("woosh");
        hammeranimator.SetTrigger("hammerhit");
    }
    void GoblinCooldown()
    {
        cooldown = false;
    }
    public void GoblinTakeDamage(int damage)
    {
        soundmanager.PlaySound("goblinhit");
        goblinanimator.Play("goblin_hurt");
        goblinHealth = goblinHealth - damage;
        StartCoroutine(camerashake.Shake(.07f, .4f));
    }
    void GoblinDie()
    {
        if (!olumsesi)
        {
            soundmanager.PlaySound("goblindie");
            olumsesi = true;
        }
        goblinanimator.Play("goblin_die");
        hasdetected = false;
        gameObject.tag = "dead";
        //Physics2D.IgnoreLayerCollision(9,9);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        
    }
    private void OnMouseOver()
    {
        if(timemanager.GetComponent<bullet_time>().agircekim == true)
        {
            if (heroobject.GetComponent<PlayerMovement>().tupatabilir)
            {
                crossair.GetComponent<SpriteRenderer>().enabled = true;//tupatilabilirse crossair goster
            }
            benhedefim = true;
            if (Input.GetMouseButtonDown(1))
            {
                heroobject.GetComponent<PlayerMovement>().Tupat(1);
                heroobject.GetComponent<PlayerMovement>().tupatabilir = false;
                heroobject.GetComponent<PlayerMovement>().Invoke("tupcooldown", 4f);
            }
        }
        
    }
    private void OnMouseExit()
    {
        crossair.GetComponent<SpriteRenderer>().enabled = false;
        benhedefim = false;
    }
    private void OnTriggerEnter2D(Collider2D vurulan)
    {
        if (vurulan.tag == "tup")//tuple vuruldu
        {
            soundmanager.PlaySound("tubebreak");
            Destroy(vurulan.gameObject);
            renklendi = true;
            GetComponent<SpriteRenderer>().color = Color.red;
            gameObject.tag = "kirmizi";//artik kirmizi oldu
            Invoke("GoblinTurnNormal", renklitime);// 4f sonra normal renge don
        }
    }

    void GoblinTurnNormal()
    {
        renklendi = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.tag = "yesil";
    }
    private void OnTriggerStay2D(Collider2D vurulan)
    {
        if (vurulan.tag == "yesil")//baska goblinleri gordu
        {
            if (renklendi)//suan renkli
            {
                GameObject tempobject = vurulan.gameObject;
                Detect(tempobject);// yeni hedefi arkadaslari
            }

        }
    }
    void OnBecameInvisible()
    {
        if(goblinHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }


}
