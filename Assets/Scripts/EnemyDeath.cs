using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{

    // Start is called before the first frame update
    public void Kill()
    {
        StartCoroutine(Enemydying());
        Debug.Log("kill");
    }

    IEnumerator Enemydying()
    {
        yield return new WaitForSecondsRealtime(.5f);
        Destroy(gameObject);
        gameObject.SetActive(false);

    }
}
