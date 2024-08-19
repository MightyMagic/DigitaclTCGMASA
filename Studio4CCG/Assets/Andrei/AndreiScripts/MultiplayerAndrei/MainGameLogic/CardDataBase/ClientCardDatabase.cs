using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientCardDatabase : MonoBehaviour
{
    [SerializeField] int startingHandSize;
    [SerializeField] int maxHandSize;

    [SerializeField] MainGameUI gameUI;

    [SerializeField] List<DeckData> deckLists;

    public CardsInHandData playerHandData = new CardsInHandData();

    //public ActiveDeckList activeDeck = new ActiveDeckList();

    private void Start()
    {
        playerHandData.playerName = PlayerInformation.Instance.PlayerData.Name;

        ClientNetworkManager.Instance.DrewNewCardEvent += OnCardDraw;
    }

    void OnCardDraw(CardDrawPacket cdp)
    {
        string playerName = cdp.playerName;
        int cardId = cdp.cardInfo.Id;
        string cardName = cdp.cardInfo.CardName;

        if(PlayerInformation.Instance.PlayerData.Name == playerName)
        {
            for(int i = 0; i < maxHandSize; i++)
            {
                if (playerHandData.cardsinHand[i].Id == -1)
                {
                    playerHandData.cardsinHand[i].Id = cardId;
                    playerHandData.cardsinHand[i].CardName = cardName;
                    break;
                }
            }

            gameUI.UpdateCards(this.playerHandData);
        }
    }
}
