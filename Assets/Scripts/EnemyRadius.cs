using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour
{
    EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = transform.parent.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealth.xyz = "new value";
    }
}
