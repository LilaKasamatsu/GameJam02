using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beginning : MonoBehaviour
{
    [SerializeField] GameObject fadeOut;
    [SerializeField] GameObject black;
    [SerializeField] GameObject one;
    [SerializeField] GameObject two;
    [SerializeField] GameObject three;
    [SerializeField] GameObject four;
    [SerializeField] GameObject five;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FadeOut());
        //StartCoroutine(Words());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSecondsRealtime(17f);
        fadeOut.SetActive(true);
    }

    IEnumerator Words()
    {
        yield return new WaitForSecondsRealtime(4f);
        black.SetActive(true);
        one.SetActive(true);
        yield return new WaitForSecondsRealtime(4f);
        black.SetActive(false);

    }
}
