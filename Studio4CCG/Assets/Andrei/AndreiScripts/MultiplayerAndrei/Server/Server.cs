using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
//using Networking;
//using Networking.Lobby; get back here
using System.Linq;
using Unity.VisualScripting;
using System.Collections;

public class Server : MonoBehaviour
{
    [SerializeField] ushort port;
    Socket serverSocket;
    List<Socket> clients;
    bool clientConnected = false;

    public static Server instance;
    GameObject instantiatedGameObject;

    LobbyData lobbyData;
    [SerializeField] PlayersStatesData playerStatesData;
    [SerializeField] CardDataBase cardDataBase;

    //Delete this as soon as possible, horrible thing
    int counterOfStateCalls = 0;
    public int counterOfDeckChoices = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        lobbyData = FindFirstObjectByType<LobbyData>();
        //playerStatesData = FindFirstObjectByType<PlayersStatesData>();

        PlayerInformation.Instance.SetPlayerName("SERVER");

        clients = new List<Socket>();

        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
        serverSocket.Listen(10);
        serverSocket.Blocking = false;
        Debug.LogError("[Server] Listening for clients to connect");
    }

    void Update()
    {
        try
        {
            clients.Add(serverSocket.Accept());
            clientConnected = true;
            Debug.LogError("[Server] Client connected!");
        }
        catch (SocketException ex)
        {
            if (ex.SocketErrorCode != SocketError.WouldBlock)
            {
                Debug.LogError($"[Server] {ex}");
            }
        }

        if (clientConnected)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Available > 0)
                {
                    try
                    {
                        byte[] buffer = new byte[clients[i].Available];
                        clients[i].Receive(buffer);

                        BasePacket bp = new BasePacket();
                        bp.Deserialize(buffer);

                        Debug.LogError($"[Server] Received a packet from the client");
                        Debug.LogError($"[Server] Packet Type is {bp.packetType}");
                        Debug.LogError($"[Server] Player ID of {bp.playerData.ID} and player name of {bp.playerData.Name}");

                        switch (bp.packetType)
                        {
                            case BasePacket.PacketType.PlayerJoinedLobby:
                                Debug.LogError($"[Server] Received a Player Joined Lobby Packet");

                                PlayerJoinedLobbyPacket pjlp = new PlayerJoinedLobbyPacket().Deserialize(buffer);
                                bool result = lobbyData.TryAddPlayerToLobby(pjlp.playerData);

                                if (result)
                                {
                                    SendPacketsToAllClients(new LobbyInfoPacket(
                                        PlayerInformation.Instance.PlayerData,
                                        lobbyData.PlayersData.ToArray(),
                                        lobbyData.PlayersReady.ToArray()).Serialize());
                                }
                                else
                                {
                                    //Send lobby full packet
                                }

                                break;

                            case BasePacket.PacketType.PlayerReadyStatus:
                                Debug.LogError($"[Server] Received Player Ready Status Packet");

                                PlayerReadyStatusPacket prsp = new PlayerReadyStatusPacket().Deserialize(buffer);

                                for (int j = 0; j < lobbyData.PlayersData.Length; j++)
                                {
                                    if (lobbyData.PlayersData[j].ID == prsp.playerData.ID)
                                    {
                                        lobbyData.PlayersReady[j] = prsp.IsPlayerRead;
                                        break;
                                    }
                                }

                               // SendPacketsToAllClients(new LobbyInfoPacket(
                               //     PlayerInformation.Instance.PlayerData,
                               //     lobbyPlayerData).Serialize());

                                SendPacketsToAllClients(new LobbyInfoPacket(
                                       PlayerInformation.Instance.PlayerData,
                                       lobbyData.PlayersData.ToArray(),
                                       lobbyData.PlayersReady.ToArray()).Serialize());

                                break;

                            /*case BasePacket.PacketType.Position:

                                PositionPacket pp = new PositionPacket().Deserialize(buffer);
                                Debug.LogWarning($"[Client] Position Packet Content is: {pp.Position}");
                                break;*/

                            case BasePacket.PacketType.Instantiate:

                                InstantiatePacket ip = new InstantiatePacket().Deserialize(buffer);
                                Debug.LogError($"[Server] Instantiate Packet Content is: {ip.PrefabName}");
                                Debug.LogError($"[Server] Instantiate Packet Content is: {ip.Position}");
                                Debug.LogError($"[Server] Instantiate Packet Content is: {ip.Rotation.eulerAngles}");

                                GameObject go = Utils.InstantiateFromNetwork(ip);
                                SendPacketsToAllOtherClients(i, buffer);

                                break;


                            case BasePacket.PacketType.Destroy:

                                DestroyPacket dp = new DestroyPacket().Deserialize(buffer);
                                Debug.LogError($"[Server] Destroy Packet Content is: {dp.GameObjectID}");
                                Utils.DestroyFromNetwork(dp);
                                SendPacketsToAllOtherClients(i, buffer);

                                break;

                            case BasePacket.PacketType.StartGame:
                                Debug.LogError($"[Server] Received Start Game Packet");
                                SendPacketsToAllClients(new SceneLoadPacket(PlayerInformation.Instance.PlayerData, "AndreiMainScene").Serialize());
                                break;

                            case BasePacket.PacketType.PlayerStateInfo:
                                Debug.LogError($"[Server] Received Player State Packet");
                                PlayerStateInfoPacket playerStateInfoPacket = new PlayerStateInfoPacket().Deserialize(buffer);
                                PlayerState playerState = new PlayerState(playerStateInfoPacket.playerState.currentHp,
                                    playerStateInfoPacket.playerState.currentMana,
                                    playerStateInfoPacket.playerState.playerName);
                                playerStatesData.ReceivedPlayerState(playerState);

                                // Basically fixes the issue of having a call overlap when the main game scene launches for the first time
                                counterOfStateCalls++;

                                if (counterOfStateCalls > 1)
                                {
                                    Debug.LogError($"[Server] Sending player states to all clients");

                                    PlayersStatesPacket psp = new PlayersStatesPacket(PlayerInformation.Instance.PlayerData,
                                        playerStatesData.playerStates);
                                    Debug.LogError($"[Server]Packet size is " + psp.Serialize().Length + " bytes");
                                    SendPacketsToAllClients(psp.Serialize());   
                                }

                                break;
                            case BasePacket.PacketType.DeckChoice:
                                DeckChoicePacket dcp = new DeckChoicePacket().Deserialize(buffer);
                                Debug.LogError($"[Server] Received the request for deck #" + dcp.deckIndex);
                                // Generate deck for the player
                                cardDataBase.PopulateDeck(dcp);
                                // Sending the first cards of the deck to corresponding players

                                counterOfDeckChoices++;

                                if (counterOfDeckChoices == 2) // this means both players choose deck, so we can start the game
                                {
                                    int playerIndex = Random.Range(0, 1);
                                    string playerName = lobbyData.PlayersData[playerIndex].Name;
                                    Debug.LogError($"[Server]Sending players turn packet: it's " + playerName + "'s turn!");
                                    //SendPacketsToAllClients(new PlayerTurnPacket(PlayerInformation.Instance.PlayerData, playerName).Serialize());
                                    StartCoroutine(SendWithDelay(1.0f, playerName));
                                }
                                break;

                            case BasePacket.PacketType.PlayerTurn:
                                Debug.LogWarning($"[Server] Is notified that need to switch turns");
                                PlayerTurnPacket ptp = new PlayerTurnPacket().Deserialize(buffer);
                                // Determining whose turn it was just now, and calling next player's turn
                                for(int k = 0; k < lobbyData.PlayersData.Length; k++)
                                {
                                    if (lobbyData.PlayersData[k].Name == ptp.playersTurnName)
                                    {
                                        switch(k)
                                        {
                                            case 0:
                                                SendPacketsToAllClients(new PlayerTurnPacket(PlayerInformation.Instance.PlayerData, lobbyData.PlayersData[1].Name).Serialize());
                                                break;
                                            case 1:
                                                SendPacketsToAllClients(new PlayerTurnPacket(PlayerInformation.Instance.PlayerData, lobbyData.PlayersData[0].Name).Serialize());
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    catch (SocketException ex)
                    {
                        if (ex.SocketErrorCode != SocketError.WouldBlock)
                        {
                            Debug.LogError($"[Server] {ex}");
                        }
                    }
                }
            }

        }
    }

    void SendPacketsToAllOtherClients(int currnetClientIndex, byte[] buffer)
    {
        for (int i = 0; i < clients.Count; i++)
        {
            if (i == currnetClientIndex)
                continue;

            clients[i].Send(buffer);
        }
    }

    public void SendPacketsToAllClients(byte[] buffer)
    {
        for (int i = 0; i < clients.Count; i++)
        {
            clients[i].Send(buffer);
        }
    }

    private IEnumerator SendWithDelay(float delay, string playerName)
    {
        yield return new WaitForSeconds(delay);
        SendPacketsToAllClients(new PlayerTurnPacket(PlayerInformation.Instance.PlayerData, playerName).Serialize());
    }
}


/*
 * 

        if (clientConnected)
        {
            try
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    PositionPacket ps = new PositionPacket(playerData, new Vector3(1, 2, 3));
    client.Send(ps.Serialize());

                    Debug.LogError($"[Server] Sent Position Packet to client with player ID of {ps.playerData.ID}");
                    Debug.LogError($"[Server] And player Name of {ps.playerData.Name}");
                    Debug.LogError($"[Server] And position of {ps.Position}");
                }

if (Input.GetKeyDown(KeyCode.W))
{
    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Server"));
    instantiatedGameObject = Utils.InstantiateOverNetwork(
        client,
        playerData,
        "Player/Prefab/Player",
        new Vector3(5, 4, -9),
        Quaternion.Euler(45, 0, 0));
}

if (Input.GetKeyDown(KeyCode.E))
{
    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Server"));
    Utils.DestoryOverNetwork(
        client,
        playerData,
        instantiatedGameObject);
}
            }
            catch (SocketException ex)
            {
    if (ex.SocketErrorCode != SocketError.WouldBlock)
    {
        Debug.LogError($"[Server] {ex}");
    }
}
        }
    }
*/