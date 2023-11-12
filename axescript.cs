using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axescript : MonoBehaviour {
    BoxCollider2D col;
    public GameObject hero;
    public GameObject devil;


    int devilhasar = 40;
    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;//default olarak kapali

    }
    // Update is called once per frame
    void Update()
    {

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
        if (devil.GetComponent<devilai>().renklendi)//devil renkliyse kirmiziya de vuracak
        {
            if (vurulan.tag == "kirmizi" || vurulan.tag == "yesil")
            {


                if (vurulan.gameObject.layer == 8)
                {
                    hero.GetComponent<PlayerMovement>().HeroTakeDamage(devilhasar);

                    col.enabled = false;

                }
                else
                {
                    soundmanager.PlaySound("hammer");
                    vurulan.gameObject.GetComponent<devilai>().DevilTakeDamage(devilhasar / 2);// devile vuruyor
                }

            }
        }
        else if (vurulan.tag == "beyaz" || vurulan.tag == "mavi" || vurulan.tag == "yesil")
        {


            if (vurulan.gameObject.layer == 8)
            {
                hero.GetComponent<PlayerMovement>().HeroTakeDamage(devilhasar);

                col.enabled = false;

            }
            else
            {
                soundmanager.PlaySound("hammer");
                vurulan.gameObject.GetComponent<devilai>().DevilTakeDamage(devilhasar / 2);// maviye vuruyor
            }

        }
    }


}
