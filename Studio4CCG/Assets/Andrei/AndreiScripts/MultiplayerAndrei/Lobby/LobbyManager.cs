using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] LobbyPlayerData[] lobbyPlayerData;
    [SerializeField] LobbyUI lobbyUI;
    int currentLobbyPlayerIndex = -1;

    void Start()
    {
        ClientNetworkManager.Instance.LobbyInfoReceivedEvent += OnLobbyInfoReceived;
        ClientNetworkManager.Instance.SceneLoadEvent += OnSceneLoad;

        lobbyUI.ReadyButton.onClick.AddListener(ReadyButtonPressed);
        lobbyUI.StartGameButton.onClick.AddListener(PlayButtonPressed);

        ClientNetworkManager.Instance.SendPacket(
            new PlayerJoinedLobbyPacket(PlayerInformation.Instance.PlayerData).Serialize());
    }

    void Update()
    {

    }

    void ReadyButtonPressed()
    {
        LobbyPlayerData currentPlayer = lobbyPlayerData[currentLobbyPlayerIndex];
        currentPlayer.IsPlayerReady = currentPlayer.IsPlayerReady ? false : true;

        ClientNetworkManager.Instance.SendPacket(new PlayerReadyStatusPacket(
            PlayerInformation.Instance.PlayerData,
            currentPlayer.IsPlayerReady).Serialize());
    }

    void PlayButtonPressed()
    {
        LobbyPlayerData currentPlayer = lobbyPlayerData[currentLobbyPlayerIndex];

        if(currentPlayer.IsPlayerReady)
        {
            ClientNetworkManager.Instance.SendPacket(new StartGamePacket(PlayerInformation.Instance.PlayerData).Serialize());
        }
    }

    void OnLobbyInfoReceived(LobbyInfoPacket lobbyInfoPacket)
    {
        for (int i = 0; i < lobbyInfoPacket.PlayersData.Length; i++)
        {
            lobbyPlayerData[i].PlayerData = lobbyInfoPacket.PlayersData[i];
            lobbyPlayerData[i].IsPlayerReady = lobbyInfoPacket.PlayersReady[i];

            if (lobbyPlayerData[i].PlayerData.Name == PlayerInformation.Instance.PlayerData.Name)
                currentLobbyPlayerIndex = i;

            lobbyUI.UpdateUI(lobbyInfoPacket.PlayersData, lobbyInfoPacket.PlayersReady);
        }
    }

    void OnSceneLoad(SceneLoadPacket lobbySceneLoadPacket)
    {
        SceneManager.LoadScene(lobbySceneLoadPacket.SceneName);
    }
}


