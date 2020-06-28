using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSmooth;
    [SerializeField] float orbitSmooth;

    Vector3 currentMousePosition;
    Vector3 lastMousePosition;
    Vector3 mouseDelta;
    Vector3 lastMouseDelta;

    private void FixedUpdate()
    {
        FollowTarget();
        OrbitTarget();
    }

    private void FollowTarget()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, followSmooth * Time.deltaTime);
    }

    private void OrbitTarget()
    {
        currentMousePosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
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
}
