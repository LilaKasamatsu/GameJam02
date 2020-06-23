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

    private void Update()
    {
        Vector3 mPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        mPos = new Vector3(mPos.x, transform.position.y, mPos.z);
        lastVelocity = mPos - lastPos;
        lastPos = mPos;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pusher"))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Transform pusher = FindObjectOfType<PusherNudgeMovement>().transform;
            Vector3 dir = transform.position - pusher.position;
            dir.y = 0;
            dir = dir.normalized;

            dir.x = dir.x;
            dir.y = dir.y;

            player.rb.AddForce(dir * pushForce, ForceMode.Impulse);
            player.rb.velocity = new Vector3(Mathf.Clamp(dir.x, player.data.minVelocity, player.data.maxVelocity), 0, Mathf.Clamp(dir.z, player.data.minVelocity, player.data.maxVelocity));
            Debug.Log("FORCE: " + player.rb.velocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Pusher"))
        {
            Debug.Log("Entered");
            Transform pusher = FindObjectOfType<PusherNudgeMovement>().transform;
            Vector3 dir = transform.position - pusher.position;
            dir.y = 0;
            dir = dir.normalized;
            player.rb.AddForce(dir * pushForce, ForceMode.Impulse);
            //lastVelocity = lastVelocity.normalized;
            //player.rb.AddForce(lastVelocity * pushForce, ForceMode.Impulse);
            player.rb.velocity = new Vector3(Mathf.Clamp(dir.x, player.data.minVelocity, player.data.maxVelocity), 0, Mathf.Clamp(dir.z, player.data.minVelocity, player.data.maxVelocity));
            Debug.Log("FORCE: " + player.rb.velocity);
        }
    }

    //private void OnMouseEnter()
    //{
    //    startPosition = Input.mousePosition;
    //    Vector3 dir = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));

    //    Vector3 mPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    //    mPos = new Vector3(mPos.x, transform.position.y, mPos.z);


    //    //dir = dir.normalized;

    //    //player.rb.AddForce(lastVelocity * pushForce, ForceMode.Impulse);
    //    player.rb.velocity = new Vector3(Mathf.Clamp(dir.x, player.data.minVelocity, player.data.maxVelocity), 0, Mathf.Clamp(dir.z, player.data.minVelocity, player.data.maxVelocity));
    //    //Debug.Log("FORCE: " + player.rb.velocity);
    //}



    ////private void OnMouseExit()
    ////{
    ////    endPosition = Input.mousePosition;
    ////    //endPosition.z = -Camera.main.transform.position.z;
    ////    //endPosition = Camera.main.ScreenToWorldPoint(endPosition);


    ////    //Debug.Log(startPosition);
    ////    //Debug.Log(endPosition);

    ////    dir = endPosition - startPosition;


    ////    dir = dir.normalized;

    ////    dir = new Vector3(dir.x, 0, dir.y);
    ////    Debug.Log("Dir " + dir);

    ////    rb.AddForce(dir * pushForce, ForceMode.Impulse);
    ////}
}
