using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerInteraction : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] List<Vector3> location = new List<Vector3>();
    [SerializeField] bool isWaiting;
    [SerializeField] bool isSleepingEnemy;
    [SerializeField] bool isHoldingPlayer;
    public bool isAttacking = false;

    private bool attackingFeedback;
    private bool movingFeedback;
    private bool holdingFeedback;
    private bool idleFeedback;
    private bool sleepingFeedback;

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

    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNewLocation(0);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemyDeath = GetComponent<EnemyDeath>();

        enemy = GetComponent<Enemy>().data;
    }

    private void OnDrawGizmos()
    {
        groundPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundPosition, enemy.actionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundPosition, enemy.attackRadius);
    }

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
                    if (movingFeedback == true) { MovingFeedback(); }
                }
                else if (isWaiting == false)
                {
                    if (movingFeedback == true) { MovingFeedback(); }
                    Move();
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

    void Attack()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, attackspeed * Time.deltaTime);
    }

    private void Move()
    {
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
        newLocation = location[i];
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
        player.GetComponent<PlayerEnemyInteraction>().OnHold(this.transform);
        transform.position = transform.position;
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

    //void AttackingFeedback()
    //{
    //    attackingFeedback = true;
    //    movingFeedback = true;
    //    holdingFeedback = true;

    //    anim.SetBool("attacking", true);

    //}
    void MovingFeedback()
    {
        //movingFeedback = true;
        sleepingFeedback = true;
        idleFeedback = true;

        anim.SetBool("moving", true);
        anim.SetBool("attacking", false);
        anim.SetBool("sleeping", false);

    }
    void HoldingFeedback()
    {
        movingFeedback = true;
        anim.SetBool("moving", false);

    }
    void SleepingFeedback()
    {
        movingFeedback = true;

        anim.SetBool("moving", false);
    }
    void IdleFeedback()
    {
        movingFeedback = true;

        anim.SetBool("moving", false);
        anim.SetBool("sleep", false);
        anim.SetBool("attacking", false);

    }
    public void KillFeedback()
    {
        anim.SetTrigger("death");
    }
}
