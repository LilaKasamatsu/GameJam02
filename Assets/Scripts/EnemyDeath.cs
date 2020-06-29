using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public bool isDying = false;
    PlayerEnergyLvl playerEnergy;
    PlayerData playerData;
    private void Start()
    {
        
        playerEnergy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyLvl>();
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().data;
    }
    // Start is called before the first frame update
    public void Kill()
    {
        StartCoroutine(Enemydying());
        isDying = true;
        Debug.Log("kill");
        playerEnergy.AddEnergy(playerData.energyGainedByKillingEnemy);
        //play
    }

    IEnumerator Enemydying()
    {
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
        gameObject.SetActive(false);

    }
}
