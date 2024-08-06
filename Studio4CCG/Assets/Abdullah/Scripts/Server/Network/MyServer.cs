using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UnityEditor.PackageManager;
using UnityEngine;

public class MyServer : MonoBehaviour
{
    Socket serverSocket;
    public List<Socket> clients = new List<Socket>();
    int orderingplayer;


    void Start()
    {
        //server socket starting point
        //created, streamed, port type
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //socket end point. 
        serverSocket.Bind(new IPEndPoint(IPAddress.Any, 3000));
        serverSocket.Listen(0);
        serverSocket.Blocking = false;


    }

    // Update is called once per frame
   public  void Update()
    {
        //Add Clients
        //Recieve Data
        //Process Data
        //Validate data.(big statment need more breaking)  Authorititive 
        //Update Players.Send the packet to all.
        //



        try
        {
            clients.Add(serverSocket.Accept());
            Debug.Log("I added someone");
            clients[orderingplayer].Send(new PlayerNumberPacket(orderingplayer).Serialize());
            orderingplayer++;
        }
        catch
        {

        }

            for (int i = 0; clients.Count > i; i++)
            {

                try
                {

                    byte[] buffer = new byte[clients[i].Available];
                    clients[i].Receive(buffer);
                int bufferOffset = 0;
                int curretBuffersize = buffer.Length;

                BasePackt basePackt = new BasePackt().DeSerialize(buffer, bufferOffset);
                

                



                // we don't send back the same data again from where we recieve it.
                for (int j = 0; clients.Count > j; j++)
                    {
                    
                    /*                    if (i == j) 
                                            {
                                                continue; 
                                            }
                    */
/*                    if (basePackt.Type == BasePackt.PacketType.FirstToPlayPacket)
                    {
                        if (i == 0)
                        {
                            Debug.Log("I'm player1");
                            clients[i].Send(buffer.ToArray());
                        }
                        else
                        {
                            Debug.Log("I'm player2");

                            FirstToPlayPacket firstToPlayPacket = new FirstToPlayPacket().DeSerialize(buffer, bufferOffset);
                            clients[i].Send(new FirstToPlayPacket(firstToPlayPacket.PlayerID, firstToPlayPacket.FirstToPlay, 1).Serialize());


                        }

                    }
*/                    

                    clients[i].Send(buffer.ToArray());
                    }

                }

                catch 
                { 
                
                }






            }
    }
    
    //player 1
    //player 2
    //Response only to player 1
    //



}
