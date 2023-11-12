using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bolumikimanager : MonoBehaviour {
    public GameObject dusman1;
    public GameObject dusman2;
    public GameObject dusman3;
    public GameObject dusman4;
    //private GameMaster gm;
    // Use this for initialization
    void Start()
    {
      //  gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dusman1 == null && dusman2 == null && dusman3 == null && dusman4 == null)
        {
            //(-7.27f, -3.31f);
            //gm.lastCheckPointPos.Set(-7.27f, -3.31f);
            Debug.Log("hepsi oldu");
            MenuyuAc();
        }
    }
    void MenuyuAc()
    {
        SceneManager.LoadScene(0);

    }
}
