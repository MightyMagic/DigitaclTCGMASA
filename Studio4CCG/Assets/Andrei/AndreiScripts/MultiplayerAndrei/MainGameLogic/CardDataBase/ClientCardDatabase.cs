using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientCardDatabase : MonoBehaviour
{
    [SerializeField] int startingHandSize;
    [SerializeField] int maxHandSize;

    [SerializeField] MainGameUI gameUI;

    public List<DeckData> deckLists;

    public CardsInHandData playerHandData = new CardsInHandData();

    //public ActiveDeckList activeDeck = new ActiveDeckList();

    private void Start()
    {
        playerHandData.playerName = PlayerInformation.Instance.PlayerData.Name;

        ClientNetworkManager.Instance.MultipleCardDrawEvent += OnManyCardDraw;
        // Add function for a single card draw
    }

    void OnManyCardDraw(MultipleCardDrawPacket mcp)
    {
        string playerName = mcp.playerName;
        CardInfo[] newCards = new CardInfo[mcp.cards.Length];

        for(int i = 0; i < mcp.cards.Length; i++)
        {
            newCards[i] = new CardInfo();
            newCards[i].Id = mcp.cards[i].Id;
            newCards[i].CardName = mcp.cards[i].CardName;
        }

        Debug.LogError("New cards array size is " + newCards.Length);

        int startIndexer = 0;
        int cardIndexer = 0;

        if(PlayerInformation.Instance.PlayerData.Name == playerName)
        {
            Debug.LogError("These cards belong to us!");

            for(int i = 0; i < maxHandSize; i++)
            {
                if (playerHandData.cardsinHand[i].Id == -1)
                {
                    startIndexer = i;
                    break;
                }
            }

            while(startIndexer < maxHandSize & cardIndexer < newCards.Length)
            {
                playerHandData.cardsinHand[startIndexer].Id = newCards[cardIndexer].Id;
                playerHandData.cardsinHand[startIndexer].CardName = newCards[cardIndexer].CardName;

                cardIndexer++;
                startIndexer++;
            }

            gameUI.UpdateCards(this.playerHandData);
        }
    }
}
