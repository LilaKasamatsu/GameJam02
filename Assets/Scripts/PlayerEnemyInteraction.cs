using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyInteraction : MonoBehaviour
{
    public Player player;

    Coroutine freePlayer;

    private bool isUsingSpecialAttack;
    PlayerSpecialAttack specialAttack;
    PlayerEnergyLvl playerEnergy;

    Transform enemyHold;
    //[HideInInspector] public bool canAttack = true;
    [HideInInspector] public bool can2Attack = true;

    public bool enemyHit = false;

    // Start is called before the first frame update
    void Start()
    {
        playerEnergy = GetComponent<PlayerEnergyLvl>();
        specialAttack = GetComponentInChildren<PlayerSpecialAttack>();
    }

    private void OnDrawGizmos()
    {
        Vector3 groundPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundPosition, player.data.specialAttackRadius);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(IsAttackVelocity());
        // Debug.Log(can2Attack);
        if (IsAttackVelocity())
        {
            this.GetComponent<MeshRenderer>().material.color = Color.red;
            player.data.isAttacking = true;
        }
        else
        {
            this.GetComponent<MeshRenderer>().material.color = Color.gray;
            player.data.isAttacking = false;
        }
        // Debug.Log(can2Attack);
        // Debug.Log(can2Attack);
        if (Input.GetMouseButtonDown(1))
        {
          //  Debug.Log("isusingspecial ttack ist true");
            if (isUsingSpecialAttack == false)
            {
               // Debug.Log("isusingspecial ttack ist false");

                if (can2Attack == true)
                {
                    // Debug.Log("no");
                   // Debug.Log("can2attack ttack ist true");
                    if (player.data.energyLVL >= player.data.energyLVLforSpecialAttack)
                    {
                        can2Attack = false;
                        isUsingSpecialAttack = false;
                        OnSpecialAttack();
                        StartCoroutine(SetToFalse());
                        playerEnergy.MinusEnergy();
                    }
                    else
                    {
                       // Debug.Log("no energy :(");
                    }
                }
            }
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
            Debug.Log("urpplayer");
            enemy.MinusHealth(1);
            enemyHit = true;
        }
    }

    public void OnSpecialAttack()
    {

        // Do some animation magic.
       // isUsingSpecialAttack = true;
        StartCoroutine(specialAttack.Expand());
       


    }
    IEnumerator SetToFalse()
    {
       // Debug.Log("aa");
        yield return new WaitForSeconds(3f);
        isUsingSpecialAttack = false;
    }

    public void OnRelease()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.gray;
        GetComponent<Rigidbody>().isKinematic = false;
        if(freePlayer != null)
        {
            StopCoroutine(freePlayer);

        }
        player.data.isMovable = true;
        if (currentEnemy != null)
        {
            currentEnemy.GetComponent<EnemyPlayerInteraction>().stunnedCooldown = 1f;
        }
    }

    Transform currentEnemy;

    public void OnHold(Transform enemy, Transform target)
    {
        currentEnemy = enemy;
        MoveToEnemy(target);
        if (player.data.isMovable)
        {

            // Debug.Log("2");
            player.data.isMovable = false;
            freePlayer = StartCoroutine(EscapeEnemy());
            GetComponent<Rigidbody>().isKinematic = true;
        }
        // Debug.Log("1");
    }
    void MoveToEnemy(Transform target)
    {

        Vector3 i = new Vector3(target.position.x, target.position.y, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, i, 5 * Time.deltaTime);
    }
    private IEnumerator EscapeEnemy()
    {
        this.GetComponent<MeshRenderer>().material.color = Color.black;
        int buttonClickCounter = 0;
        while (true)
        {
            if (currentEnemy == null)
            {
                OnRelease();
            }

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
