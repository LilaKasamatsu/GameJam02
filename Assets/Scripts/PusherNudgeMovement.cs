using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherNudgeMovement : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        FollowMouse();
        LookTowardsPlayer();
    }

    void LookTowardsPlayer()
    {
        transform.LookAt(player.transform);
        transform.Rotate(new Vector3(0, -90, 0));
    }

    void FollowMouse()
    {
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);

        RaycastHit hit;
        if(Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
    }

}
