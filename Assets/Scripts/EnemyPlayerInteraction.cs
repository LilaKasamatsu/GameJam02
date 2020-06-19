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

    EnemyDeath enemyDeath;
    Transform player;

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
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, enemy.actionRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if (isHoldingPlayer == false)
        {
            isAttacking = IsInRange();
            if (isAttacking == true)
            {
                Attack();
            }
            else if(isWaiting == false)
            {
                Move();
            }
        }
        else
        {
            Hold();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, newLocation, patrollingSpeed * Time.deltaTime);

        if (transform.position == newLocation)
        {
            SetNewLocation(a);
            Sleeping();
        }
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


    void SetNewLocation(int i)
    {
        // Debug.Log(i);
        newLocation = location[i];
        a++;
        if (a == location.Count)
        {
            a = 0;
        }
        //isWaiting = true;

    }
    void Sleeping()
    {
        StartCoroutine(RandomSleep());
    }
 
    void Attack()
    {
       // Debug.Log("ATTACKING");
        Vector3 playerPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, attackspeed * Time.deltaTime);
    }

    void Hold()
    {
        //  Debug.Log("HOLD");
        StartCoroutine(HoldPlayer());
        transform.position = transform.position;
        //isAttacking = false;
    }
    IEnumerator HoldPlayer()
    {
        yield return new WaitForSecondsRealtime(1f);
      //  Debug.Log("killing now");
    }

    IEnumerator RandomSleep()
    {
        isWaiting = true;
        yield return new WaitForSecondsRealtime(Random.Range(1, 3));
        isWaiting = false;
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
            isHoldingPlayer = true;
        }
        //if (other.CompareTag("Environment"))
        //{
           
        //}

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

    bool IsInRange()
    {
        return Vector3.Distance(player.position, transform.position) < enemy.actionRadius;
    }
    //public void EnemyDeath()
    //{

    //}
}
