using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour
{
    GameObject player;

    PlayerEnemyInteraction peiScript;
    EnemyHealth enemyHealth;
    EnemyDeath enemyDeath;
    EnemyData enemy;

    ParticleSystem circleParticles;
    ParticleSystem verticalParticles;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        peiScript = player.transform.GetComponent<PlayerEnemyInteraction>();

        enemyHealth = transform.parent.GetComponent<EnemyHealth>();
        enemyDeath = transform.parent.GetComponent<EnemyDeath>();
        enemy = transform.parent.GetComponent<Enemy>().data;

        circleParticles = transform.GetChild(0).GetComponentInChildren<ParticleSystem>();
        verticalParticles = transform.GetChild(2).GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyDeath.isDying == true)
        {
            circleParticles.Stop();
            verticalParticles.Stop();
        }
        
        if(enemy.currHealth >= 3)
        {
            var main = circleParticles.main;
            main.maxParticles = Mathf.RoundToInt(3);
        }

        if(enemy.currHealth == 2)
        {
            var main = circleParticles.main;
            main.maxParticles = Mathf.RoundToInt(2);
        }

        if(enemy.currHealth <= 1)
        {
            var main = circleParticles.main;
            main.maxParticles = Mathf.RoundToInt(1);
        }

        if(peiScript.enemyHit == true)
        {
            //hit feedback from particle system

            StartCoroutine(EnemyHit());
        }
    }

    IEnumerator EnemyHit()
    {
        yield return new WaitForSecondsRealtime(1f);
        peiScript.enemyHit = false;
    }
}
