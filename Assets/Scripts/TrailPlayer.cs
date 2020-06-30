using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPlayer : MonoBehaviour
{

    TrailRenderer tail;
    Rigidbody rb;
    float t = 0;
    float maxWidth = 1.5f;
    float currWidth;
    float maxMagnitude = 10f;
    [SerializeField] Color newColorForTrail;
    Material tailMaterial;
    Color beginColor;

    bool isLerping = false;
    // Start is called before the first frame update
    void Start()
    {
        tailMaterial = GameObject.Find("Trail").GetComponent<Renderer>().material;
        beginColor = tailMaterial.color;
        rb = GetComponent<Rigidbody>();
        tail = GameObject.Find("Trail").GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        t = rb.velocity.magnitude / maxMagnitude;
        tail.startWidth = t * maxWidth;
        if (t >= 1)
        {
           // tailMaterial.color = beginColor;
            tailMaterial.SetColor("_EmissionColor", beginColor);
            Debug.Log("change color");
            t = 1;
        }
        else
        {
            Debug.Log("new color");
        
            tailMaterial.SetColor("_EmissionColor", newColorForTrail);
        }
       
    }
}
