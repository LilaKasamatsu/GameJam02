using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewRespwan : MonoBehaviour
{
    Vector3 respawnLoc;
    Respwan respwanPlayer;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        respawnLoc = GetComponent<Transform>().position;
        respwanPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Respwan>();
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player);
        Debug.Log(respwanPlayer.location);
    }
    public void SetNewLocation()
    {
        //  respwanPlayer.location = respawnLoc;
        Debug.Log("wsd");
        respwanPlayer.location = respawnLoc;
    }

}
