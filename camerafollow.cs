using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour {
    private Vector3 offset = new Vector3(0f, 0f, -10);
    public float smoothtime = 1f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetposition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, smoothtime);
    }
}
