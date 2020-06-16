using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewRespwan : MonoBehaviour
{
    Vector3 respawnLoc;
    Respwan respwanPlayer;

    // Start is called before the first frame update
    void Start()
    {
        respawnLoc = GetComponent<Transform>().position;
        respwanPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Respwan>();
    }
    public void SetNewLocation()
    {
        //  respwanPlayer.location = respawnLoc;
        Debug.Log("wsd");
        respwanPlayer.location = respawnLoc;
    }

}
