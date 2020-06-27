using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour
{
    GameObject player;

    PlayerEnemyInteraction peiScript;
    EnemyHealth enemyHealth;
    EnemyDeath enemyDeath;

    ParticleSystem circleParticles;
    ParticleSystem verticalParticles;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        peiScript = player.transform.GetComponent<PlayerEnemyInteraction>();

        enemyHealth = transform.parent.GetComponent<EnemyHealth>();
        enemyDeath = transform.parent.GetComponent<EnemyDeath>();

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
