using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerInteraction : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] bool sleeping;
    [SerializeField] List<Vector3> location = new List<Vector3>();
    [SerializeField] float speed;
    Vector3 currLocation;
    Transform newTransform;
    Vector3 newLocation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //curr
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sleeping)
        {
           // Sleeping();
        }
        else
        {
          //  Moving();
        }
        
    }
    void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, newTransform.position,speed );
        if (transform.position == newTransform.position)
        {
            Debug.Log("ssss");
        }
        //foreach (Vector3 element in location)
        //{

        //}

    }
    void SetNewLocation()
    {
        for (int i=0; i<location.Count; i++)
        {
            newLocation = location[i];
            newTransform.position = newLocation;
        }
    }
    void Sleeping()
    {

    }
}
