using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    private static EnvironmentManager instance = null;

    // Game Instance Singleton
    public static EnvironmentManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public PlayerEnergyLvl playerEnergy;
    public PlayerHealth playerHealth;
    public Player player;
    public PlayerData playerData;




    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerEnergy = FindObjectOfType<PlayerEnergyLvl>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        playerData = player.data;
    }


}
