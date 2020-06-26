using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public EnvironmentData health;

    private Renderer rend;
    //private Renderer rend;
    private float lerpDuration = 1.5f;
    private float time;
    private bool isDead = true;
    private bool isChanging = true;
    private bool isOntop;

    // [SerializeField]

    private PlayerEnergyLvl playerEnergy;
    private PlayerHealth playerHealth;


    // Start is called before the first frame update
    void Start()
    {

        playerEnergy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyLvl>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        rend = GetComponent<Renderer>();
        rend.material = health.colorDead;

    }

    // Update is called once per frame
    void Update()
    {

        if (isOntop == true)
        {
           // Debug.Log("jetzte2");
            if (isDead == false)
            {
               // Debug.Log("jetzte1");
                //Debug.Log(isChanging);
              //  Debug.Log(playerHealth.isDying);
                if (isChanging == false && playerHealth.isDying == true)
                {

                   // Debug.Log("jetzte");
                    StartCoroutine(NoAir());
                    isDead = true;

                }
            }
        }
    }
    IEnumerator NoAir()
    {
        yield return new WaitForSecondsRealtime(0f);
        if (isOntop == true)
        {
            // Debug.Log("ffff");
            StartCoroutine(ChangeColor(rend.material, health.colorDead));
        }
    }


    IEnumerator ChangeColor(Material start, Material end)
    {
        // Debug.Log("11");
        isChanging = true;
        while (true)
        {
            rend.material.Lerp(start, end, time);

            time += Time.deltaTime / lerpDuration;
            rend.material.SetFloat("MinMovement", 0.5f);
            rend.material.SetFloat("MaxMovement", 4f);
            rend.material.SetFloat("Seed", 1f);
            rend.material.SetVector("Amount", new Vector2 (0.5f,1));
          
            if (time >= 1.2)
            {
                rend.material = end;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        time = 0;
       // rend.material.SetFloat("MaxMovement", 10f);
        isChanging = false;
        Debug.Log("finish");

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isOntop = true;
            if (isDead == true)
            {

                Debug.Log("start coroutne");

                StartCoroutine(ChangeColor(rend.material, health.colorAlive));
                playerEnergy.AddEnergy(2);
                isDead = false;
            }
        }
        if (other.transform.CompareTag("Enemy"))
        {
            //  Debug.Log("enemy");
            if (isDead == false)
            {
                isDead = true;
                this.GetComponent<MeshRenderer>().material = health.colorDead;
                StartCoroutine(ChangeColor(rend.material, health.colorDead));
            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        isOntop = false;
    }


    //void ColorChange(Color start, Color end)
    //{
    //    rend.color = Color.Lerp(start, end, time);

    //    if (time < 1.2)
    //    {
    //        time += Time.deltaTime / lerpDuration;
    //    }
    //}

}
