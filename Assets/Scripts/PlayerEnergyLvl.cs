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
        player.energyLVL = 0;
        cursor.SetCursorPower();
    }

    public void AddEnergy(float add)
    {
        if (player.energyLVL <= player.maxEnergyLVL)
        {
            player.energyLVL += add;
           // Debug.Log("add");
        }
        else
        {
            player.energyLVL = player.maxEnergyLVL;
        }
        cursor.SetCursorPower();
    }
    public void MinusEnergy()
    {
        player.energyLVL -= player.energyLVLforSpecialAttack;
    }
}
