using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEffect : BaseEffect
{
    
    [Header("Place Holder Name")]
    public string description;
    [Header("What to Target? ")]
    public bool targetSelf;
    public bool targetCards, targetPlayer, targetTile, targetHand, targetDeck, targetGraveyard;
    [Header("Does it effect stats? ")]
    public bool targetState;
    public StatesList statesList;
    public int byHowMuch;
    [Header("Does it effect Ownership? ")]
    public bool targetOwnership;
    [Header("Does it effect States? ")]
    public bool targetStates;
    public bool sendToDeck, sendToHand, sendToGraveyard, sendToField;


    QueueResponse queueResponse;
    public void Start()
    {
        //cast it to it type
        queueResponse = (QueueResponse)FindFirstObjectByType(typeof(QueueResponse));
    }

    //the server ask both player if they have a response.

    //need a target
    public override bool RequestActivation(BaseCard card)
    {

        queueResponse.RequestToBeQueued(gameObject.transform);
        //check cards info if a condition can be met.
        //Return true or false to the server.

        //case true pause aske the player for a response. 

        if (targetCards)
        {
            if (targetState) {
                //targeted card
                //card.
            }
            if (targetStates) { }
            if (targetOwnership) { }
        }
        else if (targetSelf)
        {

        }
        else if (targetPlayer)
        {

        }
        else if (targetTile)
        {

        }
        else if (targetHand)
        {

        }
        else if (targetDeck)
        {

        }
        else if (targetGraveyard)
        {

        }




        return false;
    }

    //queue card to be activated, Then ask again RequestActivation();
    // if both said no & no.
    //ResolvedEffect()
    public override void ActivateEffect()
    {
        //effect code goes here

        RequestActivation(gameObject.GetComponent<BaseCard>());
    }

    // cards will be resolved 
    // case it was destroied this effect won't activate
    // because it was removed from the queue
    public override  void ResolvedEffect()
    {
        //notiffy the server
        //end result of chain effect activation.
    }

}
