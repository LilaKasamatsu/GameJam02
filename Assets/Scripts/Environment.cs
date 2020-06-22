using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public EnvironmentData health;

    private Material material;
    private float lerpDuration = 1.5f;
    private float time;
    private bool isDead = true;
    private bool isChanging = true;
    private bool isOntop;

    private PlayerEnergyLvl playerEnergy;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {

        playerEnergy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyLvl>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        material = GetComponent<MeshRenderer>().material;
        material.color = health.colorDead;
        //material.color = health.colorAlive;

    }

    // Update is called once per frame
    void Update()
    {
        if (isOntop == true)
        {
            if (isDead == false)
            {
                if (isChanging == false && playerHealth.isDying == true)
                {

                    Debug.Log("jetzte");
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
            Debug.Log("ffff");
            StartCoroutine(ChangeColor(material.color, health.colorDead));
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {
    //        if (dead == true)
    //        {
    //            //this.GetComponent<MeshRenderer>().material.color = health.colorAlive;
    //            StartCoroutine(ChangeColor(material.color, health.colorAlive));

    //            playerEnergy.AddEnergy(2);
    //            dead = false;
    //        }


    //    }
    //    if (collision.transform.CompareTag("Enemy"))
    //    {
    //        Debug.Log("enemy");
    //    }

    //}



    IEnumerator ChangeColor(Color start, Color end)
    {
        isChanging = true;
        while (true)
        {
            material.color = Color.Lerp(start, end, time);
            time += Time.deltaTime / lerpDuration;
            if (time >= 1.2)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        time = 0;
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

                StartCoroutine(ChangeColor(material.color, health.colorAlive));
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
                this.GetComponent<MeshRenderer>().material.color = health.colorDead;
                StartCoroutine(ChangeColor(material.color, health.colorDead));
            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        isOntop = false;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.transform.CompareTag("Player"))
    //    {


    //        if (isChanging == false && on == true)
    //        {
    //            StartCoroutine(ChangeColor(material.color, health.colorDead));
    //            Debug.Log("sterben");

    //        }
    //    }
    //}

    void ColorChange(Color start, Color end)
    {
        material.color = Color.Lerp(start, end, time);

        if (time < 1.2)
        {
            time += Time.deltaTime / lerpDuration;
        }
    }

}
