using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LobbyUI : MonoBehaviour
{
    [SerializeField] PlayerUI[] playersUI;
    public Button ReadyButton;
    public Button StartGameButton;

    void Start()
    {
    }

    void Update()
    {

    }

    public void UpdateUI(PlayerData[] playersData, bool[] playersReady)
    {
        for (int i = 0; i < playersUI.Length; i++)
        {
            playersUI[i].SetPlayerName(playersData[i].Name);
            playersUI[i].ChangePlayerReadyStatus(playersReady[i]);
        }
    }

    public void UpdateUI(List<LobbyPlayerData> lobbyPlayerData)
    {
        for (int i = 0; i < playersUI.Length; i++)
        {
            playersUI[i].SetPlayerName(lobbyPlayerData[i].PlayerData.Name);
            playersUI[i].ChangePlayerReadyStatus(lobbyPlayerData[i].IsPlayerReady);
        }
    }
}
