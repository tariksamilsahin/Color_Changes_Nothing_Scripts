using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ironswordscript : MonoBehaviour {
    BoxCollider2D col;
    

    int herohasar = 20;
    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;//default olarak kapali

    }
   
	
	// Update is called once per frame
	void Update () {
		
	}
    void SwordHitAcik()
    {
        col.enabled = true;
    }
    void SwordHitKapali()
    {
        col.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D vurulan)
    {
        if (vurulan.tag == "yesil" || vurulan.tag == "mavi" || vurulan.tag == "kirmizi")
        {
            var goblinmi = vurulan.GetComponent<goblinai>();
            if (goblinmi != null)// //goblinai a sahip mi evetse bu bir goblin
            {
                vurulan.GetComponent<goblinai>().GoblinTakeDamage(herohasar);
            }
            var devilmi = vurulan.GetComponent<devilai>();
            if (devilmi != null)// //devilai a sahip mi evetse bu bir goblin
            {
                vurulan.GetComponent<devilai>().DevilTakeDamage(herohasar);
            }

            col.enabled = false;

        }
    }

}
