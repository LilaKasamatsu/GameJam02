using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Vector3 location;

    [SerializeField] int countdown;
    [SerializeField] int maxCountdown;
    bool broken = true;
    bool inRadius;

    GameObject fixedCheckpoint;

    // Start is called before the first frame update
    void Start()
    {
        location = GetComponent<Transform>().position;
        fixedCheckpoint = transform.GetChild(1).gameObject;
        fixedCheckpoint.SetActive(false);

        //location
    }

    // Update is called once per frame
    void Update()
    {
        if (inRadius == true)
        {
            Countdown();
        }
    }
    void StartCountdown()
    {

        countdown = 0;
        Debug.Log("count down");
        // all enemys are triggered
        Countdown();

    }
    void Countdown()
    {

        if (inRadius == true)
        {

            countdown++;
            Debug.Log(countdown);
         

            if (countdown == maxCountdown)
            {
                Debug.Log("now!");
                inRadius = false;
                broken = false;
                fixedCheckpoint.SetActive(true);
                ResetPlayerLocation();
            }
        }

    }
    void ResetPlayerLocation()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (broken == true)
            {
                Debug.Log("Player");
                inRadius = true;
                StartCountdown();

            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (broken == true)
        {
            Debug.Log("Player");
            inRadius = false;
        }
    }
}
