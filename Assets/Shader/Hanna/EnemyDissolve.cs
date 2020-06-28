using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDissolve : MonoBehaviour
{
    [SerializeField] float time = 0f;
    //[SerializeField] float trancparency = 0f;
    [SerializeField] float speed = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Aufrufen bei Enemy Death in enemy health!  
            StartCoroutine(changeValueOverTime(0, 1, 3));
                
        }
        
    }

    IEnumerator changeValueOverTime(float fromVal, float toVal, float duration)
    {
        float counter = 0f;

        while (counter < duration)
        {
            if (Time.timeScale == 0)
                counter += Time.unscaledDeltaTime;
            else
                counter += Time.deltaTime;

            float val = Mathf.Lerp(fromVal, toVal, counter / duration);
            gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_dissolve", val);
            yield return null;
        }
    }
}
