using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Text;

namespace AndreiScripts
{
    public class Server : MonoBehaviour
    {
        Socket socket;
        List<Socket> clients = new List<Socket>();

        [SerializeField] BoardState boardState;
        public BoardPacket boardPacket;

        void Start()
        {
            socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            socket.Bind(new IPEndPoint(IPAddress.Any, 3000));
            socket.Listen(10);
            socket.Blocking = false;

            print("Waiting for client to connect...");

            //for(int i = 0; i < boardState.boardObjects.Count; i++)
            //{
            //    boardPacket.currentBoard.boardObjects.Add(boardState.boardObjects[i]);
            //}

            //boardPacket.PlayerData = new PlayerData("Server", "Server");
        }

        void Update()
        {
            try
            {
                clients.Add(socket.Accept());
                print("Accepted connection...");
            }
            catch
            {

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    //for (int j = 0; j < 5; j++)
                    //{
                    //    clients[i].Send(new BoardPacket(boardState, new PlayerData("SERVER", "SERVER")).Serialize());
                    //}

                    //clients[i].Send(boardPacket.Serialize());
                    clients[i].Send(new BoardPacket(boardState, new PlayerData("SERVER", "SERVER")).Serialize());

                    Debug.Log("Sending board!");
                }
            }

            /*for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Available > 0)
                {
                    try
                    {
                        byte[] buffer = new byte[clients[i].Available];
                        clients[i].Receive(buffer);

                        for (int j = 0; j < clients.Count; j++)
                        {
                            if (i == j)
                                continue;

                            clients[j].Send(buffer);
                        }
                    }
                    catch
                    {

                    }
                }
            }*/
        }
    }
}