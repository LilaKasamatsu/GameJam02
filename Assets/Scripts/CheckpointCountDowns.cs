using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCountDowns : MonoBehaviour
    
{
    float maxCountdown;
    bool start = true;
    float startTime;
    float t;
    Checkpoint checkPoint;
    float countdown;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    // Start is called before the first frame update
    void Start()
    {
        maxCountdown = gameObject.GetComponentInParent<Checkpoint>().maxCountdown;
        checkPoint = gameObject.GetComponentInParent<Checkpoint>();

    }

    // Update is called once per fram
    private void OnEnable()
    {
        t = 0;
        Debug.Log(t);
        startTime = Time.time;

    }
    void Update()
    {
        if (start == true)
        {
            //Debug.Log("2");

            t++;
            Debug.Log(t);
            float b = t/maxCountdown;

            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, b);
            //if (t > 3)
            //{
            //    start = false;
            //}
            //start = false;
            if (t > maxCountdown)
            {
                checkPoint.CountdownFinish();
                start = false;

            }
        }

    }
}
