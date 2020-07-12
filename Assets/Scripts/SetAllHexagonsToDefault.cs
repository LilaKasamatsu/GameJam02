using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAllHexagonsToDefault : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        PlayerRespwan();
    //    }
    //}
   public void PlayerRespwan()
    {
        foreach (Transform child in transform)
        {
            Debug.Log("1");
            Debug.Log(child.name);
            //    child.gameObject.SetActive(false);
            //    child.gameObject.SetActive(true);
            child.gameObject.GetComponent<Environment>().OnRespwan();


        }
    }
}
