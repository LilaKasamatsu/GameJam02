using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Tutorial : MonoBehaviour
{
    //[SerializeField] GameObject screen1;
    //[SerializeField] GameObject screen2;
    //[SerializeField] GameObject screen3;
    //[SerializeField] GameObject screen4;

    public List<GameObject> screens = new List<GameObject>();
    //public List<GameObject> gameObjectstoHighlihgt = new List<GameObject>();
    public List<int> waitForSecond = new List<int>();

    GameObject cursor;
    [SerializeField] GameObject overLayBlackScreen;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemy2;
    [SerializeField] Transform enemyTransform;

    bool screenIsActive = false;
    int a = 0;
    bool begin = true;
    bool end = false;

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
        // enemy.SetActive(false);
        //enemy.GetComponent<EnemyPlayerInteraction>().isAttacking = false;
        overLayBlackScreen.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (begin == true)
        {
            StartCoroutine(TutorialManager());
            // Debug.Log("los");
            begin = false;
        }
        if (screenIsActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (end == true)
                {
                    Time.timeScale = 1;
                    LoadNextScene();

                }
                else
                {
                    //  Debug.Log("Close Tutorial");

                    StartCoroutine(CloseTutorial());

                    if (a == 5)
                    {
                        enemy.transform.position = enemyTransform.position;
                        enemy.GetComponent<EnemyPlayerInteraction>().attack = true;
                    } 
                    if (a == 9)
                    {
                        enemy2.transform.position = enemyTransform.position;
                        enemy2.GetComponent<EnemyPlayerInteraction>().attack = true;
                    }

                    //if (a == 11)
                    //{
                    //    end = true;
                    //    Debug.Log("end");
                    //}
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
        // Debug.Log("open screen");
        yield return new WaitForSecondsRealtime(waitForSecond[a]);
        //Debug.Log("open ssssssscreen");
        EnableScreen(a);
        EnableBlackScreen();
        //Screen is opened
    }
    IEnumerator CloseTutorial()
    {
        screenIsActive = false;
        animBlack.SetTrigger("fade");
        screens[a].GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(0.8f);
        screens[a].SetActive(false);
        overLayBlackScreen.SetActive(false);
        Time.timeScale = 1;
        cursor.SetActive(true);
        a++;
        if (a == 11)
        {
            end = true;
          //  Debug.Log("end");

        }
        StartCoroutine(TutorialManager());
    }
    void EnableBlackScreen()
    {
        overLayBlackScreen.SetActive(true);

    }
    void LoadNextScene()
    {
        SceneManager.LoadScene("GroundLevelDONE01");
    }
    //IEnumerator TimeScalePlus()
    //{
    //    float b=0;
    //    while (true)
    //    {
    //        b += 0.1f;
    //        Time.timeScale = b;
    //        if (b >=1)
    //        {
    //            b = 1;
    //            break;
    //        }

    //        yield return new WaitForEndOfFrame();
    //    }
    //    yield return null;
    //}
    //IEnumerator TimeScaleMinus()
    //{
    //    yield return new WaitForEndOfFrame();
    //}
}
