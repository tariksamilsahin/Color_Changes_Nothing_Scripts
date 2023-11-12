using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class devilai : MonoBehaviour {
   
    public Animator axeanimator;
    public Animator devilanimator;
    public GameObject detectedobject;
    public GameObject heroobject;
    public CameraShake camerashake;
    public bool hasdetected = false;
    public float speed = 3f;
    public float cooldowntime = 1f;
    public int devilHealth = 150;
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
    void Start()
    {
        detectedobject = heroobject;// default dusman hero
    }

    // Update is called once per frame
    void Update()
    {
        if (devilHealth <= 0)
        {
            DevilDie();
        }
        if (detectedobject.CompareTag("kirmizi"))//kirmizi birini gordu
        {
            if (!renklendi)// o an devil kirmiziysa hero dusman deil
            {
                hasdetected = false;
                devilanimator.SetBool("isrunning", false);
            }
            else//o an kirmizi deil
            {
                hasdetected = true;
            }
        }
        if (detectedobject.CompareTag("dead"))//hero oldu
        {
            hasdetected = false;
            devilanimator.SetBool("isrunning", false);
        }
        if (hasdetected)
        {
            if (detectedobject.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-scale, scale, 1);
            }
            else
                transform.localScale = new Vector3(scale, scale, 1);


            if (Vector3.Distance(transform.position, detectedobject.transform.position) > 2.4f)//move if distance from target is greater than 1
            {

                transform.position = Vector2.MoveTowards(transform.position, detectedobject.transform.position, speed * Time.deltaTime);
                devilanimator.SetBool("isrunning", true);
            }

            else if (cooldown == false)
            {
                devilanimator.SetBool("isrunning", false);
                DevilAttack(); //saldir
                Invoke("DevilCooldown", cooldowntime);//cooldownu 4f sonra baslat
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
    public void DevilAttack()
    {
        soundmanager.PlaySound("woosh");
        axeanimator.SetTrigger("hammerhit");
    }
    void DevilCooldown()
    {
        cooldown = false;
    }
    public void DevilTakeDamage(int damage)
    {
        soundmanager.PlaySound("devilhit");
        devilanimator.Play("devil_hurt");
        devilHealth = devilHealth - damage;
        StartCoroutine(camerashake.Shake(.07f, .4f));
    }
    void DevilDie()
    {
        if (!olumsesi)
        {
            soundmanager.PlaySound("devildie");
            olumsesi = true;
        }
        devilanimator.Play("devil_die");
        hasdetected = false;
        gameObject.tag = "dead";
        //Physics2D.IgnoreLayerCollision(9,9);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }
    private void OnMouseOver()
    {
        if (timemanager.GetComponent<bullet_time>().agircekim == true)
        {
            if (heroobject.GetComponent<PlayerMovement>().tupatabilir)
            {
              crossair.GetComponent<SpriteRenderer>().enabled = true;//tupatilabilirse crossair goster
             }
            benhedefim = true;
            if (Input.GetMouseButtonDown(1))
            {
                heroobject.GetComponent<PlayerMovement>().Tupat(2);
                heroobject.GetComponent<PlayerMovement>().tupatabilir = false; //tupatilamaz
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
            GetComponent<SpriteRenderer>().color = new Color(0.1647059f, 0.2196078f, 1f, 1f);
            gameObject.tag = "mavi";//artik mavi oldu
            Invoke("DevilTurnNormal", renklitime);// 4f sonra normal renge don
        }
    }

    void DevilTurnNormal()
    {
        renklendi = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.tag = "kirmizi";
    }
    private void OnTriggerStay2D(Collider2D vurulan)
    {
        if (vurulan.tag == "kirmizi")//baska goblinleri gordu
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
        if (devilHealth <= 0)
        {
            Destroy(this.gameObject);
        }

    }

}
