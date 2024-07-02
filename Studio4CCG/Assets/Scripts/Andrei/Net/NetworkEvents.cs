using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkEvents : MonoBehaviour
{
    public delegate void OnPositionReceived(Vector3 positionReceived);
    public OnPositionReceived onPositionReceived;
}
