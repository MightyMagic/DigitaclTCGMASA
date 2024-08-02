using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    public string networkID;
    public TextMeshProUGUI textUI;
    Socket playerSocket;
    float timer;


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
                    case BasePackt.PacketType.TestText:
                        TestTextPacket textPacket = new TestTextPacket().DeSerialize(buffer, bufferOffset);
                        textUI.text = textPacket.Text;


                        break;
                        /*                    case BasePackt.PacketType.None:
                                                break;
                                            case BasePackt.PacketType.None:
                                                break;
                                            case BasePackt.PacketType.None:
                                                break;
                                            case BasePackt.PacketType.None:
                                                break;
                                            case BasePackt.PacketType.None:
                                                break;
                        */
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

        string data = Encoding.ASCII.GetString(buffer);
        Debug.Log(data);
    }
    //button
    public void OnConnectedToServer()
    {
        playerSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
        playerSocket.Blocking = false;
        Debug.Log("Connecting...");
    }


}
