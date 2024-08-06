using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstToPlayButton : MonoBehaviour
{
    // Start is called before the first frame update

    [Range(1, 2)]
    int playerNumber = 0;
    string ownerID;
    Toggle toggle;
    private void Start()
    {
        ownerID = NetworkManager.instance.playerID;
        toggle = GetComponent<Toggle>();
    }

    public void ChangeFirst()
    {
        int orderToPlay = NetworkManager.instance.playerOrder;
        bool isFirst = NetworkManager.instance.isFirst;
        if (playerNumber == orderToPlay)
        {

            ownerID = NetworkManager.instance.playerID;
            NetworkManager.instance.SendData(new FirstToPlayPacket(isFirst, 0).Serialize());
            try
            {
                NetworkManager.instance.SendData(new FirstToPlayPacket(!isFirst, 1).Serialize());
            }
            catch { Debug.LogError("no second player"); }
            Debug.Log("I'm player one");

        }
        else
        {

            ownerID = NetworkManager.instance.playerID;
            NetworkManager.instance.SendData(new FirstToPlayPacket(!isFirst, 0).Serialize());
            try
            {
                NetworkManager.instance.SendData(new FirstToPlayPacket(isFirst, 1).Serialize());
            }
            catch { Debug.LogError("no second player"); }

            Debug.Log("I'm player two");

        }

    }


}
