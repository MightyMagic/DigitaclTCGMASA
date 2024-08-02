using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkComponet : MonoBehaviour
{

    // it's for syncing objects
    //Network Manager compare it to NetworkID see if it match
    // if match the NetowkrID is the owner. 
    // then it can be controlled
    public string ownerID;

    public bool IsMine()
    {

        return true;

    }
}
