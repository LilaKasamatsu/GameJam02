using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject blackFadeIn;
    [SerializeField] GameObject blackFadeOut;
    [SerializeField] float timer = 2f;
    [SerializeField] float secondTimer = 2f;

    void Start()
    {
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            blackFadeIn.SetActive(false);
            timer = 2f;
        }
    }

    public void StartGame()
    {
        blackFadeOut.SetActive(true);
        StartCoroutine(LoadGame());
    }

    public void StartTutorial()
    {
        blackFadeOut.SetActive(true);
        StartCoroutine(LoadTutorial());
    }

    IEnumerator LoadGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("GroundLevelDONE01");
    }

    IEnumerator LoadTutorial()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Tutorial_plswork");
    }
    public void OpenCredit()
    {
        //open credit
    }
}
