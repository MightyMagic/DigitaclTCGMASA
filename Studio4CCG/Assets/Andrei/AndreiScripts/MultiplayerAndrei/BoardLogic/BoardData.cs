using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class BoardData : MonoBehaviour
{
    public List<BoardPlaceHolder> ActiveBoardPlacholders = new List<BoardPlaceHolder>();
    public BoardHalf playersHalf;
    public BoardHalf enemyHalf;

    [Header("Player Stats")]
    public int myHp;
    public int enemyHp;
    [SerializeField] TextMeshProUGUI myHpText;
    [SerializeField] TextMeshProUGUI enemyHpUI;

    [SerializeField] InputTracking inputListener;
    [SerializeField] ClientCardDatabase cardDB;
    [SerializeField] MainGameManager mainGameManager;

    [SerializeField] List<BoardPlaceHolder> AllBoardPlaceHolders;

    private void Start()
    {
        ClientNetworkManager.Instance.PlayersStatesUpdatedEvent += NameEnemyHalf;
        ClientNetworkManager.Instance.BoardStateUpdatedEvent += FetchBoard;
        ClientNetworkManager.Instance.HealthReceivedEvent += FetchNewHP;

        for (int i = 0; i < playersHalf.playerBoard.Count; i++)
        {
            playersHalf.playerBoard[i].ownerName = PlayerInformation.Instance.PlayerData.Name;
        }

        playersHalf.playerName = PlayerInformation.Instance.PlayerData.Name;

        // Cleanup the board 
        RefreshBoard();
    }

    public void AttackPhase()
    {
        // Iterate through first row and attack
        for(int i = 0; i < playersHalf.playerBoard.Count;i++)
        {
            if (playersHalf.playerBoard[i].rowIndex == 0)
            {
                if (playersHalf.playerBoard[i].isOccupied)
                {
                    switch (playersHalf.playerBoard[i].column)
                    {
                        case ColumnName.Left:
                            for (int j = 0; j < enemyHalf.playerBoard.Count; j++)
                            {
                                if (enemyHalf.playerBoard[j].rowIndex == 0 && enemyHalf.playerBoard[j].column == ColumnName.Right)
                                {
                                    playersHalf.playerBoard[i].cardOnBoard.currentHP -= enemyHalf.playerBoard[j].cardOnBoard.currentAttack;
                                    enemyHalf.playerBoard[j].cardOnBoard.currentHP -= playersHalf.playerBoard[i].cardOnBoard.currentAttack;
                                    if (enemyHalf.playerBoard[j].rowIndex == 0 && !enemyHalf.playerBoard[j].isOccupied)
                                    {
                                        // Damage player
                                        enemyHp -= playersHalf.playerBoard[i].cardOnBoard.currentAttack;
                                    }
                                }
                            }
                            break;
                        case ColumnName.Right:
                            for (int j = 0; j < enemyHalf.playerBoard.Count; j++)
                            {
                                if (enemyHalf.playerBoard[j].rowIndex == 0 && enemyHalf.playerBoard[j].column == ColumnName.Left)
                                {
                                    playersHalf.playerBoard[i].cardOnBoard.currentHP -= enemyHalf.playerBoard[j].cardOnBoard.currentAttack;
                                    enemyHalf.playerBoard[j].cardOnBoard.currentHP -= playersHalf.playerBoard[i].cardOnBoard.currentAttack;
                                    if (enemyHalf.playerBoard[j].rowIndex == 0 && !enemyHalf.playerBoard[j].isOccupied)
                                    {
                                        // Damage player
                                        enemyHp -= playersHalf.playerBoard[i].cardOnBoard.currentAttack;
                                    }
                                }
                            }

                            break;
                        case ColumnName.Middle:
                            for (int j = 0; j < enemyHalf.playerBoard.Count; j++)
                            {
                                if (enemyHalf.playerBoard[j].rowIndex == 0 && enemyHalf.playerBoard[j].column == ColumnName.Middle)
                                {
                                    playersHalf.playerBoard[i].cardOnBoard.currentHP -= enemyHalf.playerBoard[j].cardOnBoard.currentAttack;
                                    enemyHalf.playerBoard[j].cardOnBoard.currentHP -= playersHalf.playerBoard[i].cardOnBoard.currentAttack;
                                    if (enemyHalf.playerBoard[j].rowIndex == 0 && !enemyHalf.playerBoard[j].isOccupied)
                                    {
                                        // Damage player
                                        enemyHp -= playersHalf.playerBoard[i].cardOnBoard.currentAttack;
                                    }
                                }   
                            }
                            break;
                    }

                    enemyHpUI.text = enemyHalf.playerName + " HP is " + enemyHp;

                }
            }
        }

        for(int i = 0; i < enemyHalf.playerBoard.Count; i++)
        {
            if (enemyHalf.playerBoard[i].rowIndex == 0 && enemyHalf.playerBoard[i].isOccupied)
            {
                for (int j = 0; j < playersHalf.playerBoard.Count; j++)
                {
                    if(!playersHalf.playerBoard[j].isOccupied)
                    {
                        if (playersHalf.playerBoard[j].column == ColumnName.Left && enemyHalf.playerBoard[i].column == ColumnName.Right)
                        {
                            myHp -= enemyHalf.playerBoard[i].cardOnBoard.currentAttack;
                            break;
                        }
                        else if(playersHalf.playerBoard[j].column == ColumnName.Right && enemyHalf.playerBoard[i].column == ColumnName.Left)
                        {
                            myHp -= enemyHalf.playerBoard[i].cardOnBoard.currentAttack;
                            break;
                        }
                        else if (playersHalf.playerBoard[j].column == ColumnName.Middle && enemyHalf.playerBoard[i].column == ColumnName.Middle)
                        {
                            myHp -= enemyHalf.playerBoard[i].cardOnBoard.currentAttack;
                            break;
                        }

                    }
                }
            }

            myHpText.text = "HP: " + myHp;
        }

        // Displaying board again
        RefreshBoard();
    }

    public void CheckForGameOver()
    {
        if(myHp <= 0)
        {
            ClientNetworkManager.Instance.SendPacket(new GameOverPacket(PlayerInformation.Instance.PlayerData, enemyHalf.playerName).Serialize());
        }
        else if(enemyHp <= 0)
        {
            ClientNetworkManager.Instance.SendPacket(new GameOverPacket(PlayerInformation.Instance.PlayerData, playersHalf.playerName).Serialize());
        }
    }

    public void SyncHP()
    {
        ClientNetworkManager.Instance.SendPacket(new HealthPacket(PlayerInformation.Instance.PlayerData, myHp, playersHalf.playerName, enemyHp, enemyHalf.playerName).Serialize());
    }

    public void FetchNewHP(HealthPacket hPacket)
    {
        Debug.LogError("Receieved hp packet, updating stats");
        if(hPacket.myName != PlayerInformation.Instance.PlayerData.Name)
        {
            myHp = hPacket.enemyHp;
            enemyHp = hPacket.myHp;

            myHpText.text = "HP: " + hPacket.enemyHp;
            enemyHpUI.text = enemyHalf.playerName + " HP is " + hPacket.myHp;
        }

        mainGameManager.currentHp = enemyHp;   

        // Here implement game over conditions
    }

    public void BoardCleanup()
    {
        // Iterate once more to move cards forward if something died
        for (int i = 0; i < playersHalf.playerBoard.Count; i++)
        {
            for (int j = 0; j < playersHalf.playerBoard.Count; j++)
            {
                if (playersHalf.playerBoard[i].rowIndex == 0 && !playersHalf.playerBoard[i].isOccupied)
                {
                    if (playersHalf.playerBoard[j].rowIndex == 1 && playersHalf.playerBoard[j].column == playersHalf.playerBoard[i].column && playersHalf.playerBoard[j].isOccupied)
                    {
                        // exchange them
                        playersHalf.playerBoard[i].cardOnBoard = new CardOnBoard(playersHalf.playerBoard[j].cardOnBoard.cardInfo, playersHalf.playerBoard[j].cardOnBoard.currentAttack,
                            playersHalf.playerBoard[j].cardOnBoard.currentHP);
                        playersHalf.playerBoard[i].isOccupied = true;

                        // empty this one
                        playersHalf.playerBoard[j].isOccupied = false;
                        playersHalf.playerBoard[j].cardOnBoard = new CardOnBoard();
                        playersHalf.playerBoard[j].cardOnBoard.cardInfo = new CardInfo();
                        playersHalf.playerBoard[j].cardOnBoard.currentAttack = 0;
                        playersHalf.playerBoard[j].cardOnBoard.currentHP = 0;
                        playersHalf.playerBoard[j].cardOnBoard.cardInfo.Id = -1;
                        playersHalf.playerBoard[j].cardOnBoard.cardInfo.CardName = "";

                        break;
                    }
                }
            }
        }


        for (int i = 0; i < enemyHalf.playerBoard.Count; i++)
        {
            for (int j = 0; j < enemyHalf.playerBoard.Count; j++)
            {
                if (enemyHalf.playerBoard[i].rowIndex == 0 && !enemyHalf.playerBoard[i].isOccupied)
                {
                    if (enemyHalf.playerBoard[j].rowIndex == 1 && enemyHalf.playerBoard[j].column == enemyHalf.playerBoard[i].column && enemyHalf.playerBoard[j].isOccupied)
                    {
                        // exchange them
                        enemyHalf.playerBoard[i].cardOnBoard = new CardOnBoard(enemyHalf.playerBoard[j].cardOnBoard.cardInfo, enemyHalf.playerBoard[j].cardOnBoard.currentAttack,
                            enemyHalf.playerBoard[j].cardOnBoard.currentHP);
                        enemyHalf.playerBoard[i].isOccupied = true;

                        // empty this one
                        enemyHalf.playerBoard[j].isOccupied = false;
                        enemyHalf.playerBoard[j].cardOnBoard = new CardOnBoard();
                        enemyHalf.playerBoard[j].cardOnBoard.cardInfo = new CardInfo();
                        enemyHalf.playerBoard[j].cardOnBoard.currentAttack = 0;
                        enemyHalf.playerBoard[j].cardOnBoard.currentHP = 0;
                        enemyHalf.playerBoard[j].cardOnBoard.cardInfo.Id = -1;
                        enemyHalf.playerBoard[j].cardOnBoard.cardInfo.CardName = "";
                        break;
                    }
                }
            }
        }

        RefreshBoard();
    }

    public BoardPlaceHolder[] ActivePlaceholders()
    {
        // List to store active placeholders
        List<BoardPlaceHolder> activePlaceHolders = new List<BoardPlaceHolder>();

        // Iterate over all placeholders
        for (int i = 0; i < AllBoardPlaceHolders.Count; i++)
        {
            var placeholder = AllBoardPlaceHolders[i];
            if (placeholder != null && placeholder.isOccupied)
            {
                // Add the existing placeholder to the list
                activePlaceHolders.Add(placeholder);
            }
            //else if(!placeholder.isOccupied)
            //{
            //    placeholder.gameObject.SetActive(false);
            //}
        }

        // Return the list as an array
        Debug.LogError("Fetching all active placeholdrs, there are " + activePlaceHolders.Count + " at the moment");
        return activePlaceHolders.ToArray();
        
    }

    public void RefreshBoard()
    {
        BoardPlaceHolder[] boardPlaceHolders = ActivePlaceholders();
        ActiveBoardPlacholders.Clear();

        for (int i = 0;i < boardPlaceHolders.Length; i++)
        {
            if (!boardPlaceHolders[i].isOccupied)
            {
                boardPlaceHolders[i].gameObject.SetActive(false);
            }
            else
            {
                // ActiveBoardPlacholders.Add(new BoardPlaceHolder(boardPlaceHolders[i].isOccupied, boardPlaceHolders[i].column, boardPlaceHolders[i].rowIndex,
                //     new CardOnBoard(new CardInfo(boardPlaceHolders[i].cardOnBoard.cardInfo.Id, boardPlaceHolders[i].cardOnBoard.cardInfo.CardName), 
                //     boardPlaceHolders[i].cardOnBoard.currentAttack, boardPlaceHolders[i].cardOnBoard.currentHP), boardPlaceHolders[i].ownerName));

                if (boardPlaceHolders[i].cardOnBoard.currentHP <= 0)
                {
                    boardPlaceHolders[i].isOccupied = false;
                    boardPlaceHolders[i].gameObject.SetActive(false);

                }
                else
                {
                    ActiveBoardPlacholders.Add(boardPlaceHolders[i]);
                }
            }
        }

        for(int i = 0; i < ActiveBoardPlacholders.Count; i++)
        {
            ActiveBoardPlacholders[i].gameObject.SetActive(true);
            ActiveBoardPlacholders[i].UpdateUI();
        }
    }

    public void SendBoardToServer()
    {
        // Updating board for both clients
        if (ActiveBoardPlacholders.Count > 0)
        {
            ClientNetworkManager.Instance.SendPacket(CaptureBoard().Serialize());
        }
    }

    public BoardStatePacket CaptureBoard()
    {
        //
        RefreshBoard();
        int numberOfCards = ActiveBoardPlacholders.Count;
        CardOnBoardInfo[] cardsToPack = new CardOnBoardInfo[ActiveBoardPlacholders.Count];
        string playerName = PlayerInformation.Instance.PlayerData.Name;


        for (int i = 0; i < ActiveBoardPlacholders.Count; i++)
        {
            cardsToPack[i] = new CardOnBoardInfo(ActiveBoardPlacholders[i].ownerName, ActiveBoardPlacholders[i].cardOnBoard, ActiveBoardPlacholders[i].column, ActiveBoardPlacholders[i].rowIndex); 
        }

        BoardStatePacket bsp = new BoardStatePacket(PlayerInformation.Instance.PlayerData,
            playerName, numberOfCards, cardsToPack);

        return bsp;
    }

    public void FetchBoard(BoardStatePacket bsp)
    {
        Debug.LogError("Receieved an updated board state, there are " + bsp.numberOfCards + " active cards on board");
        if(bsp.playerName != PlayerInformation.Instance.PlayerData.Name)
        {
            //ActiveBoardPlacholders.Clear();
            // Zero all card placeholders
            for(int i = 0; i < playersHalf.playerBoard.Count; i++)
            {
                playersHalf.playerBoard[i].isOccupied = false;
                playersHalf.playerBoard[i].cardOnBoard.cardInfo = new CardInfo();
                playersHalf.playerBoard[i].cardOnBoard.currentAttack = 0;
                playersHalf.playerBoard[i].cardOnBoard.currentHP = 0;
                playersHalf.playerBoard[i].cardOnBoard.cardInfo.Id = -1;
                playersHalf.playerBoard[i].cardOnBoard.cardInfo.CardName = "";
                //playersHalf.ow
            }

            for (int i = 0; i < enemyHalf.playerBoard.Count; i++)
            {
                enemyHalf.playerBoard[i].isOccupied = false;
                enemyHalf.playerBoard[i].cardOnBoard.cardInfo = new CardInfo();
                enemyHalf.playerBoard[i].cardOnBoard.currentAttack = 0;
                enemyHalf.playerBoard[i].cardOnBoard.currentHP = 0;
                enemyHalf.playerBoard[i].cardOnBoard.cardInfo.Id = -1;
            }


            for (int i = 0; i < bsp.numberOfCards; i++)
            {
                Debug.LogError("Processing card belonging to " + bsp.cardsOnBoard[i].OwnerName);
                Debug.LogError("Card is located in row " + bsp.cardsOnBoard[i].rowIndex + " in column " + bsp.cardsOnBoard[i].column);

                if (bsp.cardsOnBoard[i].OwnerName == playersHalf.playerName)
                {
                    for(int j = 0; j < playersHalf.playerBoard.Count; j++)
                    {
                        if (bsp.cardsOnBoard[i].column == playersHalf.playerBoard[j].column && playersHalf.playerBoard[j].rowIndex == bsp.cardsOnBoard[i].rowIndex)
                        {
                            // Found the matching card
                            playersHalf.playerBoard[j].isOccupied = true;
                            playersHalf.playerBoard[j].cardOnBoard = new CardOnBoard(bsp.cardsOnBoard[i].cardOnBoard.cardInfo, bsp.cardsOnBoard[i].cardOnBoard.currentAttack,
                                bsp.cardsOnBoard[i].cardOnBoard.currentHP);

                            Debug.LogError("Name of the card is " + bsp.cardsOnBoard[i].cardOnBoard.cardInfo.CardName + ", it belongs to " + bsp.cardsOnBoard[i].OwnerName);
                            Debug.LogError("The card belonging to " + playersHalf.playerBoard[j].ownerName + " is added to the half of " + PlayerInformation.Instance.PlayerData.Name);
                            Debug.LogError("Card stats are: Attack: " + bsp.cardsOnBoard[i].cardOnBoard.currentAttack + " / HP: " + bsp.cardsOnBoard[i].cardOnBoard.currentHP);
                        }
                    }
                }
                else
                {
                    // enemy cards
                    for (int j = 0; j < enemyHalf.playerBoard.Count; j++)
                    {
                        if (bsp.cardsOnBoard[i].column == enemyHalf.playerBoard[j].column && enemyHalf.playerBoard[j].rowIndex == bsp.cardsOnBoard[i].rowIndex)
                        {
                            // Found the matching card
                            enemyHalf.playerBoard[j].isOccupied = true;
                            enemyHalf.playerBoard[j].cardOnBoard = new CardOnBoard(bsp.cardsOnBoard[i].cardOnBoard.cardInfo, bsp.cardsOnBoard[i].cardOnBoard.currentAttack,
                                bsp.cardsOnBoard[i].cardOnBoard.currentHP);

                            Debug.Log("Updated enemy board placeholder at index " + j);
                            Debug.LogError("The card belonging to " + playersHalf.playerBoard[j].ownerName + " is added to the half of " + enemyHalf.playerName);
                        }
                    }
                }
            }


        }

        RefreshBoard();
    }

    public void PlaceCard(ColumnName columnName, string cardName)
    {
        int index = 0;

        for (int i = 0; i < playersHalf.playerBoard.Count; i++)
        {
            if (playersHalf.playerBoard[i].column == columnName)
            {
                if(playersHalf.playerBoard[i].rowIndex == index & !playersHalf.playerBoard[i].isOccupied)
                {
                    // Populate the board piece
                    BoardPlaceHolder bPh = playersHalf.playerBoard[i];

                    bPh.gameObject.SetActive(true);

                    CardInfo pendingCard = new CardInfo();
                    pendingCard.CardName = cardName;
                    pendingCard.Id = cardDB.IdByName(cardName);

                    bPh.PopulatePlaceholder(pendingCard, cardDB.AttackByName(cardName), cardDB.CardHPByName(cardName));
                    bPh.UpdateUI();


                    RefreshBoard();
                    break;
                }
                else
                {
                    index++;
                }
            }
        }

        SendBoardToServer();
    }

    void NameEnemyHalf(PlayersStatesPacket psp)
    {
        // Update player stats
        for (int i = 0; i < 2; i++)
        {
            if (psp.playerStates[i] != null)
            {
                if (psp.playerStates[i].playerName != PlayerInformation.Instance.PlayerData.Name)
                {
                    for (int j = 0; j < playersHalf.playerBoard.Count; j++)
                    {
                        enemyHalf.playerBoard[j].ownerName = psp.playerStates[i].playerName;
                    }

                    // Fetching info about the player
                    enemyHalf.playerName = psp.playerStates[i].playerName;
                    enemyHp = psp.playerStates[i].currentHp;
                    enemyHpUI.text = enemyHalf.playerName + " HP is " + psp.playerStates[i].currentHp;
                }
                else
                {
                  myHp = psp.playerStates[i].currentHp;
                }
            }
        }
    }

}

public enum CardEffect
{
    SpawnTwoAdjacent,
    BuffClass,
    None
}

public enum CardClass
{
    Neutral,
    Totem,
    Knight,
    Zombie,
    None
}

public enum ColumnName
{
    Left,
    Middle,
    Right
}
