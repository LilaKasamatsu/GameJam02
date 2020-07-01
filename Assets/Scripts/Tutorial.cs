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
    public List<GameObject> gameObjectstoHighlihgt = new List<GameObject>();
    public List<int> waitForSecond = new List<int>();

    GameObject cursor;
    [SerializeField] GameObject overLayBlackScreen;
    [SerializeField] GameObject enemy;
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
            Debug.Log("los");
            begin = false;
        }
        if (screenIsActive == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (end == true)
                {
                    LoadNextScene();

                }
                else
                {
                    Debug.Log("Close Tutorial");

                    StartCoroutine(CloseTutorial());

                    if (a == 5)
                    {
                        // enemy.SetActive(true);
                        //  enemy.GetComponent<EnemyPlayerInteraction>().isAttacking = true;
                        enemy.transform.position = enemyTransform.position;
                        enemy.GetComponent<EnemyPlayerInteraction>().attack = true;
                    }

                    if (a == 10)
                    {
                        end = true;
                    }
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
        Debug.Log("open ssssssscreen");
        EnableScreen(a);
        EnableBlackScreen();
        //Screen is opened
    }
    IEnumerator CloseTutorial()
    {
        screenIsActive = false;
        animBlack.SetTrigger("fade");
        screens[a].GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(1f);
        screens[a].SetActive(false);
        overLayBlackScreen.SetActive(false);
        Time.timeScale = 1;
        cursor.SetActive(true);
        a++;
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
}
