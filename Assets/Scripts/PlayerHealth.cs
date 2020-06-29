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

    //[SerializeField] Material materialToLerpTo;
   // [SerializeField] Material materialStart;

   // [SerializeField] TrailRenderer trail;

    //  public Vignette viginetti = null;

    private float b;
    public bool isDying;
    private bool reducing = false;
    //   [SerializeField] GameObject playerVFX;
    UnityEngine.Rendering.Universal.Vignette vignette;
    UnityEngine.Rendering.Universal.ColorAdjustments colorAdj;

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







        if (!volumeProfile.TryGet(out colorAdj)) throw new System.NullReferenceException(nameof(colorAdj));

        colorAdj.saturation.Override(0f);



    }

    // Update is called once per frame
    void Update()
    {
        //camVolume.weight = b;
       //Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude < 0.5)
        {

            isDying = true;
            player.deathTimer += Time.deltaTime;
            vignette.intensity.Override(b);
            if (reducing == false)
            {

                b += Time.deltaTime;
                if (b > 0.9)
                {
                    reducing = true;
                }
            }
            else
            {
                b -= Time.deltaTime;
                if (b < 0.1)
                {
                    reducing = false;
                }
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

    }
    IEnumerator Respawning()
    {
        yield return new WaitForSecondsRealtime(1f);
        transform.position = respawnScript.location;
    }
    public void ScreenBlink()
    {
        StartCoroutine(ScreenBlinkOnRadiusEnter());
       // Debug.Log("nnn");
    }
    IEnumerator ScreenBlinkOnRadiusEnter()
    {
        float a = 0;
        bool reducing = false;

        while (true)
        {
            colorAdj.saturation.Override(a);
            if (reducing == false)
            {
                a-=5;
                if (a < -90)
                {
                    reducing = true;
                    //a++;
                    // break;

                }
            }
            else
            {
                a+=5;
                if (a < 0)
                {
                    colorAdj.saturation.Override(0);
                    //a++;
                    break;

                }

            }




            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
