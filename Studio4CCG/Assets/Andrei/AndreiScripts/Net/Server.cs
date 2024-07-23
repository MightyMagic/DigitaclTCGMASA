using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Text;

public class Server : MonoBehaviour
{

    [SerializeField] NetworkEvents events;

    Socket socket;
    List<Socket> clients = new List<Socket>();

    Vector3 currentPosition = Vector3.up;

    // Start is called before the first frame update
    void Start()
    {
        socket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

        socket.Bind(new IPEndPoint(IPAddress.Any, 3000));
        socket.Listen(10);
        socket.Blocking = false;

        

        print("Waiting for connection...");
    }

    // Update is called once per frame
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentPosition += Vector3.up;
            PositionPacket positionPacket = new PositionPacket(currentPosition, new PlayerData("612654162", "nicolas cage"));
            events.onPositionReceived(currentPosition);

            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Send(positionPacket.Serialize());
            }
        }
    }
}
