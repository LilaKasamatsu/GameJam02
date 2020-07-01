using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public bool isDying = false;

    EnemyData enemy;

    EnemyPlayerInteraction enemyPlayer;

    PlayerEnergyLvl playerEnergy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>().data;
        enemyPlayer = GetComponent<EnemyPlayerInteraction>();

        playerEnergy = enemy.player.GetComponent<PlayerEnergyLvl>();
    }
    // Start is called before the first frame update
    public void Kill()
    {
        enemyPlayer.death = true;
        StartCoroutine(Enemydying());
        isDying = true;
        Debug.Log("kill");
        playerEnergy.AddEnergy(enemy.player.data.energyGainedByKillingEnemy);
        //play
    }

    IEnumerator Enemydying()
    {
        yield return new WaitForSecondsRealtime(4f);
     //   gameObject.SetActive(false);
        gameObject.SetActive(false);

    }
}
