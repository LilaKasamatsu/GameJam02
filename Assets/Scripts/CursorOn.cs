using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorOn : MonoBehaviour
{
    [SerializeField] GameObject cursor;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        cursor.transform.position = Input.mousePosition;
    }
}
