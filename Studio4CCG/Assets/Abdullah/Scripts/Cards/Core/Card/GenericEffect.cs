using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEffect : BaseEffect
{

    [Header("What to Target? ")]
    public bool targetSelf;
    public bool targetCards,targetPlayer, targetTile,targetHand,targetDeck, targetGraveyard;
    [Header("Does it effect stats? ")]
    public bool targetState;
    public int targetHealth, targetAttack, targetDefense;
    [Header("Does it effect States? ")]
    public bool targetStates;
    public bool stateDeck, stateHand, stateGraveyard;
    [Header("Does it effect Ownership? ")]
    public bool targetOwnership;


    //the server ask both player if they have a response.
    public override bool RequestActivation()
    {

        //check cards info if a condition can be met.
        //Return true or false to the server.

        //case true pause aske the player for a response. 

        return false;
    }

    //queue card to be activated, Then ask again RequestActivation();
    // if both said no & no.
    //ResolvedEffect()
    public override void ActivateEffect()
    {
        //effect code goes here

        RequestActivation();
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
