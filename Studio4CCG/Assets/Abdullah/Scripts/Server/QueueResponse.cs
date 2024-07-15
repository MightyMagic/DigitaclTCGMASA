using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueResponse : MonoBehaviour
{
    //
    public Transform lastChild=null;
    BaseEffect baseEffect;

    public void RequestToBeQueued(Transform card)
    {
        baseEffect = card.gameObject.GetComponent<BaseEffect>();

        //it's the first.
        if (lastChild == null)
        {
            lastChild = card;
            Debug.Log("first element is "+lastChild.name);

        }
        //
        else
        {
            //assign the previous card child.
            lastChild.GetComponent<BaseEffect>().myChild = card;
            Debug.Log(lastChild.name + "Parent of: " + card.name);

            //assign the current card parent
            baseEffect.myParent = lastChild;
            Debug.Log(card.name+" Child of: " + lastChild.name);

            //update the last node in the chian
            lastChild = card;


            //send packet to players to update ?
        }

    }



}
