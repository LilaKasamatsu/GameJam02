using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    PlayerData player;
    Rigidbody rb;
    Respwan respawnScript;

    // PostProcessingData
    Camera cam;
    Volume camVolume;

    private float b;
    public bool isDying;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>().data;
        respawnScript = GetComponent<Respwan>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        camVolume = cam.GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        camVolume.weight = b;
        if (rb.velocity.magnitude < 0.5)
        {
            isDying = true;
            player.deathTimer += Time.deltaTime;
            if (b <=0.9)
            {

                b += Time.deltaTime;
            }
            if (player.deathTimer >= player.deathTime)
            {
                Debug.Log("GAME OVER");
                // Respawn();
                //GAMEOVER
            }
        }
        else
        {
           isDying = false;
            player.deathTimer = 0;
            if (b >= 0.1)
            {
                
                b -= Time.deltaTime;
            }
            //else { b = 0; }
            
           // if (camVolume.weight >= 1) { camVolume.weight = 0; }
        }
    }

    private void Respawn()
    {
        transform.position = respawnScript.location;
    }
}
