using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentDeck : MonoBehaviour
{
    List<CardInfo> remainingCards = new List<CardInfo>();



    CardInfo DrawNewCard()
    {
        CardInfo topCard = remainingCards[remainingCards.Count-1];
        remainingCards.RemoveAt(remainingCards.Count-1);
        return topCard;
    }

    public void PopulateDeck(int deckIndex)
    {
        //Request to populate the deck from the server
    }
}
