using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public PlayerData PlayerData { get; private set; }
    public static PlayerInformation Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPlayerName(string name)
    {
        PlayerData = new PlayerData(Guid.NewGuid().ToString(), name);
    }
}
