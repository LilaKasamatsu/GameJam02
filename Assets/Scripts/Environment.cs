using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public EnvironmentHealth health;

    private Material material;
    private float lerpDuration = 1.5f;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //this.GetComponent<MeshRenderer>().material.color = health.colorAlive;
            ColorChange(material.color, health.colorAlive);
        }
        else if (collision.transform.CompareTag("Enemy"))
        {
            this.GetComponent<MeshRenderer>().material.color = health.colorDead;
            ColorChange(material.color, health.colorDead);
        }
    }

    IEnumerator ChangeColor(Color start, Color end)
    {
        while (true)
        {
            material.color = Color.Lerp(start, end, time);

            if (time < 1)
            {
                time += Time.deltaTime / lerpDuration;
            }

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<MeshRenderer>().material.color = health.colorAlive;
    }


    void ColorChange(Color start, Color end)
    {
       
    }
}
