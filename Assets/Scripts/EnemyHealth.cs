using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{


    EnemyDeath enemyDeath;
    EnemyData enemy;
    EnemyPlayerInteraction enemyPlayerInteraction;

    //[SerializeField] GameObject visualRadius;
    private SpriteRenderer colorRadius;
   // [SerializeField] Color aliveColor;
   //[SerializeField] Color deadColor;
    float a;
    float b = 0.33f;

    // Start is called before the first frame update
    void Start()
    {
        enemyPlayerInteraction = GetComponent<EnemyPlayerInteraction>();
        enemyDeath = GetComponent<EnemyDeath>();
        //colorRadius = visualRadius.GetComponent<SpriteRenderer>();
        enemy = GetComponent<Enemy>().data;
       //colorRadius.color = aliveColor;
        enemy.currHealth = enemy.maxHealth;
       // Debug.Log(enemy);


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(enemy.currHealth);
        //Debug.Log(enemy.maxHealth);
        if (Input.GetKeyDown(KeyCode.E))
        {
            MinusHealth(1);
            enemyPlayerInteraction.DamageFeedback();
            //            Debug.Log(enemy.currHealth);
        }
    }
    public void MinusHealth(int health)
    {
        enemy.currHealth -= health;


        if (enemy.currHealth <= 0)
        {
           // Debug.Log(enemy.currHealth);
            enemyDeath.Kill();
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
