using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightManager : MonoBehaviour
{
    public List<Light> licht = new List<Light>();
    public List<float> lichtMaxWert = new List<float>();
    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < licht.Count; i++)
        {
            lichtMaxWert.Add(licht[i].intensity);
        }
        //Debug.Log(lichtMaxWert[0]);
        //Debug.Log(lichtMaxWert[1]);
        //Debug.Log(lichtMaxWert[2]);
        //Debug.Log(lichtMaxWert[3]);

    }
}
