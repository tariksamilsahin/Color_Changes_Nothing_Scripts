using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bolumscript : MonoBehaviour {
    public GameObject dusman1;
    public GameObject dusman2;
    public GameObject dusman3;
    public GameObject dusman4;
    public GameObject dusman5;
    private GameMaster gm;
    AudioSource[] audiolar = new AudioSource[3];
    // Use this for initialization
    void Start () {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        audiolar = GameObject.FindGameObjectWithTag("GM").GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if (dusman1 == null && dusman2 == null && dusman3 == null && dusman4 == null && dusman5 == null)
        {
            //(-7.27f, -3.31f);
            gm.lastCheckPointPos.Set(-7.27f, -3.31f);
            LevelikiAc();
        }
	}
    void LevelikiAc()
    {
        audiolar[0].enabled = false;
        audiolar[1].enabled = true;
        SceneManager.LoadScene(2);
        
    }
}
