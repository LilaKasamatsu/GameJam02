using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyLvl : MonoBehaviour
{
    PlayerData player;

    void Start()
    {
        player = GetComponent<Player>().data;
    }

    public void AddEnergy(int add)
    {
        if (player.energyLVL <= player.maxEnergyLVL)
        {
            player.energyLVL += add;
        }
        else
        {
            player.energyLVL = player.maxEnergyLVL;
        }
    }
}
