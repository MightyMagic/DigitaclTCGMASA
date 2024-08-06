using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevel : MonoBehaviour
{
    public string sceneName;
    public void ConnectToServer()
    {
        try
        {
            NetworkManager.instance.OnConnectedToServer();
        }
        catch { }
    }
    public void SwitchScene()
    {
        NetworkManager.instance.SendData(new SceneTransitionPacket(sceneName).Serialize());

    }
}
