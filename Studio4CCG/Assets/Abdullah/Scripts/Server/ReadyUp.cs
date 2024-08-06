using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyUp : MonoBehaviour
{
    // Start is called before the first frame update

    public Button startButton;
    public bool ready=false;
    static int readyCount;
    public string sceneName;

    void Update()
    {
        try
        {
            if (readyCount == 2)
            {
                startButton.interactable = true;

            }
            else 
            { 
                startButton.interactable = false;
            }


        }
        catch { }
    }
    public void ReadyUpButton()
    {
        if (ready)
        {
            ready = false;
            readyCount--;
        } 
        else
        {
            ready = true;
            readyCount++;
        }

    }


    public void BeginGame()
    {
        //send
        NetworkManager.instance.SendData(new SceneTransitionPacket(sceneName).Serialize());

    }

}
