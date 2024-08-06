using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
     Transform selected;
     Transform deselect;
    Transform buttons;
    bool firsttimeSelected =true;
    GameObject Deck;
    void Start()
    {
        Deck=FindAnyObjectByType(typeof(Deck)).GameObject();
        selected=gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //mouse position


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
                CheckCard();
        }



    }


    void CheckCard()
    {

        //highlight Selection.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit) && hit.collider.transform.tag == "OnHands")
        {
            if (firsttimeSelected == false && hit.collider.transform.GetInstanceID() != selected.GetInstanceID())
            {
                //old button
                buttons.gameObject.SetActive(false);

                //new button
                selected = hit.collider.transform;
                selected.GetChild(0).gameObject.SetActive(true);

                //switch button
                deselect = selected;
                buttons = deselect.GetChild(0);


            }
            else if (firsttimeSelected)
            {
                //opened the button current button
                deselect = hit.collider.transform;
                buttons = deselect.GetChild(0);
                buttons.gameObject.SetActive(true);

                firsttimeSelected = false;

            }
            //show buttons nested inside the card.



        }
        else if (Physics.Raycast(ray, out hit) && hit.collider.transform.tag != "OnHands" && hit.collider.transform.tag != "Button")
        {
            buttons.gameObject.SetActive(false);
            selected = gameObject.transform;
        }


        //show cards info. 
        //showbuttons
    }



}
