using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private int turnIndex = 0; // Noone = 0, Player1 = 1, Player2 = 2.
    private int nextPlayerInLine = 1; //Stores the next players turn. 
    [SerializeField] private GameObject[] turnObjects;

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
    // End of singleton stuff.

    private void Start()
    {
        GetComponent<FoodSpawnManager>().SpawnFoodBetweenTurns(); // Spawn food at the start of the game.
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TurnManager.GetInstance().ChangeTurn(); // Change turn.
            
        }
    }

    
    public void ChangeTurn()
    {
        /* Changes turn when callled: 
        0 = noones turn
        1 = player 1
        0 = noones turn
        2 = player 2
        and repeating...
        */
        if (turnIndex != 0)  // If a player had the turn (1,2), change the turnIndex to be short pause (0). 
        {
            turnIndex = 0;
            GetComponent<FoodSpawnManager>().SpawnFoodBetweenTurns(); // Spawn food between turns.
        }
        else                 // If a pause was active (0), change to the next player in line. 
        {
            turnIndex = nextPlayerInLine;

            // Que the player to be next in  varibale nextPlayerInLine.
            if (nextPlayerInLine == 1)   
            {
                nextPlayerInLine = 2;
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

    public GameObject GetCurrentObj()  // Return the GameObject whose turn it is. 
    {     
        return turnObjects[turnIndex];
    }

    public GameObject GetNextPlayerObj() // Return the player GameObject whose next in turn. 
    {
        return turnObjects[nextPlayerInLine];
    }

    public GameObject GetCurrentPlayerObjElseNext() // Return the player GameObject whose next in turn. 
    {
        if (turnIndex != 0)
        {
            return turnObjects[turnIndex];
        }
        else 
        {
            return turnObjects[nextPlayerInLine];
        }

    }

    public GameObject IfNoonesTurnGetNextPlayerObj() // If it's noones turn, Return the player GameObject whose next in turn. 
    {
        if (turnIndex == 0)
        {
            return turnObjects[nextPlayerInLine];
        }
        else
        {
            return turnObjects[0];
        }
    }

    public GameObject GetTurnObjectByIndex(int arrayIndex)
    {
        return turnObjects[arrayIndex];
    }



}




