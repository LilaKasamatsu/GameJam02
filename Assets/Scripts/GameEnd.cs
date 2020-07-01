using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{

    public int enemysInScene;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            enemysInScene++;
        }
    }

    // Update is called once per frame
    public void Delete()
    {
        enemysInScene -= 1;
        if (enemysInScene == 0)
        {
            Debug.Log("end");
        }
    }
}
