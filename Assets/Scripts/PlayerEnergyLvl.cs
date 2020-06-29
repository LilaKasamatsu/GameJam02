using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyLvl : MonoBehaviour
{
    PlayerData player;
    CursorEnergyLVL cursor;

    void Start()
    {
        player = GetComponent<Player>().data;
        cursor = GameObject.Find("Cursor").GetComponent<CursorEnergyLVL>();
    }

    public void AddEnergy(float add)
    {
        if (player.energyLVL <= player.maxEnergyLVL)
        {
            player.energyLVL += add;
        }
        else
        {
            player.energyLVL = player.maxEnergyLVL;
        }
        cursor.SetCurcorPower();
    }
}
