using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class MyServer : MonoBehaviour
{
    Socket serverSocketTCP;
    Socket serverSocketUDP;

    public List<Socket> clientsTCP = new List<Socket>();
    public List<Socket> clientsUDP = new List<Socket>();

    int orderingplayer;
    public static MyServer instance;
    Thread serverTCP;
    Thread serverUDP;
    public bool serverIsRunning;
    private void Awake()
    {
    if (instance == null)
            {
                instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

    }
    void Start()
    {
        //server socket starting point
        //created, streamed, port type
        ListenForClientsTCP();
        ListenForClientsUDP();

    }

    private void ListenForClientsTCP()
    {
        serverSocketTCP = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //socket end point. 
        serverSocketTCP.Bind(new IPEndPoint(IPAddress.Any, 50000));
        serverSocketTCP.Listen(0);
        serverSocketTCP.Blocking = false;
        Debug.Log("connected to TCP");
        serverTCP = new Thread(() => RunTCPServer());
        serverTCP.Start();
        Debug.Log("Started TCP Server");
    }
    private void ListenForClientsUDP()
    {
        serverSocketUDP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        //socket end point. 
        serverSocketUDP.Bind(new IPEndPoint(IPAddress.Any, 3000));
        serverSocketUDP.Blocking = false;
        Debug.Log("connected to UDP");


        serverUDP = new Thread(() => RunUDPServer());
        serverUDP.Start();
        Debug.Log("Started UDP Server");
    }


    // Update is called once per frame
    public void RunTCPServer()
    {
        //Add Clients
        //Recieve Data
        //Process Data
        //Validate data.(big statment need more breaking)  Authorititive 
        //Update Players.Send the packet to all.
        //
        while (serverIsRunning)
        {
            try
            {
                clientsTCP.Add(serverSocketTCP.Accept());
                Debug.Log("I added someone");
                clientsTCP[orderingplayer].Send(new PlayerNumberPacket(orderingplayer).Serialize());
                orderingplayer++;
            }
            catch
            {

            }

            for (int i = 0; clientsTCP.Count > i; i++)
            {
                Debug.Log(i);

                try
                {

                    byte[] buffer = new byte[clientsTCP[i].Available];
                    clientsTCP[i].Receive(buffer);





                    int bufferOffset = 0;
                    int curretBuffersize = buffer.Length;

                    BasePackt basePackt = new BasePackt().DeSerialize(buffer, bufferOffset);

                    // we don't send back the same data again from where we recieve it.
                    for (int j = 0; clientsTCP.Count > j; j++)
                    {

                        if (basePackt != null && basePackt.Type == BasePackt.PacketType.FirstToPlayPacket)
                        {
                            if (i == 0)
                            {
                                Debug.Log("I'm player1");
                                clientsTCP[i].Send(buffer.ToArray());
                            }
                            else
                            {
                                Debug.Log("I'm player2");

                                FirstToPlayPacket firstToPlayPacket = new FirstToPlayPacket().DeSerialize(buffer, bufferOffset);
                                clientsTCP[i].Send(new FirstToPlayPacket(firstToPlayPacket.FirstToPlay, firstToPlayPacket.PlayerCount).Serialize());


                            }

                        }
                        else
                        {
                            Debug.Log("I'm sending a data to client" + i);
                            clientsTCP[i].Send(buffer.ToArray());
                        }

                    }

                }

                catch
                {

                }

            }


        }


    }

    public void RunUDPServer()
    {
        //Add Clients
        //Recieve Data
        //Process Data
        //Validate data.(big statment need more breaking)  Authorititive 
        //Update Players.Send the packet to all.
        //
        while (serverIsRunning)
        {
            try
            {
                clientsUDP.Add(serverSocketUDP.Accept());
                Debug.Log("I added someone");
            }
            catch
            {

            }

            for (int i = 0; clientsUDP.Count > i; i++)
            {
                Debug.Log(i);

                try
                {

                    byte[] buffer = new byte[clientsUDP[i].Available];
                    clientsUDP[i].Receive(buffer);


                    for (int j = 0; clientsUDP.Count > j; j++)
                    {
                        // we don't send back the same data again from where we recieve it.
                        //if (j == i) continue;
                        Debug.Log("I'm sending a data to client" + i);
                            clientsUDP[i].Send(buffer.ToArray());

                    }

                }
                catch
                {

                }

            }


        }


    }

    //player 1
    //player 2
    //Response only to player 1
    //

    private void OnApplicationQuit()
    {
        serverTCP.Abort();
        serverUDP.Abort();
    }
    
}
