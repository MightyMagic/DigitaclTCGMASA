using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResolveSystem : MonoBehaviour
{

    QueueResponse queueResponse;
    public void Start()
    {
        //cast it to it type
        queueResponse=(QueueResponse)FindFirstObjectByType(typeof(QueueResponse));
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Resolve();

        }
    }
    void Resolve()
    {
      
        //while loop active untile we find the last parent
        Transform activateCard = queueResponse.lastChild;

        activateCard.GetComponent<BaseEffect>().ResolvedEffect();
        if (queueResponse.lastChild.GetComponent<BaseEffect>().myParent != null) {

            Debug.Log("Resolving: "+ activateCard.name);

            //update the last child and move down the chain
            queueResponse.lastChild = queueResponse.lastChild.GetComponent<BaseEffect>().myParent;
            //clear parents and child
            activateCard.GetComponent<BaseEffect>().myParent = null;
            activateCard.GetComponent<BaseEffect>().myChild = null;

        }
        else if (queueResponse.lastChild.GetComponent<BaseEffect>().myChild != null)
        {
            Debug.Log("Resolving: " + activateCard.name);
            activateCard.GetComponent<BaseEffect>().myParent = null;
            activateCard.GetComponent<BaseEffect>().myChild = null;


        }
        else
        {
            Debug.Log("All effect are resolved");
            //break;
        }

    }

}
