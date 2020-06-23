using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{


    EnemyDeath enemyDeath;
    EnemyData enemy;

    [SerializeField] GameObject visualRadius;
    private Color colorRadius;
    [SerializeField] Color aliveColor;
    [SerializeField] Color deadColor;
    Color currColor;
    int a = 100;

    // Start is called before the first frame update
    void Start()
    {
        enemyDeath = GetComponent<EnemyDeath>();
        colorRadius = visualRadius.GetComponent<SpriteRenderer>().color;
        colorRadius = aliveColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void MinusHealth(int health)
    {
        enemy.currHealth -= enemy.currHealth;
        colorRadius = Color.Lerp(aliveColor, deadColor, 0.33f);

        if (enemy.currHealth <= 0)
        {
            Debug.Log("enemy Death");
            enemyDeath.Kill();
        }
    }
}
