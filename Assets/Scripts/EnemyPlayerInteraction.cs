using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerInteraction : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] List<Vector3> location = new List<Vector3>();
    [SerializeField] bool sleeping;
    [SerializeField] float patroulingSpeed;
    [SerializeField] float attackspeed;
    [SerializeField] bool sleepingEnemy;
    public bool attack=false;
    [SerializeField] bool holdingPlayer;

    Vector3 currLocation;
    Vector3 newLocation;
    int a;

    GameObject player;
    EnemyDeath enemyDeath;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNewLocation(0);
        player = GameObject.FindGameObjectWithTag("Player");
        enemyDeath = GetComponent<EnemyDeath>();
        

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(attack);
        //Debug.Log(holdingPlayer);

        if (holdingPlayer==false)
        {
            Debug.Log("HOLDING FALSE");
           // attack = true;
            if (sleeping)
            {
                Sleeping();
            }
            else
            {
                Moving();
            }
        }
        else
        {
            Hold();
        }

    }
    void Moving()
    {
        Debug.Log("moving");
        if (attack == false)
        {
            if (sleepingEnemy == false)
            {

                transform.position = Vector3.MoveTowards(transform.position, newLocation, patroulingSpeed * Time.deltaTime);

                if (transform.position == newLocation)
                {
                    // Debug.Log("ssss");
                    SetNewLocation(a);
                }
            }
            else
            {
                // joa darüber müssen wa nochma schnacken
                

            }
        }
        else
        {
            Attack();
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
        sleeping = true;

        //}
    }
    void Sleeping()
    {
        StartCoroutine(RandomSleep());
    }

    void Attack()
    {
        //Debug.Log("attacking");
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, attackspeed * Time.deltaTime);
    }

    void Hold()
    {
        Debug.Log("HOLD");
        transform.position = transform.position;
        attack = false;
    }

    IEnumerator RandomSleep()
    {
        yield return new WaitForSecondsRealtime(Random.Range(1, 3));
        sleeping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            holdingPlayer = true;
            Debug.Log("innen");
        }
        if (other.CompareTag("EnemyDeath"))
        {
            Debug.Log("killlll");
            enemyDeath.Kill();
            holdingPlayer = true;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        attack = false;
        holdingPlayer = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //stop
            holdingPlayer = false;
            attack = true;
            Debug.Log("draussen");
        }
    }

}
