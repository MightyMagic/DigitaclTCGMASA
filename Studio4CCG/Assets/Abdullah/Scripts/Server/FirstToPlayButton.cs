using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstToPlayButton : MonoBehaviour
{
    // Start is called before the first frame update

    [Range(1,2)]
    int playerNumber = 0;
    string ownerID;

    private void Start()
    {
        ownerID = NetworkManager.instance.playerID;

    }

    public void ChangeFirst()
    {
        int orderToPlay = NetworkManager.instance.playerOrder;
        bool isFirst = NetworkManager.instance.isFirst;
        if (playerNumber== orderToPlay) { 
        ownerID = NetworkManager.instance.playerID;
        NetworkManager.instance.SendData(new FirstToPlayPacket(isFirst,0).Serialize());
        NetworkManager.instance.SendData(new FirstToPlayPacket(isFirst, 0).Serialize());

        }
        else
        {
            ownerID = NetworkManager.instance.playerID;
            NetworkManager.instance.SendData(new FirstToPlayPacket(isFirst, 0).Serialize());


        }

    }


}
