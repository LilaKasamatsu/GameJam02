using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{/*
    public GameObject loadingScreen;
    public Slider slider;

    public void LoadLevel (string name)
    {
        StartCoroutine(LoadAsynchronously(name));
    }

    IEnumerator LoadAsynchronously (string name)
    {
        yield return new WaitForSecondsRealtime(2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }*/


    public GameObject loadingScreenObj;
    public Slider slider;
    AsyncOperation async;

    public void LoadScreen()
    {
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        yield return new WaitForSecondsRealtime(2f);
        loadingScreenObj.SetActive(true);
        async = SceneManager.LoadSceneAsync(2);
        async.allowSceneActivation = false;
        while(async.isDone == false)
        {
            slider.value = async.progress;
            if (async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }



}
