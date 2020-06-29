using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerInteraction : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] List<Vector3> location = new List<Vector3>();
    bool isWaiting;
    [SerializeField] bool isSleepingEnemy;
    bool isHoldingPlayer;
    public bool isAttacking = false;

    private bool attackingFeedback;
    private bool movingFeedback = true;
    private bool holdingFeedback;
    private bool idleFeedback;
    private bool sleepingFeedback;

    private bool attack;

    [SerializeField] float patrollingSpeed;
    [SerializeField] float attackspeed;

    EnemyData enemy;



    Vector3 currLocation;
    Vector3 newLocation;
    int a;

    public float stunnedCooldown = 0f;

    EnemyDeath enemyDeath;
    Player player;

    Vector3 groundPosition;

    public Transform target;
    [SerializeField] Animator anim;

    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNewLocation(0);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemyDeath = GetComponent<EnemyDeath>();

        enemy = GetComponent<Enemy>().data;

        enemyController = GetComponent<EnemyController>();
    }

    //private void OnDrawGizmos()
    //{
    //    groundPosition = new Vector3(transform.position.x, 0, transform.position.z);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(groundPosition, enemy.actionRadius);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(groundPosition, enemy.attackRadius);
    //}

    // Update is called once per frame
    void FixedUpdate()
    {

        if (stunnedCooldown <= 0f)
        {
            groundPosition = new Vector3(transform.position.x, 0, transform.position.z);
            if (isHoldingPlayer == false)
            {
                isAttacking = IsInRange(enemy.actionRadius);
                if (isAttacking == true)
                {
                    Attack();
                    //if (isSleepingEnemy == true)
                    //{
                    //    Debug.Log("iii");
                    //    movingFeedback = true;
                    //}
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
                isHoldingPlayer = IsInRange(enemy.attackRadius);
                isHoldingPlayer = !player.GetComponent<PlayerEnemyInteraction>().IsAttackVelocity();
                if (isHoldingPlayer)
                {
                    Hold();
                    if (holdingFeedback == true) { HoldingFeedback(); }
                }
            }
        }
        else
        {
            stunnedCooldown -= Time.deltaTime;
        }
    }
    void Rotation(Vector3 target)
    {
        //transform.LookAt(player.transform);
        Debug.Log("r");
        //StartCoroutine(RotateTowards(target));
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));

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
        Rotation(player.transform.position);
        if (attack == true)
        {

           
            Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, playerPos, attackspeed * Time.deltaTime);
        }
        else
        {
           // Rotation(player.transform);
            StartCoroutine(WaitBeforeAttack());
        }
    }
    IEnumerator WaitBeforeAttack()
    {
        float a;
        if (isSleepingEnemy == true)
        {
            Debug.Log("iiii");
            a = 1.6f;
            movingFeedback = true;
        }
        else
        {
            a = 0.4f;
        }
        yield return new WaitForSeconds(a);
        attack = true;

    }

    private void Move()
    {
        StartCoroutine(RotateTowards(newLocation));
        transform.position = Vector3.MoveTowards(transform.position, newLocation, patrollingSpeed * Time.deltaTime);
        if (transform.position == newLocation)
        {
            if (isSleepingEnemy == true)
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
        newLocation = new Vector3(location[i].x, transform.position.y, location[i].z);
       // target.position = newLocation;
       // Rotation(target);
        a++;
        if (a == location.Count)
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
      //  Rotation(player.transform);
        player.GetComponent<PlayerEnemyInteraction>().OnHold(this.transform, target);
        StartCoroutine(OnHold());

    }
    IEnumerator OnHold()
    {
        yield return new WaitForSecondsRealtime(1f);
       // transform.position = transform.position;
        Debug.Log("qqq");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isHoldingPlayer = true;
            // Debug.Log("CAUGHT");
        }
        if (other.CompareTag("EnemyDeath"))
        {
            // Debug.Log("killlll");
            enemyDeath.Kill();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //stop
            isHoldingPlayer = false;
            // Debug.Log("draussen");
        }
    }

    bool IsInRange(float range)
    {
        return Vector3.Distance(player.transform.position, groundPosition) < range;
    }

    IEnumerator ReenableAnimation()
    {
        yield return new WaitForSecondsRealtime(.6f);
        enemyController.EnableProcedural();

    }


    void MovingFeedback()
    {
        movingFeedback = false;
       // Debug.Log("moving");
        sleepingFeedback = true;
        idleFeedback = true;
        holdingFeedback = true;

        anim.SetBool("holding", false);
        anim.SetBool("moving", true);
        anim.SetBool("sleeping", false);
        StartCoroutine(ReenableAnimation());
    }
    void HoldingFeedback()
    {
        
        holdingFeedback = false;
      //  Debug.Log("holding");
        movingFeedback = true;

        enemyController.DisableProcedural();
        anim.SetBool("moving", false);
        anim.SetBool("holding", true);

    }
    void SleepingFeedback()
    {
       // Debug.Log("sleeping");

        sleepingFeedback = false;
        movingFeedback = false;

        enemyController.DisableProcedural();

        anim.SetBool("sleeping", true);
        anim.SetBool("moving", false);

    }
    void IdleFeedback()
    {
        idleFeedback = false;
       // Debug.Log("idle");
        //  movingFeedback = true;


        anim.SetBool("moving", false);
        anim.SetBool("sleeping", false);


    }
    public void KillFeedback()
    {
        //anim.SetTrigger("death");
    }
}
