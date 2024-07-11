using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueResponse : MonoBehaviour
{
    //
    public Transform lastChild=null;
   public void RequestToBeQueued(Transform card)
    {
        //it's the first.
        if (lastChild == null)
        {
            lastChild = card;
            //send packet to players to update ?
            //
        }
        //
        else
        {
            //assign the previous card child.
            lastChild.GetComponent<BaseEffect>().child = card;
            //assign the current card parent
            card.parent = lastChild;
            //update the last node in teh chian
            lastChild = card;


            //send packet to players to update ?
        }

    }



}
