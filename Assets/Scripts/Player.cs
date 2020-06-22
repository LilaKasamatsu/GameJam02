using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData data;

    [HideInInspector]
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        data.isMovable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
