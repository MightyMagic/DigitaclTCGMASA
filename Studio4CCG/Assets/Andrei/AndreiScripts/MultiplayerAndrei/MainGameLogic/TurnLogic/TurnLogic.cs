using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnLogic : MonoBehaviour
{
    public bool isMyTurn = false;
    [SerializeField] TurnUI turnUI;
    [SerializeField] InputTracking inputListener;
    [SerializeField] BoardData boardData;

    [Header("Turn Conditions")]
    public bool turnJustStarted = false;
    public bool listeningToInput = false;

    private void Start()
    {
        // Disabling all turn related UI and subscribing to the Start Turn event
        turnUI.DeactivateTurnUI();
        inputListener.DisableCardButtons(false);
        inputListener.gameObject.SetActive(false);
        ClientNetworkManager.Instance.PlayerTurnEvent += StartTurn;

        
    }

    private void Update()
    {
        if (isMyTurn)
        {
            //if(!turnJustStarted)
            //{
            //    turnJustStarted = true;
            //    turnUI.ActivateTurnUI();
            //    listeningToInput = true;
            //}
            //
            //if(listeningToInput)
            //{
            //    // Here I process all mouse input for placing the card
            //    ProcessInput();
            //}
        }
    }

    public void StartTurn(PlayerTurnPacket ptp)
    {
        if (!isMyTurn & ptp.playersTurnName == PlayerInformation.Instance.PlayerData.Name)
        {
            isMyTurn = true;
            turnJustStarted = true;
            turnUI.ActivateTurnUI();
            inputListener.gameObject.SetActive(true);
            inputListener.DisableCardButtons(true);

            listeningToInput = true;
        }
    }

    public IEnumerator EndOfTurnCoroutine()
    {
        yield return new WaitForSeconds(1f);

        // Attack sequence
        turnUI.DisplayPhase(true, "Attack sequence...");
        boardData.AttackPhase();

        yield return new WaitForSeconds(2f);

        //boardData.BoardCleanup();

        yield return new WaitForSeconds(2f);
        boardData.SyncHP();


        yield return new WaitForSeconds(3f);
        turnUI.DisplayPhase(true, "Sending board over server...");
        boardData.SendBoardToServer();

        // Board sync again

        yield return new WaitForSeconds(6f);
        turnUI.DisplayPhase(true, "Opponent's turn");
        EndTurn();
    }

    public void EndTurnWrapper()
    {
        StartCoroutine(EndOfTurnCoroutine());
    }

    public void EndTurn()
    {
        boardData.CheckForGameOver();

        turnJustStarted = false;
        listeningToInput = false;
        isMyTurn = false;
        // Send the packet to server that my turn is over
        ClientNetworkManager.Instance.SendPacket(new PlayerTurnPacket(PlayerInformation.Instance.PlayerData, PlayerInformation.Instance.PlayerData.Name).Serialize());
        turnUI.DeactivateTurnUI();

        inputListener.DisableCardButtons(false);
        inputListener.gameObject.SetActive(false);
    }
}
