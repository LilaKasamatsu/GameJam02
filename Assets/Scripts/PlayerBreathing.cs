﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBreathing : MonoBehaviour
{
    [SerializeField] float maxSize = 4.5f;
    [SerializeField] float growFactor = 2f;
    [SerializeField] float waitTime = 0.5f;

    public GameObject player;

    void Start()
    {
        StartCoroutine(Breathe());
    }

    IEnumerator Breathe()
    {
        float timer = 0;

        while (true)
        {
            while (maxSize > transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }
        
            yield return new WaitForSeconds(waitTime);
            //shorter wait time for shorter breaths

            timer = 0;
            while (3.273025f < transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
                yield return null;
            }

            timer = 0;
            yield return new WaitForSeconds(waitTime);
        
        }
        
    }
}
