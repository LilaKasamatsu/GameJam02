using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
//using UnityEngine.Rendering.LWRP;


public class PlayerHealth : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject fadeOut;

    GameObject respawn;
    GameEnd gamEnd;
    SetAllHexagonsToDefault hex;

    PlayerData player;
    Renderer rend;
    Rigidbody rb;
    Respwan respawnScript;
    PlayerLightManager playerLight;


    // PostProcessingData
    Camera cam;
    Volume camVolume;

    [SerializeField] GameObject blackOut;

    //[SerializeField] Material materialToLerpTo;
    // [SerializeField] Material materialStart;

    // [SerializeField] TrailRenderer trail;

    //  public Vignette viginetti = null;

    private float b;
    private float a;
    public bool isDying;
    private bool reducing = false;
    public bool noVignette = false;

    //   [SerializeField] GameObject playerVFX;
    UnityEngine.Rendering.Universal.Vignette vignette;
    UnityEngine.Rendering.Universal.ColorAdjustments colorAdj;

    // Start is called before the first frame update
    void Start()
    {
        hex = GameObject.Find("Hex").GetComponent<SetAllHexagonsToDefault>();
        gamEnd = GameObject.Find("Enemies").GetComponent<GameEnd>();
        respawn = GameObject.Find("Player Respawn");
        blackOut.SetActive(false);
        rend = GetComponent<Renderer>();
        player = GetComponent<Player>().data;
        respawnScript = GetComponent<Respwan>();
        playerLight = GetComponent<PlayerLightManager>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        UnityEngine.Rendering.VolumeProfile volumeProfile = cam.GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));
        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));

        vignette.intensity.Override(0f);







        if (!volumeProfile.TryGet(out colorAdj)) throw new System.NullReferenceException(nameof(colorAdj));

        colorAdj.saturation.Override(0f);

        fadeOut.SetActive(false);

    }

    // Update is called once per frame
    public void Update()
    {
        //    Debug.Log(Time.deltaTime);
        if (noVignette == false)
        {
            if (rb.velocity.magnitude < 0.5)
            {
                ReduceLight();
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
                    Respawn();
                    //GAMEOVER
                }
            }
            else
            {
                AddLight();
                isDying = false;
                player.deathTimer = 0;
                if (b >= 0.1)
                {
                    vignette.intensity.Override(b);
                    b -= Time.deltaTime;
                }
                else
                {
                    b = 0;
                    //a = 1;
                }
            }
        }
        else
        {
            for (int i = 0; i < playerLight.licht.Count; i++)
            {

                playerLight.licht[i].intensity = playerLight.lichtMaxWert[i];

            }
        }
    }

    private void Respawn()
    {
        StartCoroutine(Respawning());
        blackOut.SetActive(true);
    }
    void ReduceLight()
    {
        //  Debug.Log("reduce light");
        for (int i = 0; i < playerLight.licht.Count; i++)
        {
            playerLight.licht[i].intensity = playerLight.lichtMaxWert[i] * a;
        }
        if (a <= 0.1)
        {
            a = 0.1f;
        }
        else
        {
            a -= Time.deltaTime / 5;

        }
    }
    void AddLight()
    {
        if (a <= 0.95)
        {
            a += Time.deltaTime;
            for (int i = 0; i < playerLight.licht.Count; i++)
            {

                playerLight.licht[i].intensity = playerLight.lichtMaxWert[i] * a;
            }
        }
        else
        {
            a = 1;

            for (int i = 0; i < playerLight.licht.Count; i++)
            {

                playerLight.licht[i].intensity = playerLight.lichtMaxWert[i];
            }
        }
    }
    IEnumerator Respawning()
    {
        yield return new WaitForSecondsRealtime(3f);
        hex.PlayerRespwan();
        gameOverScreen.SetActive(true);

        yield return new WaitForSecondsRealtime(3f);
        fadeOut.SetActive(true);

        yield return new WaitForSecondsRealtime(3f);
        transform.position = respawn.transform.position;
        gamEnd.OnRespwan();
        //  SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // transform.position = respawnScript.location;
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
            //Debug.Log(a);
            if (reducing == false)
            {
                a -= 3;
                if (a < -90)
                {
                    reducing = true;
                    //a++;
                    // break;

                }
            }
            else
            {
                a += 3;
                if (a > 0)
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
