using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int turnIndex = 0; // Noone = 0, Player1 = 1, Player2 = 2 
    private int nextPlayerInLine = 1; //Stores the next players turn. 
    private int numbersOfPlayers = 2; 
 

    // Singelton stuff 
    private static TurnManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public static TurnManager GetInstance()
    {
        return instance;
    }


    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.R))
        {
            
           
            TurnManager.GetInstance().ChangeTurn();
        }
    }

    
    public void ChangeTurn()
    {
     /* Changes turn when callled: 
     0 = paused
     1 = player 1
     0 = paused
     2 = player 2
     and repeating...
     */
        if (turnIndex != 0)  // If a player had the turn (1,2), change the turnIndex to be short pause (0). 
        {
            turnIndex = 0;
        }
        else                 // If a pause was active (0), change to the next player in line. 
        {
            turnIndex = nextPlayerInLine;

            // Que the player to be next in  varibale nextPlayerInLine.
            if (nextPlayerInLine < numbersOfPlayers)   
            {
                nextPlayerInLine++;
            }
            else
            {
                nextPlayerInLine = 1;
            }

        }


    }

    public int GetTurnIndex()
    {
        return turnIndex;
    }

    public int GetNextPlayerInLine()
    {
        return nextPlayerInLine;
    }
}




