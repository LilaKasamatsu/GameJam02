using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] int countdown;
    [SerializeField] int maxCountdown;
    [SerializeField] GameObject setNewRespawn;
    bool broken = true;
    bool inRadius;

    GameObject fixedCheckpoint;
    //SetNewRespwan setNewRespawn;

    // Start is called before the first frame update
    void Start()
    {

        fixedCheckpoint = transform.GetChild(1).gameObject;

        //setNewRespawn = transform.GetChild().gameObject;
        //setNewRespawn.GetComponent<SetNewRespwan>().SetNewRespawn();
        Debug.Log(setNewRespawn);
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

        // all enemys are triggered
        Countdown();

    }
    void Countdown()
    {

        if (inRadius == true)
        {

            countdown++;



            if (countdown == maxCountdown)
            {

                inRadius = false;
                broken = false;
                fixedCheckpoint.SetActive(true);
               // setNewRespawn.GetComponent<SetNewRespwan>().SetNewLocation();


            }
        }

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
