using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckShuffle : MonoBehaviour
{
    Deck deck;
    
      
    // Start is called before the first frame update
    private void Start()
    {
        deck = gameObject.GetComponent<Deck>();

    }
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            ShuffleDeck();

        }
    }
    public void ShuffleDeck()
    {
        // deck.myCards
        for (int i = 0; i < deck.myCards.Count ; i++)
        {
            int randomNumber = Random.Range(0, deck.myCards.Count);
            GameObject temp = deck.myCards[i];
            deck.myCards[i] = deck.myCards[randomNumber];
            deck.myCards[randomNumber] = temp;
        }

    }
}