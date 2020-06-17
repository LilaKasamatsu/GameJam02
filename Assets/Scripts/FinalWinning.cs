using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWinning : MonoBehaviour
{
    // Start is called before the first frame update
    public List<bool> checkpoints = new List<bool>();
    [SerializeField] GameObject treasureObject;
    int currcount = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void CheckIfAllTrue()
    {
        for (int i = 0; i < checkpoints.Count; i++)
        {
            if (checkpoints[i] == false)
            {
                break;
            }
            else
            {
                //WIN!!!
            }
        }

    }
    public void SetToTrue()
    {
        checkpoints[currcount] = true;
        currcount++;
        CheckIfAllTrue();
    }
}
