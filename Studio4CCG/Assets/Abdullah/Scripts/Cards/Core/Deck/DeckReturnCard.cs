using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckReturnCard : MonoBehaviour
{
    Deck deck;
    // Start is called before the first frame update
    private void Start()
    {
         deck = gameObject.GetComponent<Deck>();

    }

    public void ReturnCard(GameObject addedCard)
    {
        deck.myCards.Add(addedCard);
        //update the server by the cardID state
    }


}
