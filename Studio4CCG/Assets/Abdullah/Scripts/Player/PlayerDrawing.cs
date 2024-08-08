using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrawing : MonoBehaviour
{
    public int drawCount;
    // Start is called before the first frame update
    DeckDrawCard drawCard;
    // Update is called once per frame
    void Update()
    {
        if (drawCount >0)
        {
            //stop input untill all cards are drawn.

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {

                drawCard.DrawCard();
                drawCount--;

            }


        }
        //Restore Input.
        else
        {



        }
    }



}
