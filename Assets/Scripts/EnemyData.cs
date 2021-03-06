﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    //public List<Vector3> location = new List<Vector3>();

    

    [Header("EnemyInteraction")]
    public List<Vector3> location;

    public bool isSleepingEnemy;

    public float patrollingSpeed;
    public float attackSpeed;

    public Transform target;
    public Animator anim;

    //public bool isSleeping;
    //public bool isWaiting;
    //public bool isFollowing;
    //public bool isHoldingPlayer;
    //[Space(10)]
    //public float patrollingSpeed;
    //public float attackSpeed;
    public float actionRadius;
    public float attackRadius;
    public int maxHealth = 3;
    public int currHealth;
    //public EnemyDeath enemyDeath;



    [Header("Player")]
    public Player player;

    public PlayerEnemyInteraction playerEnemy;


}
