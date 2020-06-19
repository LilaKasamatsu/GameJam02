using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerData player;

    Rigidbody rb;
    Respwan respawnScript;

    // PostProcessingData
    Camera cam;
    Volume camVolume;

    // Start is called before the first frame update
    void Start()
    {
        respawnScript = GetComponent<Respwan>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        camVolume = cam.GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Debug.Log(rb.velocity.magnitude);
        //if (rb.velocity.x < 0.05f && rb.velocity.z < 0.05f)
        if (rb.velocity.magnitude < 0.5)
        {

            player.deathTimer += Time.deltaTime;
            camVolume.weight += Time.deltaTime;
            Debug.Log("2");
            if (player.deathTimer >= player.deathTime)
            {
                Debug.Log("GAME OVER");
                // Respawn();
                //GAMEOVER
            }
        }
        else
        {
            player.deathTimer = 0;
            // camVolume.weight = 0;
            camVolume.weight -= Time.deltaTime;
            if (camVolume.weight >= 1) { camVolume.weight = 0; }
        }
    }

    private void Respawn()
    {
        transform.position = respawnScript.location;
    }
}
