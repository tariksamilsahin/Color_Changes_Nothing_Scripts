using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangedetector : MonoBehaviour {
    public bool degdi = false;
    public GameObject goblin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   
    private void OnTriggerStay2D(Collider2D vurulan)
    {
        if (vurulan.tag == "beyaz" || vurulan.tag == "mavi" || vurulan.tag == "kirmizi")
        {
            degdi = true;
            GameObject kurban = vurulan.gameObject;
            goblin.GetComponent<goblinai>().Detect(kurban);

        }
    }
}
