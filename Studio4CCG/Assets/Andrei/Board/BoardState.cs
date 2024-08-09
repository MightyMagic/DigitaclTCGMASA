using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class BoardState 
{

    public List<BoardObject> boardObjects;

   // public List<TextMeshProUGUI> cardNames;

    private void Start()
    {
        //boardObjects = new List<BoardObject>(24);

        //for(int i = 0; i < boardObjects.Count; i++)
        //{
        //    boardObjects[i] = new BoardObject();
        //}
    }

    public BoardState()
    {
        boardObjects = new List<BoardObject>();
    }

    private void Update()
    {
       // for(int i = 0; i < boardObjects.Count;i++)
       // {
       //     cardNames[i].text = "Card " + i + ": " + boardObjects[i].cardName;
       // }
    }
}
