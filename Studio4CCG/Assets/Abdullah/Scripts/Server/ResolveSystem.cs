using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResolveSystem : MonoBehaviour
{

    QueueResponse queueResponse;

    void Resolve()
    {
      
        //while loop active untile we find the last parent
        Transform activateCard = queueResponse.lastChild;

        activateCard.GetComponent<BaseEffect>().ResolvedEffect();
        if (queueResponse.lastChild.parent != null) { 

            //update the last child and move down the chain
        queueResponse.lastChild = queueResponse.lastChild.parent;
            //clear parents and child
            activateCard.GetComponent<BaseEffect>().parent = null;
            activateCard.GetComponent<BaseEffect>().child = null;

        }
        else
        {
            Debug.Log("All effect was resolved");
            //break;
        }

    }

}
