﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttack : MonoBehaviour
{
    PlayerData player;
    // Vector3 scaleOG;
    PlayerEnemyInteraction playerEnemy;


    private void Start()
    {
        //  scaleOG = transform.localScale;
        transform.localScale = Vector3.one;
        player = GetComponentInParent<Player>().data;
        playerEnemy = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnemyInteraction>();
    }


    float duration = 2f;
    float durationInvert = 0.5f;

    public IEnumerator Expand()
    {
        float t = 0f;
        Transform sphere = this.transform;
        Vector3 specialAttackVector = new Vector3(player.specialAttackRadius, player.specialAttackRadius, player.specialAttackRadius);

        while (sphere.localScale.x < specialAttackVector.x)
        {
            sphere.localScale = Vector3.Lerp(sphere.localScale, specialAttackVector, t);
            if (t < 1)
            {
                t += Time.deltaTime / duration;
            }
            Debug.Log("4");
            yield return null;

        }
        StartCoroutine(Invert());
        Debug.Log("5");
        //sphere.localScale = scaleOG;
    }
    IEnumerator Invert()
    {
        yield return new WaitForSeconds(0.1f);
        float t = 0f;
        Transform sphere = transform;
        Vector3 specialAttackVector = new Vector3(0, 0, 0);

        while (sphere.localScale.x > specialAttackVector.x)
        {
            sphere.localScale = Vector3.Lerp(sphere.localScale, specialAttackVector, t);
            Debug.Log("1");
            if (t < 1)
            {
                Debug.Log("2");
                t += Time.deltaTime / durationInvert;
            }
            sphere.localScale = specialAttackVector;
            playerEnemy.can2Attack = true;
            yield return null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().MinusHealth(3);
        }
    }
}
