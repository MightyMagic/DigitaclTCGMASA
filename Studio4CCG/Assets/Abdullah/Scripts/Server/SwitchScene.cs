using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    public void SwitchingScene()
    {

        NetworkManager.instance.OnConnectedToServer();
        NetworkManager.instance.SendData(new SceneTransitionPacket(sceneName).Serialize());


    }
}
