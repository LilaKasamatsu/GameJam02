using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerInteraction : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] List<Vector3> location = new List<Vector3>();
    [SerializeField] bool sleeping;
    [SerializeField] float patroulingSpeed;
    [SerializeField] float Attackspeed;
    [SerializeField] bool sleepingEnemy;
    public bool attack;
    [SerializeField] bool holdingPlayer;

    Vector3 currLocation;
    Vector3 newLocation;
    int a;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNewLocation(0);
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(attack);
        if (holdingPlayer == false)
        {


            if (sleeping)
            {
                Sleeping();
            }
            else
            {
                Moving();
            }
        }

    }
    void Moving()
    {
        if (attack == false)
        {
            if (sleepingEnemy == false)
            {



                transform.position = Vector3.MoveTowards(transform.position, newLocation, patroulingSpeed);

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
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, patroulingSpeed);
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
        //run towards player
    }
    void SearchForPlayer()
    {

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
            //stop
            holdingPlayer = true;
        }
    }

}
