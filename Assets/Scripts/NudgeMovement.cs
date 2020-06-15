using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NudgeMovement : MonoBehaviour
{

    public float pushForce = 3f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {

        Vector3 dir = Input.mousePosition - transform.position;

        dir = -dir.normalized;

        rb.AddForce(dir * pushForce);

            
    }
}
