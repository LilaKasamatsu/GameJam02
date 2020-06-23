using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NudgeMovement : MonoBehaviour
{
    #region Attributes
    Player player;
    public float pushForce = 3f;


    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 dir = Vector3.zero;

    private Vector3 lastVelocity;
    private Vector3 lastPos;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }


    private void OnMouseEnter()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        player.rb.AddForce(mouseX*10, 0, mouseY* 10, ForceMode.Impulse);
        Debug.Log("Mouse Entered");
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Pusher"))
    //    {
    //        float mouseX = Input.GetAxis("Mouse X");
    //        float mouseY = Input.GetAxis("Mouse Y");
    //        dir = collision.collider.transform.position - transform.position;
    //        dir = dir.normalized;

    //        //dir = new Vector3(mouseX, 0, mouseY);
    //        //dir = dir.normalized;
    //        this.GetComponent<Rigidbody>().AddForce(dir * pushForce, ForceMode.Impulse);
    //        Debug.Log("PLAYER HIT" + this.GetComponent<Rigidbody>().velocity);
    //    }
    //}

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.CompareTag("Pusher"))
    //    {
    //        float mouseX = Input.GetAxis("Mouse X");
    //        float mouseY = Input.GetAxis("Mouse Y");
    //        dir = collision.transform.position;
    //        dir = -dir.normalized;
    //        this.GetComponent<Rigidbody>().AddForce(dir * pushForce, ForceMode.Impulse);
    //        Debug.Log("PLAYER HIT" + this.GetComponent<Rigidbody>().velocity);
    //    }
    //}

}
