using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigCollider : MonoBehaviour
{
    EnemyPlayerInteraction enemyMain;
    bool detected;
        // Start is called before the first frame update
    void Start()
    {
        enemyMain = gameObject.GetComponentInParent<EnemyPlayerInteraction>();
        //detected = enemyMain.attack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
            //stop
            // detected = true;
            enemyMain.attack = true;
            Debug.Log("aweqwe");
        
    }
    private void OnTriggerExit(Collider other)
    {
        enemyMain.attack = false;
    }
}
