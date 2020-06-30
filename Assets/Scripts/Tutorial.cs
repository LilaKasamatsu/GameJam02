using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    //[SerializeField] GameObject screen1;
    //[SerializeField] GameObject screen2;
    //[SerializeField] GameObject screen3;
    //[SerializeField] GameObject screen4;

    public List<GameObject> screens = new List<GameObject>();
    public List<int> waitForSecond = new List<int>();

    GameObject cursor;
    [SerializeField] GameObject overLayBlackScreen;
    [SerializeField] GameObject enemy;

    bool screenIsActive = false;
    int a = 0;
    bool begin = true;

    Animator animBlack;
    // Start is called before the first frame update
    void Start()
    {
        animBlack = overLayBlackScreen.GetComponent<Animator>();
        cursor = GameObject.Find("pusherTest");
        for (int i = 0; i < screens.Count; i++)
        {
            screens[i].SetActive(false);
        }
        enemy.SetActive(false);
        overLayBlackScreen.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (begin == true)
        {
            StartCoroutine(TutorialManager());
            Debug.Log("los");
            begin = false;
        }
        if (screenIsActive == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Close Tutorial");

                StartCoroutine(CloseTutorial());
                a++;
                if (a == 6)
                {
                    enemy.SetActive(true);
                }
                if (a == 10)
                {
                    LoadNextScene();
                }

            }
        }

    }
    void EnableScreen(int i)
    {
        screens[a].SetActive(true);
        screenIsActive = true;
        cursor.SetActive(false);
        Time.timeScale = 0f;
    }
    IEnumerator TutorialManager()
    {
        Debug.Log("open screen");
        yield return new WaitForSecondsRealtime(waitForSecond[a]);
        EnableScreen(a);
        EnableBlackScreen();
        //Screen is opened
    }
    IEnumerator CloseTutorial()
    {
        screens[a].SetActive(false);
        animBlack.SetTrigger("fade");
        yield return new WaitForSecondsRealtime(1f);
        screenIsActive = false;
        overLayBlackScreen.SetActive(false);
        Time.timeScale = 1;
        cursor.SetActive(true);
        StartCoroutine(TutorialManager());
    }
    void EnableBlackScreen()
    {
        overLayBlackScreen.SetActive(true);

    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
