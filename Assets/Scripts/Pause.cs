using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool menuIsOpen = false;
    bool screenshotMenuIsOpen = false;
    HighResScreenshot mainCam;
    public List<string> randomTip = new List<string>();
    Text tip;
    // Start is called before the first frame update
    void Start()
    {
        tip = GameObject.Find("tip").GetComponent<Text>();
        pauseMenu.SetActive(false);
       // mainCam = GameObject.Find("MainCamera").GetComponent<HighResScreenshot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuIsOpen == false)
            {
                OpenMenu();
              //  Debug.Log("open");
                Time.timeScale = 0;
            }
            else
            {
              //  Debug.Log("close");
                StartCoroutine(CloseMenu());
                Time.timeScale = 1;
            }
            //Debug.Log("qq");
        }
        //if (menuIsOpen == true)
        //{


        //    if (Input.GetKeyDown(KeyCode.K))
        //    {
        //        if (screenshotMenuIsOpen == false)
        //        {
        //            screenshotMenuIsOpen = true;
        //            StartCoroutine(Screenshot());

        //        }
        //    }
        //}
    }
    IEnumerator CloseMenu()
    {
       // Debug.Log("1");
        menuIsOpen = false;
        pauseMenu.GetComponent<Animator>().SetTrigger("fade");
        yield return new WaitForSecondsRealtime(0.1f);
        pauseMenu.SetActive(false);
      //  Debug.Log("2");
    }
    void OpenMenu()
    {
        tip.text = randomTip[Random.Range(0,randomTip.Count)];
        pauseMenu.SetActive(true);
        menuIsOpen = true;
    }
    //void MakeScreenshot()
    //{

    //}
    //IEnumerator Screenshot()
    //{
    //    CloseMenu();
    //    yield return new WaitForSecondsRealtime(1f);
    //    // mainCam.takeHiResShot = true;
    //    yield return new WaitForSecondsRealtime(1f);
    //    OpenMenu();
    //    screenshotMenuIsOpen = false;
    //}
}
