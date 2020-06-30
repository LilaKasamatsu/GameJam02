﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public bool isDying = false;
    PlayerEnergyLvl playerEnergy;
    PlayerData playerData;
    EnemyPlayerInteraction enemyPlayer;
    PlayerEnemyInteraction playerENnemy;
    private void Start()
    {
        
        playerEnergy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyLvl>();
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().data;
        playerENnemy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnemyInteraction>();
        enemyPlayer = GetComponent<EnemyPlayerInteraction>();
    }
    // Start is called before the first frame update
    public void Kill()
    {
        enemyPlayer.death = true;
        StartCoroutine(Enemydying());
        isDying = true;
        Debug.Log("kill");
        playerEnergy.AddEnergy(playerData.energyGainedByKillingEnemy);
        playerENnemy.OnRelease();
        //play
    }

    IEnumerator Enemydying()
    {
        yield return new WaitForSecondsRealtime(4f);
     //   gameObject.SetActive(false);
        gameObject.SetActive(false);

    }
}
