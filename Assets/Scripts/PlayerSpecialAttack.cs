using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttack : MonoBehaviour
{
    AudioManager audioManager;

    PlayerData player;
    // Vector3 scaleOG;
    [SerializeField] PlayerEnemyInteraction playerEnemy;
    SphereCollider coll;
    Transform OG;
    bool isMaking = false;


    private void Start()
    {
        audioManager = AudioManager.instance;
        //  scaleOG = transform.localScale;
        transform.localScale = Vector3.zero;
        player = GetComponentInParent<Player>().data;
        coll = GetComponent<SphereCollider>();
        coll.enabled = false;
        OG = this.transform;
    }


    float duration = 1f;
    float durationInvert = 0.5f;

    public IEnumerator Expand()
    {
        if (isMaking == false)
        {
            audioManager.PlaySound("Special Attack");
            isMaking = true;
            coll.enabled = true;
            float t = 0f;
            Transform sphere = this.transform;
            Vector3 specialAttackVector = new Vector3(player.specialAttackRadius, 18, player.specialAttackRadius);

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
            sphere.localScale = specialAttackVector;
            StartCoroutine(Invert());
        }
        //sphere.localScale = scaleOG;
    }
    IEnumerator Invert()
    {
        // yield return new WaitForSeconds(0.1f);
        float t = 0f;
        Transform sphere = transform;
        Vector3 specialAttackVector = new Vector3(0.3f, 0.3f, 0.3f);


        while (sphere.localScale.x > specialAttackVector.x)
        {
            sphere.localScale = Vector3.Lerp(sphere.localScale, specialAttackVector, t);
            if (t < 1)
            {

                t += Time.deltaTime / durationInvert;
            }
            // sphere.localScale = specialAttackVector;
            StartCoroutine(SetToFalse());
            coll.enabled = false;
            sphere = OG;
           // Debug.Log("1");
            isMaking = false;
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
