﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDissolve : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.SetFloat("_dissolve", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dissolveNow()
    {
        StartCoroutine(changeValueOverTime(0, 1, 3));
    }

    IEnumerator changeValueOverTime(float fromVal, float toVal, float duration)
    {
        float counter = 0f;

        while (counter < duration)
        {
            if (Time.timeScale == 0)
                counter += Time.unscaledDeltaTime;
            else
                counter += Time.deltaTime;

            float val = Mathf.Lerp(fromVal, toVal, counter / duration);
            gameObject.GetComponent<Renderer>().material.SetFloat("_dissolve", val);
            yield return null;
        }
    }
}
