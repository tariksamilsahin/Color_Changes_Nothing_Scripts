using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    public AudioSource soundman;
    public Animator heroanimator;
    public GameObject sword;
    public Character_Controller_2D controller;
    public CameraShake camerashake;
    public GameObject kirmizitube;
    public GameObject mavitube;
    public bool tupatabilir = true;
    private GameMaster gm;

    float horizontalMove = 0f;
    public float runspeed = 38f;
    public float cooldowntime = 0.4f;
    public float colortime = 3f;
    bool jump = false;
    Animator swordanimator;
    bool cooldown = false;
    public int HeroHealth = 100;
    

    void Awake()
    {
        swordanimator = sword.GetComponent<Animator>();
    }
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
    }
    // Update is called once per frame
    void Update () {
       horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;
        heroanimator.SetFloat("speed", Mathf.Abs(horizontalMove));//kosu animasyonu

       if (Input.GetButtonDown("Jump"))
       {
            soundmanager.PlaySound("herojump");
           jump = true;
            heroanimator.SetBool("isjumping", true);
       }
        if (Input.GetMouseButtonDown(0))// mouse tiklandi
        {
            
             if (cooldown == false)
            {
                Attack();//saldir
                Invoke("ResetCooldown", cooldowntime);//cooldownu 0.5f sonra baslat
                cooldown = true;
            }
        }
        if(HeroHealth <= 0)
        {
            HeroDie();
        }
        
    }
    public void OnLand()
    {
        heroanimator.SetBool("isjumping", false);
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
    void ResetCooldown()
    {
        cooldown = false;
    }
    void Attack()
    {
        soundmanager.PlaySound("sword");
        swordanimator.SetTrigger("swordhit");//kilic animasyonu
    }
    public void TurnBlue()
    {
       
        heroanimator.SetBool("isblue", true);
        Invoke("TurnNormal", colortime);
        heroanimator.Play("mHero_toblue");
        gameObject.tag = "mavi";
    }
    public void TurnGreen()
    {

        heroanimator.SetBool("isgreen", true);
        Invoke("TurnNormal", colortime);
        heroanimator.Play("mHero_togreen");
        gameObject.tag = "yesil";
    }
    public void TurnRed()
    {

        heroanimator.SetBool("isred", true);
        Invoke("TurnNormal", colortime);
        heroanimator.Play("mHero_tored");
        gameObject.tag = "kirmizi";
    }
    void TurnNormal()
    {
        
        heroanimator.SetBool("isblue", false);
        heroanimator.SetBool("isgreen", false);
        heroanimator.SetBool("isred", false);
        gameObject.tag = "beyaz";

    }
    public void HeroTakeDamage(int damage)
    {
        soundmanager.PlaySound("herohit");
        HeroHealth = HeroHealth - damage;
        StartCoroutine(camerashake.Shake(.15f, .5f));
       
       if(!heroanimator.GetBool("isblue") & !heroanimator.GetBool("isred")& !heroanimator.GetBool("isgreen"))//duz
        {
            heroanimator.Play("mHero_hurt");
        }
        if (heroanimator.GetBool("isblue") & !heroanimator.GetBool("isred") & !heroanimator.GetBool("isgreen"))//mavi
        {
            heroanimator.Play("mHero_blue_hurt");
        }
        if (!heroanimator.GetBool("isblue") & heroanimator.GetBool("isred") & !heroanimator.GetBool("isgreen"))//kirmizi
        {
            heroanimator.Play("mHero_red_hurt");
        }
        if (!heroanimator.GetBool("isblue") & !heroanimator.GetBool("isred") & heroanimator.GetBool("isgreen"))//yesil
        {
            heroanimator.Play("mHero_green_hurt");
        }

    }
    void HeroDie()
    {
        if (!soundman.isPlaying)
        {
            soundmanager.PlaySound("herodie");
        }

        
        if (!heroanimator.GetBool("isblue") & !heroanimator.GetBool("isred") & !heroanimator.GetBool("isgreen"))//duz
        {
            heroanimator.Play("mHero_die");
        }
        if (heroanimator.GetBool("isblue") & !heroanimator.GetBool("isred") & !heroanimator.GetBool("isgreen"))//mavi
        {
            heroanimator.Play("mHero_blue_die");
        }
        if (!heroanimator.GetBool("isblue") & heroanimator.GetBool("isred") & !heroanimator.GetBool("isgreen"))//kirmizi
        {
            heroanimator.Play("mHero_red_die");
        }
        if (!heroanimator.GetBool("isblue") & !heroanimator.GetBool("isred") & heroanimator.GetBool("isgreen"))//yesil
        {
            heroanimator.Play("mHero_green_die");
        }
        gameObject.tag = "dead";
        Invoke("RestartLevel", 1f);
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void Tupat(int dusman)
    {
      if (tupatabilir)
        {
            soundmanager.PlaySound("tubethrow");
            if (dusman == 1)
            {
                Instantiate(kirmizitube, transform.position, transform.rotation);
            }
            else if (dusman == 2)
            {
                Instantiate(mavitube, transform.position, transform.rotation);
            }


        }

    }
    public void tupcooldown()
    {
        tupatabilir = true;
    }
}
