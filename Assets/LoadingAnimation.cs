using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{

    [SerializeField] GameObject dotOne;
    [SerializeField] GameObject dotTwo;
    [SerializeField] GameObject dotThree;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Loading());
    }

    // Update is called once per frame
    IEnumerator Loading()
    {
        dotOne.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        dotTwo.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        dotThree.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        dotOne.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        dotTwo.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        dotThree.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(Loading());
    }
}
