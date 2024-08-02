using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class MyServer : MonoBehaviour
{
    Socket serverSocket;
    List<Socket> clients = new List<Socket>();



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


                    // we don't send back the same data again from where we recieve it.
                    for (int j = 0; clients.Count > j; j++)
                    {

/*                    if (i == j) 
                        {
                            continue; 
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





}
