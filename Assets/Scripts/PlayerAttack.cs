using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public PlayerData player;

    public Transform enemy;

    float attackStartAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(enemy.position, transform.position) < player.attackRadius)
        {
            Debug.Log("ATTACK ENEMY");
            GetComponent<MeshRenderer>().material.color = player.attackColor;
            attackStartAngle = Vector3.SignedAngle(new Vector3(0,0,1), transform.position - enemy.position, Vector3.up);
            Debug.Log("ANGLE: " + attackStartAngle);
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = player.standardColor;
        }
    }


}
