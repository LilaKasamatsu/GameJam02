using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOverlay : MonoBehaviour
{
    public Camera MainCamera;

    private Camera overlayCam;
    // Start is called before the first frame update
    void Start()
    {
        overlayCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        overlayCam.orthographicSize = MainCamera.orthographicSize;
    }
}
