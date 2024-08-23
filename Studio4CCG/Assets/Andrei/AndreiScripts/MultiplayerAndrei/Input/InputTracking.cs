using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTracking : MonoBehaviour
{
    [Header("Current Card")]
    public CardOnBoard currentCard;
    public int indexInColumn;
    public ColumnName column;

    [Header("Objects determining the card placement")]
    [SerializeField] LayerMask columnMask;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject middleButton;
    [SerializeField] GameObject rightButton;

    GameObject currentColumn;

    [Header("Card play input")]
    [SerializeField] List<Button> cardButtons;

    [Header("Utilities")]
    [SerializeField] MainGameManager mainGameManager;
    [SerializeField] ClientCardDatabase cardDb;
    [SerializeField] BoardData boardData;
    [SerializeField] MainGameManager gameManager;

    public bool choseCard;
    public bool placedCard;
    
    void Start()
    {
        // Temporary card is null
        currentCard = new CardOnBoard();
        currentCard.cardInfo = new CardInfo();
        currentCard.cardInfo.CardName = "";
        currentCard.cardInfo.Id = -1;

        choseCard = false;
        placedCard = false;

        // DisableColumns
        EnableColumns(false);
        //leftButton.gameObject.SetActive(false);

    }

    
    void Update()
    {
        if (leftButton.gameObject.activeInHierarchy)
        {
            TrackMouseHover();
        }
    }

    void TrackMouseHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, columnMask))
        {
            currentColumn = hit.collider.gameObject;
            Debug.Log("Hovered over: " + currentColumn.name);

            // Ideally I should check whether the column is avaliable for card placement
            //BoardPlaceHolder bPh = 

            if(currentColumn.GetComponent<Renderer>().material.color != Color.green)
            {
                currentColumn.GetComponent<Renderer>().material.color = Color.green;
            }

            if(Input.GetMouseButtonDown(0) & currentCard.cardInfo.Id != -1)
            {
                Debug.LogError("Played a card!");
                // Make card appear on board
                if(currentColumn.name == "LeftColumn")
                {
                    boardData.PlaceCard(ColumnName.Left, currentCard.cardInfo.CardName);
                }
                else if(currentColumn.name == "RightColumn")
                {
                    boardData.PlaceCard(ColumnName.Right, currentCard.cardInfo.CardName);
                }
                else if(currentColumn.name == "MiddleColumn")
                {
                    boardData.PlaceCard(ColumnName.Middle, currentCard.cardInfo.CardName);
                }

                // Reset UI
                currentColumn = null;
                DisableCardButtons(true);
                EnableColumns(false);

                // Reset the current card
                currentCard.cardInfo.Id = -1;
                currentCard.cardInfo.CardName = "";   
            }
        }
        else
        {
            if(currentColumn != null)
            {
                currentColumn.GetComponent<Renderer>().material.color = Color.white;
            }
            currentColumn = null; 
        }
    }

    public void CardChosen(CardUIObject cardObject)
    {
        // Check for mana from hand
        int mana = cardDb.ManaByName(cardObject.CardName.text);
        // Populate card object here
        if (mana <= gameManager.currentMana)
        {
            if (currentCard.cardInfo.Id == -1)
            {
                currentCard.cardInfo.CardName = cardObject.CardName.text;
                currentCard.cardInfo.Id = cardDb.IdByName(currentCard.cardInfo.CardName);
            }


            // Clean up hand
            for (int i = 0; i < cardDb.playerHandData.cardsinHand.Length; i++)
            {
                if(i == cardObject.handIndex)
                {
                    cardDb.playerHandData.cardsinHand[i].CardName = "";
                    cardDb.playerHandData.cardsinHand[i].Id = -1;

                    cardObject.gameObject.SetActive(false);
                }
            }

            // Readjust mana
            gameManager.currentMana -= mana;
            gameManager.gameUI.UpdateOffline(boardData.myHp, gameManager.currentMana);

            // Enable column buttons, disable CardButtons
            DisableCardButtons(false);
            EnableColumns(true);
        }
        else
        {
            Debug.LogError("No mana, bro!");
        }
    }

    public void DisableCardButtons(bool disable)
    {
        for (int i = 0; i < cardButtons.Count; i++)
        {
            cardButtons[i].gameObject.SetActive(disable);
        }
    }

    void EnableColumns(bool disable)
    {
        leftButton.gameObject.SetActive(disable);
        rightButton.gameObject.SetActive(disable);
        middleButton.gameObject.SetActive(disable);
    }
}
