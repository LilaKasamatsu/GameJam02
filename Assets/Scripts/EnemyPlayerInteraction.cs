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

    [SerializeField] float patrollingSpeed;
    [SerializeField] float attackspeed;

    [SerializeField] EnemyData enemy;
    
    Vector3 currLocation;
    Vector3 newLocation;
    int a;

    public float attackCooldown = 0f;

    EnemyDeath enemyDeath;
    Transform player;

    Vector3 groundPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNewLocation(0);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyDeath = GetComponent<EnemyDeath>();
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
    void Update()
    {
        if(attackCooldown <= 0f)
        {
            groundPosition = new Vector3(transform.position.x, 0, transform.position.z);
            if (isHoldingPlayer == false)
            {
                isAttacking = IsInRange(enemy.actionRadius);
                if (isAttacking == true)
                {
                    Attack();
                }
                else if (isWaiting == false)
                {
                    Move();
                }
            }
            else
            {
                isHoldingPlayer = IsInRange(enemy.attackRadius);
                if (isHoldingPlayer)
                {
                    Hold();
                }

            }
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
        
    }

    void Attack()
    {
        Vector3 playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, attackspeed * Time.deltaTime);
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, newLocation, patrollingSpeed * Time.deltaTime);

        if (transform.position == newLocation)
        {
            SetNewLocation(a);
            StartCoroutine(RandomWait());
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

    //void Moving()
    //{
    //    //Debug.Log("moving");
    //    if (isWaiting == false)
    //    {
    //        //Debug.Log("movingggg");
    //        transform.position = Vector3.MoveTowards(transform.position, newLocation, patrollingSpeed * Time.deltaTime);

    //        if (isSleepingEnemy == false)
    //        {
    //            transform.position = Vector3.MoveTowards(transform.position, newLocation, patrollingSpeed * Time.deltaTime);

    //            if (transform.position == newLocation)
    //            {
    //                // Debug.Log("ssss");
    //                SetNewLocation(a);
    //            }
    //        }
    //        else
    //        {
    //            // joa darüber müssen wa nochma schnacken
    //        }
    //    }
    //    else
    //    {
    //        Sleeping();
    //    }
    //}


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
        return Vector3.Distance(player.position, groundPosition) < range;
    }
}
