using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyLvl : MonoBehaviour
{
    public int energyLVL;
    [SerializeField] int maxEnergyLVL;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddEnergy(int add)
    {
        if (energyLVL <= maxEnergyLVL)
        {
            energyLVL += add;
        }
        else
        {
            energyLVL = maxEnergyLVL;
        }
    }
}
