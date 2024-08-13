using System.Collections;
using UnityEngine;

public class DeckDrawCard : MonoBehaviour
{
    Deck deck;
    Hands hand;
    float timer;
    public Transform spawnPoint;
    public Transform cardOnHandPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        deck = gameObject.GetComponent<Deck>();
        hand= FindAnyObjectByType<Hands>();
    }

    public void DrawCard()
    {
        if (deck.myCards.Count==0) 
        {

            
            //update the server I losted.
            Debug.Log("no more cards");

        }
        else
        {
            GameObject newCard = CreateCard();

            //Add newCard to hand.
            //Remove from the deck. 
            //update tag.
            hand.cardsOnHand.Add(newCard);
            hand.cardsOnHand[0].gameObject.GetComponentInChildren<BaseCard>().UpdateCardState(CardState.hand);
            newCard.transform.parent = hand.gameObject.transform;


            Debug.Log("I added " + newCard.name);
            Debug.Log("I removed " + deck.myCards[0].name);
            deck.myCards.Remove(deck.myCards[0]);

            //Send Packet I drew 
            //Send Packet I drew 

/*            NetworkManager.instance.SendData(new InstantiateCardPacket(
                "PrefabName", 
                newCard.transform.position, 
                new Quaternion(0, 0, 0, 0)).Serialize());
*/

            //Send Packet I drew 
            //Send Packet I drew 

            //
            StartCoroutine(LerpToTile(newCard, cardOnHandPosition));

        }
    }

    public GameObject CreateCard()
    {
        //send it to hand
        //Instantiate(newCard);

        //Spawn card at deck
        return Instantiate(deck.myCards[0], spawnPoint.position, Quaternion.identity);
    }

    public IEnumerator LerpToTile(GameObject newCard, Transform positionOnHand)
    {
        timer = 0.3f;
        float currentTimer = timer;
        Quaternion startRotation = positionOnHand.rotation;
        while (currentTimer >= 0)
        {

            //lerp to hand
            newCard.transform.position = Vector3.Lerp(positionOnHand.position, newCard.transform.position, currentTimer / timer);


            //lerp card to the new rotation 
            newCard.transform.rotation = Quaternion.Lerp(newCard.transform.rotation, startRotation, 0.2f);
            
            //countdown
            currentTimer -= 1 * Time.deltaTime;
            yield return null;
            foreach (GameObject card in hand.cardsOnHand)
            {
                card.transform.position = Vector3.Lerp(card.transform.position, card.transform.position + new Vector3(0.05f, 0, 0), currentTimer / timer);

            }

        }


    }


}
