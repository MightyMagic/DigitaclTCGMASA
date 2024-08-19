using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActiveDeckList 
{
    public string playerName;
    public List<CardInfo> deckList = new List<CardInfo>();


    public ActiveDeckList()
    {
        deckList = new List<CardInfo>();
        playerName = "";
    }

    public void PopulateDeck(CardInfo[] deckList)
    {
        this.deckList.Clear();
        for(int i = 0; i < deckList.Length; i++)
        {
            this.deckList.Add(deckList[i]);
        }
    }
}
