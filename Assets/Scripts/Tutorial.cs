using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    //[SerializeField] GameObject screen1;
    //[SerializeField] GameObject screen2;
    //[SerializeField] GameObject screen3;
    //[SerializeField] GameObject screen4;

   public List<GameObject> screens = new List<GameObject>();
   public List<int> waitForSecond = new List<int>();

    bool screenIsActive = false;
    int a = 0;
    // Start is called before the first frame update
    void Start()
    {

        EnableScreen(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (screenIsActive == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                a++;
                StartCoroutine(CloseTutorial());
            }
        }

    }
    void EnableScreen(int i)
    {
        screens[a].SetActive(true);
        screenIsActive = true;
    }
    IEnumerator TutorialManager()
    {
        yield return new WaitForSecondsRealtime(waitForSecond[a]);
        EnableScreen(a);
        //Screen is opened
    }
    IEnumerator CloseTutorial()
    {
        yield return new WaitForSecondsRealtime(1f);
        screens[a].SetActive(false);
        screenIsActive = false;
        StartCoroutine(TutorialManager());
    }
}
