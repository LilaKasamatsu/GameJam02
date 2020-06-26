using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGlow : MonoBehaviour

{
    [SerializeField] float valueMax;
    [SerializeField] float valueMin;

    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        SetValue();
        
    }
    void SetValue()
    {
        float value = Random.Range(valueMin, valueMax);
        rend.material.SetFloat("GlowSpeed", value);
        Debug.Log(rend.material.GetFloat("GlowSpeed"));

    }

    // Update is called once per frame

}
