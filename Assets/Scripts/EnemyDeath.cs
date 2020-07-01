using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public bool isDying = false;

    EnemyData enemy;

    EnemyPlayerInteraction enemyPlayer;

    PlayerEnergyLvl playerEnergy;
    PlayerData playerData;
    PlayerEnemyInteraction playerENnemy;
    GameEnd gameEnd;
    [SerializeField] bool tutorialScene;

    private void Awake()
    {
        playerEnergy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyLvl>();
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().data;
        playerENnemy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnemyInteraction>();
        enemy = GetComponent<Enemy>().data;
        enemyPlayer = GetComponent<EnemyPlayerInteraction>();
        if (GameObject.Find("Enemies"))
        {
            gameEnd = GameObject.Find("Enemies").GetComponent<GameEnd>();
        }
        
    }
    // Start is called before the first frame update
    public void Kill()
    {
        enemyPlayer.death = true;
        StartCoroutine(Enemydying());
        isDying = true;
      //  Debug.Log("kill");
        if (tutorialScene == false)
        {
            gameEnd.Delete();
        }
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
