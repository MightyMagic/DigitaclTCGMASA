using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Client : MonoBehaviour
{
    Socket socket;

    void Start()
    {
        socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
            socket.Blocking = false;
        }

        if (socket.Available > 0)
        {
            try
            {

                byte[] buffer = new byte[socket.Available];
                socket.Receive(buffer);

                PositionPacket ps = new PositionPacket();
                ps.Deserialize(buffer);
                Debug.Log("Id: " + ps.playerData.ID);
                Debug.Log("Name: " + ps.playerData.Name);
                Debug.Log(ps.Position.x + " " +  ps.Position.y + " " + ps.Position.z);
            }
            catch
            {

            }
        }
    }
}
