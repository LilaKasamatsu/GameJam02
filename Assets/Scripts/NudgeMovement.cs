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
        // for better control of player velocity
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPosition = Input.mousePosition;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pusher"))
        {
            Transform pusher = FindObjectOfType<PusherNudgeMovement>().transform;
            Debug.Log("PUSHER: " + pusher.name);
            Vector3 dir = transform.position - pusher.position;
            dir.y = 0;
            dir = dir.normalized;
            Vector2 acc = endPosition - startPosition;

            player.rb.AddForce(dir * pushForce, ForceMode.Impulse);

            player.rb.velocity = new Vector3(Mathf.Clamp(player.rb.velocity.x, player.data.minVelocity, player.data.maxVelocity), 0, Mathf.Clamp(player.rb.velocity.z, player.data.minVelocity, player.data.maxVelocity));



            //float mouseX = Input.GetAxis("Mouse X");
            //float mouseY = Input.GetAxis("Mouse Y");
            //player.rb.velocity = new Vector3(Mathf.Clamp(dir.x* mouseY*pushForce, player.data.minVelocity, player.data.maxVelocity), 0, Mathf.Clamp(dir.z* mouseX*pushForce, player.data.minVelocity, player.data.maxVelocity));
            Debug.Log("FORCE: " + player.rb.velocity);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Pusher"))
    //    {
    //        Debug.Log("Entered");
    //        Transform pusher = FindObjectOfType<PusherNudgeMovement>().transform;
    //        Vector3 dir = transform.position - pusher.position;
    //        dir.y = 0;
    //        dir = dir.normalized;
    //        player.rb.AddForce(dir * pushForce, ForceMode.Impulse);
    //        //lastVelocity = lastVelocity.normalized;
    //        //player.rb.AddForce(lastVelocity * pushForce, ForceMode.Impulse);
    //        player.rb.velocity = new Vector3(Mathf.Clamp(dir.x, player.data.minVelocity, player.data.maxVelocity), 0, Mathf.Clamp(dir.z, player.data.minVelocity, player.data.maxVelocity));
    //        Debug.Log("FORCE: " + player.rb.velocity);
    //    }
    //}

}
