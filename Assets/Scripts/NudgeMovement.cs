using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NudgeMovement : MonoBehaviour
{
    #region Attributes

    public float pushForce = 3f;

    private Rigidbody rb;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 dir = Vector3.zero;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }




    private void OnMouseEnter()
    {
        startPosition = Input.mousePosition;
        Vector3 dir = new Vector3(Input.GetAxis("Mouse X"),0, Input.GetAxis("Mouse Y"));




        dir = dir.normalized;

        rb.AddForce(dir * pushForce, ForceMode.Impulse);    
    }


    //private void OnMouseExit()
    //{
    //    endPosition = Input.mousePosition;
    //    //endPosition.z = -Camera.main.transform.position.z;
    //    //endPosition = Camera.main.ScreenToWorldPoint(endPosition);


    //    //Debug.Log(startPosition);
    //    //Debug.Log(endPosition);

    //    dir = endPosition - startPosition;


    //    dir = dir.normalized;

    //    dir = new Vector3(dir.x, 0, dir.y);
    //    Debug.Log("Dir " + dir);

    //    rb.AddForce(dir * pushForce, ForceMode.Impulse);
    //}
}
