using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Chating_Test : MonoBehaviour
{

   public TextMeshProUGUI inputText;


    void Start()
    {
        
    }


    public void SendTextToServer()
    {


        NetworkManager.instance.SendData(new TestTextPacket(inputText.text).Serialize());



    }

}
