using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Beginning : MonoBehaviour
{
    [SerializeField] GameObject fadeOut;
    [SerializeField] GameObject one;
    [SerializeField] GameObject two;
    [SerializeField] GameObject three;
    [SerializeField] GameObject four;
    [SerializeField] GameObject five;
    [SerializeField] GameObject black;

    // Update is called once per frame
    private void Start()
    {
        Time.timeScale = 0.7f;
    }
    void Update()
    {
        StartCoroutine(FadeOut());
        StartCoroutine(Words());

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(Skip());
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSecondsRealtime(26.5f);
        fadeOut.SetActive(true);
    }

    IEnumerator Words()
    {
        yield return new WaitForSecondsRealtime(3.5f);
        one.SetActive(true);


        yield return new WaitForSecondsRealtime(5.5f);
        two.SetActive(true);


        yield return new WaitForSecondsRealtime(5f);
        three.SetActive(true);


        yield return new WaitForSecondsRealtime(4f);
        four.SetActive(true);


        yield return new WaitForSecondsRealtime(3.5f);
        five.SetActive(true);

        yield return new WaitForSecondsRealtime(3f);
        black.SetActive(true);

        yield return new WaitForSecondsRealtime(4f);
        SceneManager.LoadScene("LoadingScreen");
    }

    IEnumerator Skip()
    {
        black.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        SceneManager.LoadScene("LoadingScreen");

    }
}
