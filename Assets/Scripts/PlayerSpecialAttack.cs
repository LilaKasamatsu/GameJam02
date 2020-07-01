using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttack : MonoBehaviour
{
    PlayerData player;
    // Vector3 scaleOG;
    [SerializeField] PlayerEnemyInteraction playerEnemy;
    SphereCollider coll;


    private void Start()
    {
        //  scaleOG = transform.localScale;
        transform.localScale = Vector3.zero;
        player = GetComponentInParent<Player>().data;
        coll = GetComponent<SphereCollider>();
        coll.enabled = false;
    }


    float duration = 1f;
    float durationInvert = 0.5f;

    public IEnumerator Expand()
    {

        coll.enabled = true;
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
           // Debug.Log("4");
            yield return null;

        }
        StartCoroutine(Invert());
        //sphere.localScale = scaleOG;
    }
    IEnumerator Invert()
    {
        // yield return new WaitForSeconds(0.1f);
        float t = 0f;
        Transform sphere = transform;
        Vector3 specialAttackVector = Vector3.zero;


        while (sphere.localScale.x > specialAttackVector.x)
        {
            sphere.localScale = Vector3.Lerp(sphere.localScale, specialAttackVector, t);
            // Debug.Log("1");
            if (t < 1)
            {
               
                t += Time.deltaTime / durationInvert;
            }
            // sphere.localScale = specialAttackVector;
            StartCoroutine(SetToFalse());
            coll.enabled = false;
            yield return null;
        }

    }

    IEnumerator SetToFalse()
    {
       // Debug.Log("t");
        yield return new WaitForSecondsRealtime(1f);
        playerEnemy.can2Attack = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
          // Debug.Log("urp special attack");
            other.GetComponent<EnemyHealth>().MinusHealth(3);
        }
    }
}
