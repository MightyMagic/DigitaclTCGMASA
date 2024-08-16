using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyData : MonoBehaviour
{
    public PlayerData[] PlayersData = new PlayerData[2];
    public bool[] PlayersReady = new bool[2];

    private void Awake()
    {
        PlayersData = new PlayerData[2];

        for (int i = 0; i < PlayersData.Length; i++)
            PlayersData[i] = new PlayerData("", "");

        PlayersReady = new bool[2];
    }

    public bool TryAddPlayerToLobby(PlayerData playerData)
    {
        bool playerAdded = false;

        for (int i = 0; i < PlayersData.Length; i++)
        {
            if (PlayersData[i].Name == "")
            {
                PlayersData[i] = playerData;
                playerAdded = true;
                break;
            }
        }

        return playerAdded;
    }
}
