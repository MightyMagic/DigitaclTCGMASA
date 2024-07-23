using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    public string networkID;
    public TextMeshProUGUI textUI;
    Socket playerSocket;

    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //persest between scene changes
            DontDestroyOnLoad(gameObject);
        
        }
        else
        {
            //no overwriting 
            Destroy(gameObject);

        }

    }
    void Start()
    {
        //create a socket to connect
        playerSocket = new Socket(
                                AddressFamily.InterNetwork,
                                SocketType.Stream,
                                ProtocolType.Tcp);

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
            int bufferOffset =0;
            int curretBuffersize= buffer.Length;

            //Deserilize the data
            //The logic to sort the data
            while( curretBuffersize > 0 )
            {
                Debug.Log("curretBuffersize bigger than 0");
                //All data inhert from basePacket.
                BasePackt basePackt = new BasePackt().DeSerialize(buffer, bufferOffset);
                //set the value back to zero
                curretBuffersize -= basePackt.PacketSize;



                //execute logic based the type of the packet
                switch(basePackt.Type)
                {
                    case BasePackt.PacketType.TestText:
                        Debug.Log("Updated Text");
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

        Debug.Log("Sent Message");
        playerSocket.Send(buffer);
    }
//button
public void OnConnectedToServer()
    {
        playerSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
        playerSocket.Blocking = false;
    }


}
