using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [Header("Health Attributes")]
    public float deathTimer ;
    public float deathTime ;


    public Color32 standardColor;
    public Color32 attackColor;

    [Header("Enemy Interaction")]
    public bool isMovable = true;

    [Header("Energy")]
    public int energyLVL;
    public int maxEnergyLVL;
    public int energyLVLforSpecialAttack;
    public int energyGainedByKillingEnemy;

    [Header("Movement")]
    public float minVelocity;
    public float maxVelocity;

    public float minAttackVelocity;
    [HideInInspector] public bool isAttacking;

    public float specialAttackRadius;
   // public float breathRadius;

}
