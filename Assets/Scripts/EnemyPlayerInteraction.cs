using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerInteraction : MonoBehaviour
{
    Rigidbody rb;
    EnemyData enemy;


    // private bool attackingFeedback;
    private bool isWaiting;
    public bool isHoldingPlayer;
    public bool movingFeedback = true;
    private bool holdingFeedback;
    private bool idleFeedback;
    private bool sleepingFeedback;
    private bool attackFeedback = true;
    public bool attack;

    
    private PlayerEnemyInteraction playerEnemy;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool death;
    [HideInInspector] public float stunnedCooldown = 0f;

    Vector3 currLocation;
    Vector3 newLocation;
    int a;


    Player player;
    PlayerHealth playerHealth;

    CameraRig camRig;

    Vector3 groundPosition;

    private EnemyController enemyController;
    private EnemyDeath enemyDeath;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemy = GetComponent<Enemy>().data;
        enemyDeath = GetComponent<EnemyDeath>();
        enemyController = GetComponent<EnemyController>();
        playerEnemy = enemy.playerEnemy;

        player = GameObject.FindObjectOfType<Player>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        camRig = GameObject.Find("CameraRig").GetComponent<CameraRig>();

        SetNewLocation(0);
    }



    void Update()
    {
        if (!enemyDeath.isDying)
        {
            if (stunnedCooldown <= 0f)
            {
                groundPosition = new Vector3(transform.position.x, 0, transform.position.z);

                isHoldingPlayer = !playerEnemy.IsAttackVelocity() && IsInRange(enemy.actionRadius);

                if (isHoldingPlayer == false)
                {
                    isAttacking = IsInRange(enemy.attackRadius);
                    if (isAttacking)
                    {
                        Attack();
                        if (movingFeedback == true) { MovingFeedback(); }
                    }
                    else if (isWaiting == false)
                    {
                        if (movingFeedback == true) { MovingFeedback(); }
                        Move();
                        attack = false;
                    }
                }
                else
                {
                    Hold();

                    if (holdingFeedback == true)
                    {
                        HoldingFeedback();
                    }
                    else
                    {
                        //stunnedCooldown = 1f;
                    }
                }
            }
            else
            {
                stunnedCooldown -= Time.deltaTime;
            }
            if (death == true) { KillFeedback(); }
        }
  
    }


    void Rotation(Transform target)
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

    }

    float currentAngularVelocity = 0;

    IEnumerator RotateTowards(Vector3 target)
    {
        while (true)
        {
            Vector3 towardTarget = target - transform.position;
            Vector3 towardTargetProjected = Vector3.ProjectOnPlane(towardTarget, transform.up);
            float angToTarget = Vector3.SignedAngle(transform.forward, towardTargetProjected, transform.up);

            float targetAngularVelocity = 0;

            if (Mathf.Abs(angToTarget) > 2)
            {
                if (angToTarget > 0)
                {
                    targetAngularVelocity = 2;
                }
                else
                {
                    targetAngularVelocity = -2;
                }
            }
            else
            {
                yield break;
            }
            currentAngularVelocity = Mathf.Lerp(currentAngularVelocity, targetAngularVelocity, 1 - Mathf.Exp(-2 * Time.deltaTime));

            transform.Rotate(0, Time.deltaTime * currentAngularVelocity, 0, Space.World);
            yield return null;

        }
    }

    void Attack()
    {
        //Rotation(player.transform);
        Rotation(player.transform);
        if (attack == true)
        {

            //  attackFeedback = false;
            Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, playerPos, enemy.attackSpeed * Time.deltaTime);
        }
        else
        {
            // Rotation(player.transform);
            StartCoroutine(WaitBeforeAttack());
        }
    }
    IEnumerator WaitBeforeAttack()
    {
        if (attackFeedback == true)
        {
            playerHealth.ScreenBlink();
            //StartCoroutine(AttackingFeedback());
            attackFeedback = false;

        }
        float a;
        if (enemy.isSleepingEnemy == true)
        {
            a = 1.6f;
            movingFeedback = true;
        }
        else
        {
            a = 0.4f;
        }
        yield return new WaitForSecondsRealtime(a);
        attack = true;

    }


    private void Move()
    {
        StartCoroutine(RotateTowards(newLocation));
        transform.position = Vector3.MoveTowards(transform.position, newLocation, enemy.patrollingSpeed * Time.deltaTime);
        if (transform.position == newLocation)
        {
            if (enemy.isSleepingEnemy == true)
            {
                if (sleepingFeedback == true) { SleepingFeedback(); }
            }
            else
            {
                if (idleFeedback == true) { IdleFeedback(); }
                SetNewLocation(a);
                StartCoroutine(RandomWait());
            }
        }
    }

    void SetNewLocation(int i)
    {
        // Debug.Log(i);
        newLocation = new Vector3(enemy.location[i].x, transform.position.y, enemy.location[i].z);
        // target.position = newLocation;
        // Rotation(target);
        a++;
        if (a == enemy.location.Count)
        {
            a = 0;
        }

    }

    IEnumerator RandomWait()
    {
        isWaiting = true;
        yield return new WaitForSecondsRealtime(Random.Range(1, 3));
        isWaiting = false;
    }

    void Hold()
    {
        enemyController.DisableProcedural();
        playerEnemy.OnHold(this.transform, enemy.target);
    }
    IEnumerator OnHold()
    {
        yield return new WaitForSecondsRealtime(1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyDeath"))
        {
            enemyDeath.Kill();
        }
    }


    private bool IsInRange(float range)
    {
        if(Vector3.Distance(player.gameObject.transform.position, groundPosition) < range)
        {
            return true;
        }
        return false;
    }

    private bool wasSleeping;

    void MovingFeedback()
    {
        movingFeedback = false;
        sleepingFeedback = true;
        idleFeedback = true;
        holdingFeedback = true;
        attackFeedback = true;
        if (enemy.anim.GetBool("sleeping") == true)
        {
            AwakeningFeeback();
            wasSleeping = true;
        }
        enemy.anim.SetBool("holding", false);
        enemy.anim.SetBool("moving", true);
        enemy.anim.SetBool("sleeping", false);

        if (!wasSleeping)
        {
            StartCoroutine(ReactivateWithDelay(.6f));

        }


        //enemyController.EnableProcedural();
    }
    void AwakeningFeeback()
    {
        StartCoroutine(ReactivateWithDelay(1.4f));
        
    }


    //IEnumerator AttackingFeedback()
    //{
    //    yield return new WaitForEndOfFrame();
    //}

    void HoldingFeedback()
    {
        holdingFeedback = false;
        movingFeedback = true;

        enemyController.DisableProcedural();
        enemy.anim.SetBool("moving", false);
        enemy.anim.SetBool("holding", true);
        camRig.isHolded = true;

    }
    void SleepingFeedback()
    {
        sleepingFeedback = false;
        movingFeedback = false;

        enemyController.DisableProcedural();

        enemy.anim.SetBool("sleeping", true);
        enemy.anim.SetBool("moving", false);

    }

    void IdleFeedback()
    {
        idleFeedback = false;
        movingFeedback = true;
        attackFeedback = true;

        enemy.anim.SetBool("moving", false);
        enemy.anim.SetBool("sleeping", false);

    }
    public void KillFeedback()
    {
        enemyController.DisableProcedural();
        enemy.anim.SetTrigger("death");
    }

    public void DamageFeedback()
    {

        enemyController.DisableProcedural();

        enemy.anim.SetTrigger("damage");
        stunnedCooldown = 1.4f;
        StartCoroutine(ReactivateWithDelay(1.4f));
    }

    public IEnumerator ReactivateWithDelay(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        enemyController.EnableProcedural();
        wasSleeping = false;
    }

}
