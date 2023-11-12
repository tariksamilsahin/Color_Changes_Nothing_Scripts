using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ironhammerscript : MonoBehaviour {

    BoxCollider2D col;
    public GameObject hero;
    public GameObject goblin;
   
    
    int goblinhasar = 30 ;
    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;//default olarak kapali
        
    }
    // Update is called once per frame
    void Update () {
		
	}
    void HammerHitAcik()
    {
        col.enabled = true;
    }
    void HammerHitKapali()
    {
        col.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D vurulan)
    {
        if (goblin.GetComponent<goblinai>().renklendi)//goblin renkliyse yesile de vuracak
        {
            if (vurulan.tag == "mavi" || vurulan.tag == "yesil")
            {


                if (vurulan.gameObject.layer == 8)
                {
                    hero.GetComponent<PlayerMovement>().HeroTakeDamage(goblinhasar);

                    col.enabled = false;

                }
                else
                {
                    soundmanager.PlaySound("hammer");
                    vurulan.gameObject.GetComponent<goblinai>().GoblinTakeDamage(goblinhasar/2);// gobline vuruyor
                }

            }
        }
        else if (vurulan.tag == "beyaz" || vurulan.tag == "mavi" || vurulan.tag == "kirmizi")
        {
           
            
            if (vurulan.gameObject.layer== 8)
            {
                hero.GetComponent<PlayerMovement>().HeroTakeDamage(goblinhasar);

                col.enabled = false;

            }
            else
            {
                soundmanager.PlaySound("hammer");
                vurulan.gameObject.GetComponent<goblinai>().GoblinTakeDamage(goblinhasar/2);// kirmiziya vuruyor
            }

        }
    }
    

}
