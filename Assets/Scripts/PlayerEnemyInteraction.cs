using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyInteraction : MonoBehaviour
{
    public Player player;

    Coroutine freePlayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAttackVelocity())
        {
            this.GetComponent<MeshRenderer>().material.color = Color.red;
            player.data.isAttacking = true;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material.color = Color.grey;
            player.data.isAttacking = false;
        }
    }

    public bool IsAttackVelocity()
    {
        return (player.rb.velocity.x > player.data.minAttackVelocity || player.rb.velocity.z > player.data.minAttackVelocity
             || player.rb.velocity.x < -player.data.minAttackVelocity || player.rb.velocity.z < -player.data.minAttackVelocity);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && player.data.isAttacking)
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy.MinusHealth(1);         
        }
    }


    public void OnRelease()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        StopCoroutine(freePlayer);
        player.data.isMovable = true;
        currentEnemy.GetComponent<EnemyPlayerInteraction>().stunnedCooldown = 1f;
        //maybe set an enemy cooldown to prevent instant recapture?

    }

    Transform currentEnemy;

    public void OnHold(Transform enemy)
    {
        currentEnemy = enemy;
        if (player.data.isMovable)
        {
            player.data.isMovable = false;
            freePlayer = StartCoroutine(EscapeEnemy());
            GetComponent<Rigidbody>().isKinematic = true;
        }
        
    }


    private IEnumerator EscapeEnemy()
    {
        int buttonClickCounter = 0;
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                buttonClickCounter++;
            }
            if (buttonClickCounter >= 5)
            {
                OnRelease();
            }
            yield return new WaitForFixedUpdate();
        }
        
    }

}
