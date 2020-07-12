using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject blackFadeIn;
    [SerializeField] GameObject blackFadeOut;
    [SerializeField] GameObject credit;
    Animator creditAnim;
    [SerializeField] float timer = 2f;
    [SerializeField] float secondTimer = 2f;

    bool creditIsOpen = false;

    void Start()
    {
        creditAnim = credit.GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            blackFadeIn.SetActive(false);
            timer = 2f;
        }
        if (creditIsOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseCredit();
            }
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
        PlayerPrefs.SetInt("scene", 3);
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("LoadingScreen");
        //transform.parent.GetChild(1).gameObject.SetActive(true);
        
    }

    IEnumerator LoadTutorial()
    {
        PlayerPrefs.SetInt("scene", 2);
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("LoadingScreen");
        //transform.parent.GetChild(1).gameObject.SetActive(true);
    }
    public void OpenCredit()
    {
        //open credit
        creditIsOpen = true;
        creditAnim.SetTrigger("fadeIn");
    }
    public void CloseCredit()
    {
        creditIsOpen = false;

        creditAnim.SetTrigger("fadeOut");
    }
}
