using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public EnvironmentData health;

    private Material material;
    private float lerpDuration = 1.5f;
    private float time;
    private bool dead = true;

    private PlayerEnergyLvl playerEnergy;

    // Start is called before the first frame update
    void Start()
    {

        playerEnergy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyLvl>();
        material = GetComponent<MeshRenderer>().material;
        material.color = health.colorDead;
        //material.color = health.colorAlive;

    }

    // Update is called once per frame
    void Update()
    {

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
        while (true)
        {
            material.color = Color.Lerp(start, end, time);
           // Debug.Log("1");
            //if (time < 1)
            //{
            time += Time.deltaTime / lerpDuration;
            if (time >= 1.2)
            {
                break;
                //yield return null;
            }

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (dead == true)
            {
                StartCoroutine(ChangeColor(material.color, health.colorAlive));
                playerEnergy.AddEnergy(2);
                dead = false;
            }
        }
        if (other.transform.CompareTag("Enemy"))
        {
            Debug.Log("enemy");
            if (dead == false) ;
            {
                dead = true;
                this.GetComponent<MeshRenderer>().material.color = health.colorDead;
                //StartCoroutine(ChangeColor(material.color, health.colorDead));
                StartCoroutine(ChangeColor(material.color, health.colorDead));
            }


        }
        //void EnemyEnters()
        //{
        //    Debug.Log("enemy enters");
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
}
