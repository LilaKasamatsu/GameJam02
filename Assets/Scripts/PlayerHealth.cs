using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerData player;
    
    Rigidbody rb;
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
            player.deathTimer += Time.deltaTime;
            if(player.deathTimer >= player.deathTime)
            {
                Debug.Log("GAME OVER");
                //GAMEOVER
            }
        }
        else
        {
            player.deathTimer = 0;
        }    
    }

    private void Respawn()
    {
        transform.position = respawnScript.location;
    }
}
