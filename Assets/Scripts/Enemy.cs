using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(this.transform.position, data.actionRadius);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(this.transform.position, data.attackRadius);
    }

    private void Start()
    {
        data.player = FindObjectOfType<Player>();
        data.playerEnemy = FindObjectOfType<PlayerEnemyInteraction>();
    }
    public void Respawn()
    {
        transform.position = data.location[0];
       // Debug.Log(data.location[0]);
    } 
}
