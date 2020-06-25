using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDissolveTest : MonoBehaviour
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
            //time += Time.deltaTime * speed;

            //trancparency = Mathf.Lerp(0, 1, time);

            StartCoroutine(changeValueOverTime(0, 1, 3));
            //gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_dissolve", val));
                
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
