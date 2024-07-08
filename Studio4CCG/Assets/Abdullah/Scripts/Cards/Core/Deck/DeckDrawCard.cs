using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckDrawCard : MonoBehaviour
{
    Deck deck;
    Hands hand;
    public Transform cardposition;
    // Start is called before the first frame update
    private void Start()
    {
         deck = gameObject.GetComponent<Deck>();
        hand= FindAnyObjectByType<Hands>();
    }

    public void DrawCard()
    {
        if (deck.myCards[0] == null)
        {
            Debug.Log("no more cards");


        }
        else
        {
            //send it to hand
            hand.cardsOnHand.Add(deck.myCards[0]);
            Debug.Log("I added "+ deck.myCards[0].name);
            Debug.Log("I removed " + deck.myCards[0].name);
            deck.myCards.Remove(deck.myCards[0]);

            //update the server


        }
    }


}
