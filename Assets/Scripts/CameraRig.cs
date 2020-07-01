using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSmooth;
    [SerializeField] float orbitSmooth;
    [SerializeField] Transform posWhenHold;

    Vector3 currentMousePosition;
    Vector3 lastMousePosition;
    Vector3 mouseDelta;
    Vector3 lastMouseDelta;

    [HideInInspector] public bool isHolded=false;
    bool holdCoroutineActive = false;
    GameObject cam;
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void FixedUpdate()
    {
        FollowTarget();
        OrbitTarget();
        if (isHolded == true)
        {
            //if (holdCoroutineActive == false)
            //{
            //    Debug.Log("222");
            //    LerpPos(cam.transform, posWhenHold);
            //    holdCoroutineActive = true;
            //}

        }

    }

    private void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, followSmooth * Time.deltaTime);
    }

    private void OrbitTarget()
    {
        currentMousePosition = Input.mousePosition;

        if (Input.GetMouseButton(2))
        {
            mouseDelta = lastMousePosition - currentMousePosition;

            mouseDelta = Vector3.Lerp(lastMouseDelta, mouseDelta, orbitSmooth * Time.deltaTime);

            transform.Rotate(0, mouseDelta.x, 0, Space.World);
            //transform.Rotate(mouseDelta.y, 0, 0, Space.Self);
        }

        if (Input.GetMouseButtonUp(1))
        {
            mouseDelta = Vector3.zero;
        }

        lastMouseDelta = mouseDelta;
        lastMousePosition = currentMousePosition;
    }
    //public void EnemyHoldPosition()
    //{
    //    StartCoroutine(LerpPos());
    //}
    void LerpPos(Transform start, Transform end)
    {
        //Debug.Log("111");
        //while (true)
        //{


        transform.position = end.position;
        transform.position = Vector3.Lerp(start.position, end.position, followSmooth * Time.deltaTime);
       
            if (transform.position == end.position)
            {
              
               // Debug.Log("fff");
                isHolded = false;

            }
        //}
       // break;
    }
}
