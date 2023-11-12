using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tubescript : MonoBehaviour {
    public float speed=30f;
    public Transform hedef;
    // Use this for initialization

   
    void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.position = Vector2.MoveTowards(transform.position, hedef.position, speed * Time.deltaTime);
        
        
    }
    
   
}
