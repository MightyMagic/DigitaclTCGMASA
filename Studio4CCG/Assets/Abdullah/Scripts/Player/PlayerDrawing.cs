using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrawing : MonoBehaviour
{
    public int drawCount;
    // Start is called before the first frame update
    
    DeckDrawCard drawCard;
    GameObject drawCollider;
    void Start()
    {
        drawCollider = GameObject.FindGameObjectWithTag("Draw");
        drawCard = FindAnyObjectByType<DeckDrawCard>();

    }
    // Update is called once per frame
    void Update()
    {

        if (drawCount >0)
        {
            //stop input untill all cards are drawn.
            drawCollider.SetActive(true);
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {

                drawCard.DrawCard();
                drawCount--;

            }


        }
        //Restore Input.
        else
        {
            drawCollider.gameObject.SetActive(false);



        }
    }



}
