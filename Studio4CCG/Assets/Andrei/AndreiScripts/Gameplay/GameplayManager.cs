using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public bool cardSelectionPhase = true;
    public bool placementPhase = false;
    public bool attackPhase = false;

    public bool myTurn = false;

    [SerializeField] TextMeshProUGUI debugText;

    [Header("Logic")]
    [SerializeField] MouseTracking trackingLogic;
    public GameObject cardsInHand;
    [SerializeField] Board boardLogic;

    //public CardObject cardObjectSelected;
    public CardSO cardSelected;

    void Start()
    {
        debugText.color = Color.red;
        debugText.text = "Waiting for your turn, select the card";

        cardsInHand.SetActive(true);
    }

    void Update()
    {
       // if(Input.GetKeyDown(KeyCode.E) & !myTurn)
       // {
       //     myTurn = true;
       //     placementPhase = true;
       // }

        if (myTurn)
        {
            if(placementPhase)
            {
                debugText.text = "Placement phase";
                TurnPhase();
            }
            else if (attackPhase)
            {
                debugText.text = "Press R to play another card";
                //AttackPhase();
                StartCoroutine(boardLogic.AttackSequence());
                attackPhase = false;
            }
            else
            {
                debugText.text = "For some reason, nothing is happening";
            }
        }
    }

    void TurnPhase()
    {
        trackingLogic.TrackMouse();
    }

    void AttackPhase()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            attackPhase = false;
            placementPhase = false;
            cardsInHand.SetActive(true) ;
        }
    }

    public void AppendNewCard()
    {

    }
}
