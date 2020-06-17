using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointRadiusxpanding : MonoBehaviour
{
    Vector3 expansion;
    [SerializeField] float expansionSpeed;
    Vector3 expansionWIP;
    // Start is called before the first frame update
    void Start()
    {
        expansionWIP = transform.localScale;
        expansionWIP = new Vector3(expansionWIP.x / 20, expansionWIP.y / 20, expansionWIP.z / 20);
        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    
   public void Expanding()
    {
        //Debug.Log("now");
        StartCoroutine(WaitForExpansion());
      
    }
    IEnumerator WaitForExpansion()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSecondsRealtime(0.2f);

          //  Debug.Log(expansion);

            expansion = new Vector3(expansion.x += expansionWIP.x, expansion.y += expansionWIP.y, expansion.z += expansionWIP.z);
            transform.localScale = expansion;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("enemy tod");
        }
    }   private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("enemy tod");
        }
    }
}
