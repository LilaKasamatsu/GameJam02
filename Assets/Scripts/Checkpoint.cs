using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    // float countdown;
    public float maxCountdown;
    [SerializeField] GameObject setNewRespawn;
    [SerializeField] GameObject radiusExpaning;
    bool broken = true;
    bool inRadius;

    GameObject fixedCheckpoint;
    FinalWinning finalWinning;
    //CheckpointCountDowns cPcD;
    //SetNewRespwan setNewRespawn;

    // Start is called before the first frame update
    void Start()
    {
        finalWinning = GameObject.Find("Winning Condition").GetComponent<FinalWinning>();
        //cPcD = fixedCheckpoint.GetComponent<CheckpointCountDowns>();
        fixedCheckpoint = transform.GetChild(1).gameObject;
        //  Debug.Log(setNewRespawn);
        fixedCheckpoint.SetActive(false);
        //location
        finalWinning.checkpoints.Add(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (inRadius == true)
        //{
        //    Countdown();
        //}
    }
    void StartCountdown()
    {

        //countdown = 0;
        fixedCheckpoint.SetActive(true);
        // all enemys are triggered
        // Countdown();


    }
    public void CountdownFinish()
    {

        // if (inRadius == true)
        //{

        //  countdown++;



        //if (countdown == maxCountdown)
        //  {

        // inRadius = false;
        Debug.Log("Countdown Finished");
        broken = false;
        finalWinning.SetToTrue();
        setNewRespawn.GetComponent<SetNewRespwan>().SetNewLocation();
        radiusExpaning.GetComponent<CheckpointRadiusxpanding>().Expanding();

         
        // }
        //}

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
            fixedCheckpoint.SetActive(false);
        }
    }
}
