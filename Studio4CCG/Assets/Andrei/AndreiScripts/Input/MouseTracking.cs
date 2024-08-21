using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseTracking : MonoBehaviour
{
    /*
    bool playingCard = false;
    bool pointingAtPlaceHolder = false;

    [SerializeField] LayerMask placeHolderLayer;
    [SerializeField] LayerMask backgroundLayer;

    [SerializeField] Card cardExample;
    [SerializeField] GameplayManager gameplayManager;

    GameObject placeHolderObject;
    CardPlaceHolder cardPlaceHolder;

    //[SerializeField] Color defaultColor;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void TrackMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.black);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placeHolderLayer))
        {

            Debug.Log("Hit object name: " + hit.transform.name);
            GameObject currentTarget = hit.transform.gameObject;
            if (currentTarget.GetComponent<CardPlaceHolder>() != null)
            {
                cardPlaceHolder = currentTarget.GetComponent<CardPlaceHolder>();
                
                //pointingAtPlaceHolder = true;

                if (cardPlaceHolder.occupied)
                {
                    // can't place the card here
                    cardPlaceHolder.transform.GetComponent<Renderer>().material.color = Color.black;
                }
                else
                {
                    // card is playable

                    if(Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        //Debug.LogError(cardPlaceHolder.gameObject.transform.GetChild(0).GetChild(0).name);
                        ////cardPlaceHolder.currentCard = gameplayManager.cardSelected;
                        //cardPlaceHolder.currentCard.power = gameplayManager.cardSelected.power;
                        //cardPlaceHolder.currentCard.image = gameplayManager.cardSelected.image;
                        //cardPlaceHolder.currentCard.mana = gameplayManager.cardSelected.mana;
                        //
                        ////Debug.LogError(cardPlaceHolder.gameObject.transform.GetChild(0).GetChild(0).name);
                        //cardPlaceHolder.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 255f);
                        //cardPlaceHolder.gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = gameplayManager.cardSelected.image;

                        cardPlaceHolder.PlaceACard(gameplayManager.cardSelected);
                        cardPlaceHolder.transform.GetComponent<Renderer>().material.color = cardPlaceHolder.defaultColor;
                        gameplayManager.placementPhase = false;
                        gameplayManager.attackPhase = true;

                    }

                    if (!pointingAtPlaceHolder)
                    {
                        pointingAtPlaceHolder = true;
                        cardPlaceHolder.transform.GetComponent<Renderer>().material.color = Color.green;
                    }
                }
            }
        }
        else
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, backgroundLayer))
            {
                if (pointingAtPlaceHolder)
                {
                    pointingAtPlaceHolder = false;
                    if (cardPlaceHolder != null)
                    {
                        cardPlaceHolder.transform.GetComponent<Renderer>().material.color = cardPlaceHolder.defaultColor;
                        // stopped pointing at the place holder
                    }
                    cardPlaceHolder = null;
                }
            }
        }
    }
    */
}
