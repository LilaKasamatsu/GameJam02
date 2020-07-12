using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoLightRotation : MonoBehaviour
{
    [SerializeField] GameObject lightOne;
    [SerializeField] GameObject lightTwo;
    [SerializeField] GameObject lightThree;
    [SerializeField] GameObject lightFour;
    [SerializeField] GameObject lightFive;

    void Update()
    {
        lightOne.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 3f, this.transform.position.z);
        lightTwo.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 3f, this.transform.position.z);
        lightThree.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 3f, this.transform.position.z);
        lightFour.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 3f, this.transform.position.z);
        lightFive.transform.position = this.transform.position;
    }
}
