using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float deathTimer = 0;
    public float deathTime = 3;
    public float attackRadius = 5;

    public Color32 standardColor;
    public Color32 attackColor;
    public Vector3 attackStartPos;


    public bool isMovable = true;

    public float breathRadius;

}
