using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttack : MonoBehaviour
{
    PlayerData player;

    private void Start()
    {
        transform.localScale = Vector3.one;
        player = GetComponentInParent<Player>().data;
    }


    float duration = .5f;

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
            yield return null;
        }
        sphere.localScale = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().MinusHealth(3);
        }
    }
}
