using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class mainmenu : MonoBehaviour {
    public GameObject intro;
    public GameObject videoplayer;

	public void Loadfirstlevel()
    {
        SceneManager.LoadScene(1);
    }
    public void Loadsecondlevel()
    {
        SceneManager.LoadScene(2);
    }
    public void Quitgame()
    {
        Application.Quit();
    }
    public void PlayIntro()
    {
        intro.SetActive(true);
        videoplayer.GetComponent<VideoPlayer>().enabled = true;
        Invoke("StopIntro", 55f);
    }
    void StopIntro()
    {
        videoplayer.GetComponent<VideoPlayer>().enabled = false;
        intro.SetActive(false);
    }
   
}
