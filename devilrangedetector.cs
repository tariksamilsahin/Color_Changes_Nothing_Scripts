using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class devilrangedetector : MonoBehaviour {

    public bool degdi = false;
    public GameObject devil;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D vurulan)
    {
        if (vurulan.tag == "beyaz" || vurulan.tag == "mavi" || vurulan.tag == "yesil")
        {
            degdi = true;
            GameObject kurban = vurulan.gameObject;
            devil.GetComponent<devilai>().Detect(kurban);

        }
    }
}
