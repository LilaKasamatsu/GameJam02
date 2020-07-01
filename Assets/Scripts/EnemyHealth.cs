using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
   [SerializeField] Transform enemyChild;
    
    EnemyData enemy;
    EnemyPlayerInteraction enemyPlayerInteraction;
    EnemyDeath enemyDeath;
    
    EnemyDissolve enemyDissolve;
    EnemyRadius enemyRadius;

    private SpriteRenderer colorRadius;

    float a;
    float b = 0.33f;

    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<Enemy>().data;
        enemyPlayerInteraction = GetComponent<EnemyPlayerInteraction>();
        enemyDeath = GetComponent<EnemyDeath>();

        enemy.currHealth = enemy.maxHealth;    
    }

    // Update is called once per frame
    void Update()
    {
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        Debug.Log("urp e");
    //        MinusHealth(1);
    //        enemyPlayerInteraction.DamageFeedback();
    //        //            Debug.Log(enemy.currHealth);
    //    }
    }
    public void MinusHealth(int health)
    {
        enemy.currHealth -= health;

        if (enemy.currHealth == 2)
        {
            GameObject particleRadius = transform.Find("particleRadius").gameObject;
            enemyRadius = particleRadius.GetComponent<EnemyRadius>();
            enemyPlayerInteraction.DamageFeedback();
            enemyRadius.circleParticlesTwo.Stop();
        }

        if (enemy.currHealth == 1)
        {
            GameObject particleRadius = transform.Find("particleRadius").gameObject;
            enemyRadius = particleRadius.GetComponent<EnemyRadius>();
            enemyPlayerInteraction.DamageFeedback();
            enemyRadius.circleParticlesOne.Stop();
        }

        if (enemy.currHealth <= 0)
        {
            // Debug.Log(enemy.currHealth);
            enemyDeath.Kill();
            Debug.Log("urp");


          //  Transform enemyChild = transform.Find("enemy mit joints");

            foreach (Transform gc in enemyChild)
            {
                if(gc.name == "Enemy")
                {
                    GameObject enemyGrandchild = gc.gameObject;

                    enemyDissolve = enemyGrandchild.GetComponent<EnemyDissolve>();

                    enemyDissolve.dissolveNow();

                }
                
            }
            
        }

        /*
        else
        {

            StartCoroutine(MinusHealthCoroutine());

        }
        */

    }

    /*
    IEnumerator MinusHealthCoroutine()
    {
        while (true)
        {
           // Debug.Log("ee");
            colorRadius.color = Color.Lerp(aliveColor, deadColor, a);
            a += 0.01f;
            if (a >= b)
            {
              //  Debug.Log(a);
                b += 0.33f;
                break;

            }
            yield return new WaitForEndOfFrame();
        }
        yield return null;

    }
    */
}
