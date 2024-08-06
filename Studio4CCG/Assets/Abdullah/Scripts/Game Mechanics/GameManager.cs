using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //my playing deck carried from Hosted room.
    DeckDrawCard deckDrawCard;
    


    [Header("Timer")]
    public static GameManager Instance;
    public TextMeshProUGUI timerUI;
    public float timer;
    public float maxTimer;
        
    [Header("Player")]
    [Range(0, 10)]
    public int maxMana;
    [HideInInspector]
    public int mana;
    public TextMeshProUGUI manaUI;

    [HideInInspector]
    public int health;
    [Range(0, 10)]
    public int maxHealth;
    public TextMeshProUGUI healthUI;

    [Header("Phases")]
    //[HideInInspector]
    public bool myTurn;
    public TextMeshProUGUI phaseUI;
    public Animator screenUIAnimator;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        deckDrawCard = FindAnyObjectByType<DeckDrawCard>();
        healthUI.text = "Health: " + maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            timer = 0;

        }
        else
        {

            timer -= 1 * Time.deltaTime;
            timerUI.text = "Timer: " + timer.ToString("0");


        }
    }
    public void UpdateHealth(int number)
    {
        health += number;
        if (health <= 0)
        {
            // gameOver 
        }

    }
    public void UpdateMana(int number)
    {

        if (mana - number >= 0) 
        { 
        mana -= number;
        }
        else
        {
            Debug.LogError("Mana is blow 0 = " + (mana - number));
        }

    }


    //Phases
    public void FirstRound()
    {



    }
    public void DrawPhase()
    {
        //Reset Timer
        timer = maxTimer;

        // UI indecation
        screenUIAnimator.SetTrigger("NextPhase");
        phaseUI.text = "Draw Phase";
        //Draw
        //deckDrawCard.DrawCard();

        //Reset Mana
        mana = maxMana;
        manaUI.text = "Mana: " + mana;

        // PlayPhase();


    }
    public void PlayPhase()
    {

        //Reset Timer
        timer=maxTimer;
        // Start Timer 

        // UI indecation
        screenUIAnimator.SetTrigger("NextPhase");
        phaseUI.text = "Play Phase";

        //Notify The server 


    }
    public void AttackPhase()
    {
        //Reset Timer
        timer = maxTimer;

        //UI indecation
        screenUIAnimator.SetTrigger("NextPhase");
        phaseUI.text = "Attack Phase";

        //Notify The server 


    }
    public void EndTurn()
    {
        //mana zero
        mana = 0;
        manaUI.text = "Mana: " + mana;
        myTurn = false;


        //Disable Playing Cards. only interactions.
        screenUIAnimator.SetTrigger("NextPhase");
        phaseUI.text = "End Turn";

        //Notify The server 


        //Reset timer.

    }



}
