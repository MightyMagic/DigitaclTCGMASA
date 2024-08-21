using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData : MonoBehaviour
{
    public List<BoardPlaceHolder> ActiveBoardPlacholders = new List<BoardPlaceHolder>();
    public BoardHalf playersHalf;

    private void Start()
    {
        for (int i = 0; i < playersHalf.playerBoard.Count; i++)
        {
            playersHalf.playerBoard[i].ownerName = PlayerInformation.Instance.PlayerData.Name;
        }

        playersHalf.playerName = PlayerInformation.Instance.PlayerData.Name;

        RefreshBoard();
    }

    void RefreshBoard()
    {
        BoardPlaceHolder[] boardPlaceHolders = FindObjectsOfType<BoardPlaceHolder>();
        ActiveBoardPlacholders.Clear();

        for (int i = 0;i < boardPlaceHolders.Length; i++)
        {
            if (boardPlaceHolders[i] == null & !boardPlaceHolders[i].isOccupied)
            {
                boardPlaceHolders[i].gameObject.SetActive(false);
            }
            else
            {
                ActiveBoardPlacholders.Add(boardPlaceHolders[i]);
            }
        }
    }

}

public enum CardEffect
{
    SpawnTwoAdjacent,
    BuffClass,
    None
}

public enum CardClass
{
    Neutral,
    Totem,
    Knight,
    Zombie,
    None
}

public enum ColumnName
{
    Left,
    Middle,
    Right
}
