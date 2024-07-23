using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhase : MonoBehaviour
{
    //[HideInInspector]
    public bool myTurn;


    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawPhase()
    {
        //Draw
        PlayPhase();
    }
    void PlayPhase()
    {
        // Start Timer 
        // UI indecation
        //Notify The server 

    }
    public void AttackPhase()
    {

        //UI indecation
        //Notify The server 
    }
    public void EndTurn()
    {
        myTurn = false;
        //Disable Playing Cards. only interactions.
        //Notify The server 
        //Reset timer.

    }


}
