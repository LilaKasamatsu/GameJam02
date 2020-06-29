using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorEnergyLVL : MonoBehaviour
{
    PlayerData player;
    Renderer rend;
  //  float maxEnergyLVL;
  //  float curEnergyLVL;
     float currPower;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().data;
        SetCurcorPower();

    }

    // Update is called once per frame

    public void SetCurcorPower()
    {
        // Debug.Log(rend.material.GetFloat("CursorPower"));
        float b = player.energyLVL / 100;
      // Debug.Log(b);
        rend.material.SetFloat("CursorPower", player.energyLVL / 100);
    }
}
