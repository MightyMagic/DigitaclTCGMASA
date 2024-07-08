using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCards : MonoBehaviour
{
    [SerializeField] string cardID;
        string characters= "zxcvbnmasdfghjklqwertyuiop1234567890";
    [SerializeField] int health = 0;
    [SerializeField] int defense = 0;
    [SerializeField] int attack=0;

    public IBaseEffect BaseEffect;
    public CardState cardState=CardState.deck;

    private void Awake()
    {
        for (int i = 0; i < 24; i++)
        {
            cardID += characters[Random.Range(0, characters.Length)];
        }
        Debug.Log(cardID);
    }
    public int EffectStats(int value, StatesList enumValue)
    {
        switch (enumValue)
        {
            case StatesList.health:
                    return health = CalculateNewState(health, value);
            case StatesList.attack:
                    return attack = CalculateNewState(attack, value);
            case StatesList.defense:
                    return defense = CalculateNewState(defense, value);


            default:
                Debug.LogError("Unknown stat: " + enumValue);
                return 0;
        }

        //send updated card to the server
    }

    //Request the server to update 
    //other cards effect it
    public void UpdateCardState(CardState newState)
    {
        cardState = newState;
        switch (newState)
        {
            case CardState.hand:

                 gameObject.tag="OnHands";
                break;
            case CardState.graveyard:

                gameObject.tag = "Grave";
                break;
            case CardState.deck:

                gameObject.tag = "Deck";
                break;
            case CardState.field:

                gameObject.tag = "Tile";
                break;





                default:
                Debug.LogWarning("Unkown State from CardState");
                break;

        }
    }
    
    private int CalculateNewState(int myState, int value)
    {
        myState = myState + value;
        if (myState < 0) myState = 0;
        return myState;
    }
}


    
