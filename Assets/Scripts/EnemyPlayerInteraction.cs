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

    // private bool attackingFeedback;
    private bool movingFeedback = true;
    private bool holdingFeedback;
    private bool idleFeedback;
    private bool sleepingFeedback;
    private bool attackFeedback = true;
    public bool attack;

    [HideInInspector] public bool death;

    [SerializeField] float patrollingSpeed;
    [SerializeField] float attackspeed;

    EnemyData enemy;



    Vector3 currLocation;
    Vector3 newLocation;
    int a;

    public float stunnedCooldown = 0f;

    EnemyDeath enemyDeath;
    Player player;
    PlayerHealth playerHealth;
    PlayerEnergyLvl playerenergy;

    CameraRig camRig;

    Vector3 groundPosition;

    public Transform target;
    public Transform playerTarget;
    [SerializeField] Animator anim;

    private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        camRig = GameObject.Find("CameraRig").GetComponent<CameraRig>();
        rb = GetComponent<Rigidbody>();
        SetNewLocation(0);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerenergy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyLvl>();
        enemyDeath = GetComponent<EnemyDeath>();

        enemy = GetComponent<Enemy>().data;

        enemyController = GetComponent<EnemyController>();
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
                    if (holdingFeedback == true)
                    {
                        HoldingFeedback();
                    }
                }
                else
                {
                    stunnedCooldown = 2f;
                }
            }
        }
        else
        {
            stunnedCooldown -= Time.deltaTime;
        }
        if (death == true) { KillFeedback(); }
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
        if (attackFeedback == true)
        {
            playerHealth.ScreenBlink();
            //StartCoroutine(AttackingFeedback());
            attackFeedback = false;

        }
        Debug.Log("jetzt");
        float a;
        if (isSleepingEnemy == true)
        {
            // Debug.Log("iiii");
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
        player.GetComponent<PlayerEnemyInteraction>().OnHold(this.transform, target);
        StartCoroutine(OnHold());
    }
    IEnumerator OnHold()
    {
        yield return new WaitForSecondsRealtime(1f);
        // transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("HI");
            isHoldingPlayer = true;
        }
        if (other.CompareTag("EnemyDeath"))
        {
            enemyDeath.Kill();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //stop
            isHoldingPlayer = false;
        }
    }

    bool IsInRange(float range)
    {
        return Vector3.Distance(player.transform.position, groundPosition) < range;
    }



    void MovingFeedback()
    {
        movingFeedback = false;
        sleepingFeedback = true;
        idleFeedback = true;
        holdingFeedback = true;
        attackFeedback = true;


        anim.SetBool("holding", false);
        anim.SetBool("moving", true);
        anim.SetBool("sleeping", false);

        if (!isSleepingEnemy)
        {
            enemyController.EnableProcedural();
        }

    }

    void AwakingFeedback()
    {

        enemyController.DisableProcedural();
        anim.SetBool("sleeping", false);
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
        anim.SetBool("moving", false);
        anim.SetBool("holding", true);
        camRig.isHolded = true;

    }
    void SleepingFeedback()
    {
        sleepingFeedback = false;
        movingFeedback = false;

        enemyController.DisableProcedural();

        anim.SetBool("sleeping", true);
        anim.SetBool("moving", false);

    }

    void IdleFeedback()
    {
        idleFeedback = false;
        movingFeedback = true;
        attackFeedback = true;

        anim.SetBool("moving", false);
        anim.SetBool("sleeping", false);

    }
    public void KillFeedback()
    {
        isHoldingPlayer = false;
        enemyController.DisableProcedural();

        anim.SetTrigger("death");
        isWaiting = true;
        stunnedCooldown = 5f;
    }
    public void DamageFeedback()
    {
        playerenergy.AddEnergy(30);
        enemyController.DisableProcedural();

        anim.SetTrigger("damage");

        StartCoroutine(ReactivateWithDelay(1.4f));
    }

    IEnumerator ReactivateWithDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        enemyController.EnableProcedural();

    }

}
