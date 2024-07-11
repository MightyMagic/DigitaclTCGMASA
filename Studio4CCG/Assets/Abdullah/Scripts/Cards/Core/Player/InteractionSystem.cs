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
    public bool canDraw=true;
    GameObject Deck;
    void Start()
    {
        Deck=FindAnyObjectByType(typeof(Deck)).GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        //mouse position
        HighlightSelection();


        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(canDraw==true && selected.tag=="Deck")
            {
                //Draw a card can be and event to raise
                Deck.GetComponent<DeckDrawCard>().DrawCard();
            }
            else { 
                CheckCard();
            }
        }



    }

    private void HighlightSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        //highlight selection
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (firsttimeSelected == false && hit.collider.name != selected.name)
                {

                    if (hit.collider.transform.tag != "Button")
                    {
                        selected = hit.collider.transform;
                        selected.GetComponent<ParticleSystem>().Play();

                        //highlight tile 
                        //highlight cards on hand & aniamtion.
                    }

                }
                else if (firsttimeSelected == true)
                {
                    firsttimeSelected = false;
                    selected = hit.collider.transform;
                    selected.GetComponent<ParticleSystem>().Play();
                    deselect = selected;
                }
            }
        }
    }

    void CheckCard()
    {

        //highlight Selection.


        //if it's mising a child give it one
        if (deselect.name != selected.name&& selected.tag!= "Button")
        {
            //show buttons nested inside the card.
            if (selected.childCount == 0) {
                GameObject empty = Instantiate(new GameObject("empty"));
                empty.transform.parent = selected.transform;
                Debug.LogWarning("no button found for" + selected.name);


            }
            buttons = deselect.GetChild(0);
                buttons.gameObject.SetActive(false);



            //store the buttons to hide it later.
            deselect = selected;
            //return to normal
        }
        //on card selected show buttons.
            buttons = selected.GetChild(0);
            buttons.gameObject.SetActive(true);


        //show cards info. 
        //showbuttons
    }



}
