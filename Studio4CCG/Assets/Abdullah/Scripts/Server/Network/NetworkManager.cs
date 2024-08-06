using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    
    string randmizeID = "1234567890qwertyuiop[]asdfghjklzxcvbnm";
    public string playerID;
    Socket playerSocket;
    float timer;

    string playerName;
    public bool isFirst = false;

    [HideInInspector]
    public int playerOrder;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //persest between scene changes
            DontDestroyOnLoad(gameObject);
            //create a socket to connect
            playerSocket = new Socket(
                                    AddressFamily.InterNetwork,
                                    SocketType.Stream,
                                    ProtocolType.Tcp);
            for (int i = 0; i < 24; i++)
            {
                playerID += randmizeID[Random.Range(0, randmizeID.Length)];
            }

        }
        else
        {
            //no overwriting 
            Destroy(gameObject);

        }

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //test connection
        if (Input.GetKeyDown(KeyCode.C)) OnConnectedToServer();

        //recieve data
        try
        {
            byte[] buffer = new byte[playerSocket.Available];
            playerSocket.Receive(buffer);
            Debug.Log("I receieved data");

            //data position in the buffer;
            int bufferOffset = 0;
            int curretBuffersize = buffer.Length;

            //Deserilize the data
            //The logic to sort the data
            timer = 0;
            while (curretBuffersize > 0)
            {
                timer++;
                if (timer > 100)
                {
                    Debug.LogError("Infinite Loops from curretBuffersize");
                    break;

                }
                    
                //All data inhert from basePacket.
                BasePackt basePackt = new BasePackt().DeSerialize(buffer, bufferOffset);
                //set the value back to zero

                curretBuffersize -= basePackt.PacketSize;



                //execute logic based the type of the packet
                switch (basePackt.Type)
                {

                    case BasePackt.PacketType.PositionPacket:
                        break;
                    case BasePackt.PacketType.InstantiatePacket:
                        break;
                    case BasePackt.PacketType.SceneTransitionPacket:
                        SceneTransitionPacket switchScene = new SceneTransitionPacket().DeSerialize(buffer, bufferOffset);
                        SceneManager.LoadScene(switchScene.SceneName);

                        break;
                    case BasePackt.PacketType.AnimationPacket:
                        break;
                    case BasePackt.PacketType.OwnershipPacket:
                        OwnershipPacket ownershipPacket = new OwnershipPacket().DeSerialize(buffer,bufferOffset);
                        BaseCard[] cards = FindObjectsOfType<BaseCard>();
                        foreach (BaseCard card in cards)
                        {
                            if (card._ownerID == ownershipPacket.currentCardID)
                            {
                                card._ownerID = ownershipPacket.newOwner;
                                break;
                            }
                            
                        }
                        break;

                    case BasePackt.PacketType.PlayerNumberPacket:
                        PlayerNumberPacket playerNumberPacket = new PlayerNumberPacket().DeSerialize(buffer, bufferOffset);
                        playerOrder= playerNumberPacket.PlayerOrder;

                        break;



                }

            }


        }
        catch
        {

        }


    }

    //send data he final proceessed data from EndSerSerialize() feed to this.
    //Wait for GameManager to chose Packet to send and use it Serialize()
    public void SendData(byte[] buffer)
    {

        playerSocket.Send(buffer);

    }
    //button
    public void OnConnectedToServer()
    {
        playerSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
        playerSocket.Blocking = false;
        Debug.Log("Connecting...");
    }


}
