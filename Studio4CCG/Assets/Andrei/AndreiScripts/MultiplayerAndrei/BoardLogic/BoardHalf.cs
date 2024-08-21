using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHalf : MonoBehaviour
{
    public string playerName;
    public List<BoardPlaceHolder> playerBoard = new List<BoardPlaceHolder>();


    void Start()
    {
        playerName = PlayerInformation.Instance.PlayerData.Name;
    }
}
