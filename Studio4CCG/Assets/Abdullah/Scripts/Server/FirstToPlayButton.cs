using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstToPlayButton : MonoBehaviour
{
    // Start is called before the first frame update

    [Range(1, 2)]
    string ownerID;
    public Image checkMark;
    int orderToPlay;
    private void Start()
    {
        ownerID = NetworkManager.instance.playerID;
    }
    private void Update()
    {
        if (NetworkManager.instance.isFirst == true)
        {
            checkMark.enabled = true;
        }
        else
        {

            checkMark.enabled = false;
        }


    }
    public void ChangeFirst()
    {

        //player1 
        //0 == 0
        //0=true
        //1=false

        //1!=0
        //0=false
        //1=true
        ownerID = NetworkManager.instance.playerID;
        //send true for first player
        //false for second
        NetworkManager.instance.isFirst = true;
            orderToPlay = NetworkManager.instance.playerOrder;

        if (orderToPlay == 0)
        {
            NetworkManager.instance.isFirst=true;
            NetworkManager.instance.SendData(new FirstToPlayPacket(true, 0).Serialize());

            try
            {
                NetworkManager.instance.SendData(new FirstToPlayPacket(false, 1).Serialize());
            }
            catch { }
        }
        else
        {
            NetworkManager.instance.SendData(new FirstToPlayPacket(true, 1).Serialize());
            Debug.LogWarning(NetworkManager.instance.isFirst);
            try
            {

                NetworkManager.instance.SendData(new FirstToPlayPacket(false, 0).Serialize());

            }
            catch
            {
                Debug.LogError("no second player");
            }

        }

    }



}
