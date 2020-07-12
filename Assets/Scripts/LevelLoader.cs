using Packages.Rider.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Text m_Text;
    public Button m_Button;

    void Start()
    {
        //Call the LoadButton() function when the user clicks this Button
        //   m_Button.onClick.AddListener(LoadButton);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        //yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("scene"));

        while(asyncOperation.progress < 1)
        {
            yield return new WaitForEndOfFrame();
        }

        ////Don't let the Scene activate until you allow it to
        //asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);
        ////When the load is still in progress, output the Text and progress bar
        //while (!asyncOperation.isDone)
        //{
        //    //Output the current progress
        //    m_Text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
        //    Debug.Log(asyncOperation.progress);

        //    // Check if the load has finished
        //    if (asyncOperation.progress >= 0.9f)
        //    {
        //        Debug.Log(asyncOperation.progress);
        //        Debug.Log("Press the space bar to continue");
        //        m_Text.text = "Press the space bar to continue";
        //        //Wait to you press the space key to activate the Scene
        //        if (Input.GetKeyDown(KeyCode.Space))
        //            //Activate the Scene
        //            asyncOperation.allowSceneActivation = true;
        //    }

        //    yield return null;
        //}
        //if (asyncOperation.isDone)
        //{
        //    Debug.Log("donee");

        //}
    }
}
//{
//    public GameObject loadingScreen;
//    public Slider slider;

//    public void LoadLevel()
//    {
//        StartCoroutine(LoadAsynchronously(3));
//    }

//    IEnumerator LoadAsynchronously(int sceneIndex)
//    {
//        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
//        operation.allowSceneActivation = false;
//        // loadingScreen.SetActive(true);

//        while (!operation.isDone)
//        {
//            float progres = Mathf.Clamp01(operation.progress / .9f);

//            slider.value = progres;
//            // slider.value = progress;
//            Debug.Log("1");
//            Debug.Log(progres);

//            //if (operation.isDone)
//            //{
//            //    Debug.Log("done");
//            //    if (Input.GetKeyDown(KeyCode.Space))
//            //    {
//            //        operation.allowSceneActivation = true;
//            //    }
//            //    yield return null;
//            //}
//        }
//    }
//}


//    public GameObject loadingScreenObj;
//    public Slider slider;
//    AsyncOperation async;

//    public void LoadScreen()
//    {
//        StartCoroutine(LoadingScreen());
//    }

//    IEnumerator LoadingScreen()
//    {

//        yield return new WaitForSecondsRealtime(0f);
//       // loadingScreenObj.SetActive(true);
//        async = SceneManager.LoadSceneAsync(2);
//        async.allowSceneActivation = false;
//        while(async.isDone == false)
//        {
//            slider.value = async.progress;
//            if (async.progress == 0.9f)
//            {
//                slider.value = 1f;
//                async.allowSceneActivation = true;
//            }
//            yield return null;
//        }
//    }



//}
