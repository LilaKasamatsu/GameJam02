using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    Rigidbody rb;
    public float deathTimer = 0;
    public float deathTime = 3;
    Respwan respawnScript;


    // Start is called before the first frame update
    void Start()
    {
        respawnScript = GetComponent<Respwan>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x < 0.1f && rb.velocity.y < 0.1f)
        {
            deathTimer += Time.deltaTime;
            if(deathTimer >= deathTime)
            {
                Debug.Log("GAME OVER");
                //GAMEOVER
                //Respawn();
            }
        }
        else
        {
            deathTimer = 0;
        }    
    }
    void Respawn()
    {
        transform.position = respawnScript.location;
    }



}
