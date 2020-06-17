using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerData player;
    
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x < 0.1f && rb.velocity.y < 0.1f)
        {
            player.deathTimer += Time.deltaTime;
            if(player.deathTimer >= player.deathTime)
            {
                //GAMEOVER
            }
        }
        else
        {
            player.deathTimer = 0;
        }    
    }
}
