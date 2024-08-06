using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePhase : MonoBehaviour
{
    //[HideInInspector]
    public bool myTurn;
    public TextMeshProUGUI phaseUI;
    public Animator screenUIAnimator;
    DeckDrawCard deckDrawCard;
    // Update is called once per frame

    private void Start()
    {
        deckDrawCard = GameObject.FindAnyObjectByType<DeckDrawCard>();
    }
    void Update()
    {
        
    }

    public void DrawPhase()
    {
        GameManager.Instance.DrawPhase();


    }
    public void PlayPhase()
    {

        GameManager.Instance.PlayPhase();

    }
    public void AttackPhase()
    {

        GameManager.Instance.AttackPhase();

    }
    public void EndTurn()
    {
        GameManager.Instance.EndTurn();
    }


}
