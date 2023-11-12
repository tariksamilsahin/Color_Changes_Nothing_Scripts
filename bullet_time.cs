
using UnityEngine;

public class bullet_time : MonoBehaviour {
    
    public GameObject hero;
    public Transform target;
    public GameObject palet;
    public GameObject pause;
    Camera cam;
    Animator heroanimator;
    public bool agircekim = false;
    bool slowmooynadi = false;
    void Start()
    {
        cam = Camera.main;
        heroanimator = hero.GetComponent<Animator>();
    }
    void Update()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(target.position);//screenPos targetin yeri
        palet.transform.position = screenPos;
        if (Input.GetKey(KeyCode.Q))
        {
            if (!heroanimator.GetBool("isblue") && !heroanimator.GetBool("isgreen") && !heroanimator.GetBool("isred"))
            {
                if (!slowmooynadi)
                {
                    soundmanager.PlaySound("slowmo");
                    slowmooynadi = true;
                }
                
                Time.timeScale = 0.3f;
                OpenPalet();
                agircekim = true;
            }

        }
        else
        {
            Time.timeScale = 1;
            agircekim = false;
            slowmooynadi = false;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            slowmooynadi = false;
            agircekim = false;
            ClosePalet();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            OpenPause();
        }
       
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ClosePause();
        }

    }
    void OpenPalet()
    {
        if (!heroanimator.GetBool("isblue") && !heroanimator.GetBool("isgreen") && !heroanimator.GetBool("isred"))
        {
            if (palet != null)
            {
                palet.SetActive(true);
            }
        }
    }
    void ClosePalet()
    {
      
        palet.SetActive(false);
       
    }
    void OpenPause()
    {
        if (pause != null)
        {
            pause.SetActive(true);
        }
    }
    void ClosePause()
    {
        if (pause != null)
        {
            pause.SetActive(false);
        }
    }


}

