using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;

namespace AndreiScripts
{
    public class Client : MonoBehaviour
    {
        Socket socket;
        public TextMeshProUGUI debugText;
        public List<TextMeshProUGUI> cardNames;

        void Start()
        {
            socket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
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

                    debugText.text = "Receiving the package of size " + buffer.Length;
                    //Debug.Log("Receiving the package of size " + buffer.Length);

                    BoardPacket bp = new BoardPacket();
                    bp.Deserialize(buffer, 0);

                    for (int i = 0; i < bp.currentBoard.boardObjects.Count; i++)
                    {
                        debugText.text += bp.currentBoard.boardObjects[i].cardName + " , ";
                    }



                    for (int i = 0; i < bp.currentBoard.boardObjects.Count; i++)
                    {
                        cardNames[i].text = "Client Card " + i + ": " + bp.currentBoard.boardObjects[i].cardName;
                    }



                    //PositionPacket ps = new PositionPacket();
                    ////ps.Deserialize(buffer);
                    //Debug.Log("Id: " + ps.playerData.ID);
                    //Debug.Log("Name: " + ps.playerData.Name);
                    //Debug.Log(ps.Position.x + " " +  ps.Position.y + " " + ps.Position.z);
                }
                catch
                {

                }
            }
        }
    }
}
