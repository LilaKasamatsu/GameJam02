using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorEnergyLVL : MonoBehaviour
{
    [SerializeField] Player player;

    Renderer rend;
  //  float maxEnergyLVL;
  //  float curEnergyLVL;
     float currPower;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        SetCursorPower();

    }

    // Update is called once per frame

    public void SetCursorPower()
    {
        // Debug.Log(rend.material.GetFloat("CursorPower"));
        float b = player.data.energyLVL / 100;
      // Debug.Log(b);
        rend.material.SetFloat("CursorPower", player.data.energyLVL / 100);
    }
}
