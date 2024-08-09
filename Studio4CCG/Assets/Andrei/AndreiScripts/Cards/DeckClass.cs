using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckClass 
{
    public List<Card> allCards;
    public List<Card> cardsLeft;

    public Card topCard;

    public Card DrawNewCard()
    {

        Card card = new Card();

        if (cardsLeft.Count == 0)
        {

            Debug.LogError("No cards left in the deck!");
            return card;
        }
        else
        {
            card = cardsLeft[cardsLeft.Count - 1]; // dk about this, but whatever
            cardsLeft.RemoveAt(cardsLeft.Count - 1);

            return card;
        }
    }

    public List<Card> DrawSeveralCards(int count)
    {
        List<Card> cards = new List<Card>();

        for(int i = 0; i < count; i++)
        {
            cards.Add(DrawNewCard());
        }

        return cards;
    }

    public List<Card> ShuffleDeck()
    {
        int n = cardsLeft.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            Card value = cardsLeft[k];
            cardsLeft[k] = cardsLeft[n];
            cardsLeft[n] = value;
        }

        return cardsLeft;
    }
}
