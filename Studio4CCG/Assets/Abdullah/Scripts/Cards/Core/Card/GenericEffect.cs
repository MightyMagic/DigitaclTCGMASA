using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEffect : MonoBehaviour, IBaseEffect
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

    public void RequestActivate()
    {
        //Request Response from server

        /*        bool isActivated=false;
                bool[] targetlist = { targetCards, targetPlayer, targetTile, targetHand, targetDeck, targetGraveyard};
                foreach (var item in targetlist)
                {
                    if (isActivated == true) break;
                    if (item == true)
                    {
                        //RequestResponse()  from server
                        isActivated = true;
                        break;
                    }
                }*/

    }
    public void ActivateEffect()
    {
        //Activate from server.
        ResolvedEffect();
    }
    public void ResolvedEffect()
    {
        //notiffy the server

    }

}
