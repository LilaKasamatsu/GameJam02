using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float speed = 5.0f;

    public bool moveOnClick = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit))
        {
            this.transform.LookAt(hit.point);
            if (moveOnClick)
            {
                if (Input.GetMouseButton(0) && Vector3.Distance(this.transform.position, hit.point) > 0.0f)
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * this.speed);
                }
            }
            else
            {
                this.transform.Translate(Vector3.forward * Time.deltaTime * this.speed);

            }

        }
    }
}
