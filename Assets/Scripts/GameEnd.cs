using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameEnd : MonoBehaviour
{

    public int enemysInScene;
    public GameObject endingScreen;
    public GameObject fadeOut;
    public Player p;
    public PlayerHealth ph;
    GameObject player;
    GameObject up;
    GameObject cursor;
    GameObject spotlight;
    bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        p = FindObjectOfType<Player>();
        ph = FindObjectOfType<PlayerHealth>();
        player = GameObject.Find("Player");
        up = GameObject.Find("Up");
        cursor = GameObject.Find("pusherTest");
        spotlight = GameObject.Find("PlayerSpotLight");

        spotlight.SetActive(false);
        fadeOut.SetActive(false);
      //  enemysInScene = 9;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            enemysInScene++;
           // Debug.Log("enemy");
        }
    }
    public void OnRespwan()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            child.GetChild(5).GetComponent<Enemy>().Respawn();
            


        }
        enemysInScene = 0;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            enemysInScene++;
        }
       // Debug.Log(enemysInScene);

    }

    // Update is called once per frame
    public void Delete()
    {
        enemysInScene -= 1;
      //  Debug.Log("enemies: " + enemysInScene);
        if (enemysInScene == 0)
        {
           // Debug.Log(enemysInScene);
            p.data.isMovable = false;

            StartCoroutine(Ending());
        }
    }

    void FixedUpdate()
    {
        if(end == true)
        {
        player.transform.position = Vector3.MoveTowards(player.transform.position, up.transform.position, 15f * Time.deltaTime);
        }
    }

    IEnumerator Ending()
    {
        AudioManager.instance.StopSound("Main Music");
        AudioManager.instance.StopSound("Player Death");
        AudioManager.instance.PlaySound("End Scene");
        yield return new WaitForSecondsRealtime(1f);
        player.GetComponent<Rigidbody>().isKinematic = true;
        cursor.SetActive(false);
        ph.noVignette = true;

        yield return new WaitForSecondsRealtime(1.5f);
        end = true;
        spotlight.SetActive(true);

        yield return new WaitForSecondsRealtime(6f);
        endingScreen.SetActive(true);

        yield return new WaitForSecondsRealtime(3f);
        fadeOut.SetActive(true);

        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene("Menu");
    }
}
