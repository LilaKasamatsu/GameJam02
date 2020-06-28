using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
//using UnityEngine.Rendering.LWRP;


public class PlayerHealth : MonoBehaviour
{
    PlayerData player;
    Renderer rend;
    Rigidbody rb;
    Respwan respawnScript;

    // PostProcessingData
    Camera cam;
    Volume camVolume;

    [SerializeField] Material materialToLerpTo;
    [SerializeField] Material materialStart;

    //  public Vignette viginetti = null;

    private float b;
    public bool isDying;
    //   [SerializeField] GameObject playerVFX;
    UnityEngine.Rendering.Universal.Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        player = GetComponent<Player>().data;
        respawnScript = GetComponent<Respwan>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        UnityEngine.Rendering.VolumeProfile volumeProfile = cam.GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));



        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));

        vignette.intensity.Override(0f);



    }

    // Update is called once per frame
    void Update()
    {
        //camVolume.weight = b;
        //  Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude < 0.5)
        {
            isDying = true;
            player.deathTimer += Time.deltaTime;
            //b += Time.deltaTime;
            vignette.intensity.Override(b);
            if (b <= 0.9)
            {
                // rend.material.Lerp(materialStart, materialToLerpTo, Time.deltaTime / 2);
              //  rend.material = materialToLerpTo;
                b += Time.deltaTime / 4;
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
                vignette.intensity.Override(b);
              //  rend.material.Lerp(materialToLerpTo,materialStart, Time.deltaTime / 2);
                b -= Time.deltaTime;
            }
            else
            {
                b = 0;
               // rend.material = materialStart;

            }

            //if (camVolume.weight >= 1)
            //{
            //    vignette.intensity.Override(0f);
            //}
        }
    }

    private void Respawn()
    {
        transform.position = respawnScript.location;
    }
}
