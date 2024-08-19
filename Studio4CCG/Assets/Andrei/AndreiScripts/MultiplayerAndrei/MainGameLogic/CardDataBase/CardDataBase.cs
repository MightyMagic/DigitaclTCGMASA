using AndreiMultiplayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    [SerializeField] int startingHandSize;
    [SerializeField] int maxHandSize;

    [SerializeField] List<DeckData> deckLists;

    public CardsInHandData[] playerHands = new CardsInHandData[2];

    public List<ActiveDeckList> activeDecks = new List<ActiveDeckList>();

    LobbyData lobbyData;

    private void Start()
    {
        lobbyData = FindObjectOfType<LobbyData>();

        activeDecks.Clear();

        if(lobbyData != null)
        {
            for(int i = 0; i < playerHands.Length; i++)
            {
                playerHands[i].playerName = lobbyData.PlayersData[i].Name;

                activeDecks.Add(new ActiveDeckList());
                activeDecks[i].playerName = lobbyData.PlayersData[i].Name;
            }
        }
    }

    public void PopulateDeck(DeckChoicePacket packet)
    {
        //activeDecks.Clear();

        if (lobbyData != null)
        {
            for (int i = 0; i < playerHands.Length; i++)
            {
                playerHands[i].playerName = lobbyData.PlayersData[i].Name;
        
                //activeDecks.Add(new ActiveDeckList());
                activeDecks[i].playerName = lobbyData.PlayersData[i].Name;
            }
        }


        string playerName = packet.playerData.Name;
        int deckIndex = packet.deckIndex;

        for (int i = 0; i < activeDecks.Count; i++)
        {
            if (activeDecks[i].playerName == playerName)
            {
                activeDecks[i].deckList.Clear();

                //Append the deck to an active deck list
                for (int j = 0; j < deckLists[deckIndex].CardsInDeck.Count; j++)
                {
                    activeDecks[i].deckList.Add(new CardInfo());
                    activeDecks[i].deckList[j].CardName = deckLists[deckIndex].CardsInDeck[j].CardName;
                    activeDecks[i].deckList[j].Id = deckLists[deckIndex].CardsInDeck[j].CardId;
                }

                PopulateHand(playerName, startingHandSize, packet.playerData);
                // (Sending the packet with new cards within the same method)
            }
        }
    }

    public void PopulateHand(string playerName, int amountOfCards, PlayerData playerData)
    {
        int playerIndex = 0;

        // Finding the players hand object
        for (int i = 0; i < playerHands.Length; i++)
        {
            if(playerHands[i].playerName == playerName)
            {
                playerIndex = i;
            }
        }

        List<CardInfo> list = new List<CardInfo>();

        // Finding the correspondent player deck and drawing cards from it
        for (int i = 0; i < activeDecks.Count; i++)
        {
            if (activeDecks[i].playerName == playerName)
            {
                for (int j = 0; j < amountOfCards; j++)
                {
                    list.Add(Draw(activeDecks[i]));
                }
                
            }
        }

        // Adding new cards to hand
        int emptyCardIndex = 0;
        // spotting empty card space
        for (int i = 0; i < maxHandSize; i++)
        {
            if (playerHands[playerIndex].cardsinHand[i].Id == -1)
            {
                emptyCardIndex = i;
                break;
            }
        }
        // inserting cards in remaining slots
        int handIndexer = emptyCardIndex;
        int additionalIndex = 0;

        while(handIndexer < maxHandSize & additionalIndex < list.Count)
        {
            playerHands[playerIndex].cardsinHand[handIndexer].Id = list[additionalIndex].Id;
            playerHands[playerIndex].cardsinHand[handIndexer].CardName = list[additionalIndex].CardName;

            handIndexer++;
            additionalIndex++;
        }

        // Send newly acquired cards in hand to client
        for(int i = 0; i < list.Count; i++)
        {
            Server.instance.SendPacketsToAllClients(new CardDrawPacket(playerData, list[i], playerName).Serialize());
        }

    }

    public CardInfo Draw(ActiveDeckList playerDeck)
    {
        CardInfo cardInfo = new CardInfo();

        cardInfo.CardName = playerDeck.deckList[playerDeck.deckList.Count - 1].CardName;
        cardInfo.Id = playerDeck.deckList[playerDeck.deckList.Count - 1].Id;

        playerDeck.deckList.RemoveAt(playerDeck.deckList.Count - 1);

        return cardInfo;
    }
}

[System.Serializable]
public class DeckData
{
    public string deckName;
    public List<CardScriptableObject> CardsInDeck = new List<CardScriptableObject>();
}
