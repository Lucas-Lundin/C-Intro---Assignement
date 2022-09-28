//using System.Collections;
//using System.Collections.Generic;
//using System.Drawing;
using System.Globalization;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class TurnManager : MonoBehaviour
{
    private int turnIndex = 0; // Noone = 0, Player1 = 1, Player2 = 2.
    private int nextPlayerInLine = 1; //Stores the next players turn. 
    [SerializeField] private GameObject[] turnObjects;

    [SerializeField] private  TextMeshProUGUI ui_SwitchPlayerNextUp;
    [SerializeField] private TextMeshProUGUI ui_SwitchPlayerPlayerX;
    [SerializeField] private Image ui_SwitchPlayerBG;
    private float fadeMuliplierNextPlayerUI;
    [SerializeField] private Image ui_SwitchPlayerTimerR;
    [SerializeField] private Image ui_SwitchPlayerTimerV;
    [SerializeField] public Color Player1Color;
    [SerializeField] public Color Player2Color;
    [SerializeField] private Image ui_TurnTimerCircle;

    private float turnTimePlayer = 5.5f;
    private float turnTimeNoone = 2.5f;
    public float turnTimeCurrentLeft = 0;

    public int numberOfTurns = 0;

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
        //GetComponent<FoodSpawnManager>().SpawnFoodBetweenTurns(); // Spawn food at the start of the game.
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TurnManager.GetInstance().ChangeTurn(); // Change turn.
            
        }

        RenderNextPlayerUI();
        AutoChangeTurnWithTimer();

    }

    private void AutoChangeTurnWithTimer()
    {
        turnTimeCurrentLeft -= 1 * Time.deltaTime;
        if (turnTimeCurrentLeft <= 0.0f)
        {
            ChangeTurn();
        }
    }

    private void RenderNextPlayerUI() 
    {
        if (GetTurnIndex() == 0)
        {
            ui_TurnTimerCircle.fillAmount = Mathf.Clamp01(turnTimeCurrentLeft / turnTimeNoone);
            ui_TurnTimerCircle.color = new Vector4(0f, 0f, 0f, 0.35f * fadeMuliplierNextPlayerUI);
        }
        if (GetTurnIndex() == 1)
        {
            ui_TurnTimerCircle.fillAmount = Mathf.Clamp01(turnTimeCurrentLeft / turnTimePlayer);
            ui_TurnTimerCircle.color = Player1Color;
        }
        if (GetTurnIndex() == 2)
        {
            ui_TurnTimerCircle.fillAmount = Mathf.Clamp01(turnTimeCurrentLeft / turnTimePlayer);
            ui_TurnTimerCircle.color = Player2Color;
        }


            if (GetTurnIndex() == 0)
        {
            fadeMuliplierNextPlayerUI = Mathf.Lerp(fadeMuliplierNextPlayerUI, 1, 0.1f);

            ui_SwitchPlayerNextUp.alpha = 1 * fadeMuliplierNextPlayerUI;
            ui_SwitchPlayerPlayerX.alpha = 1 * fadeMuliplierNextPlayerUI;
            ui_SwitchPlayerBG.color = new Vector4(0f, 0f, 0f, 0.15f * fadeMuliplierNextPlayerUI);

            ui_SwitchPlayerPlayerX.text = "PLAYER " + GetNextPlayerInLine();
            //ui_SwitchPlayerTimerR.fillAmount = Mathf.Clamp01(turnTimeCurrentLeft/turnTimeNoone);
            //ui_SwitchPlayerTimerV.fillAmount = Mathf.Clamp01(turnTimeCurrentLeft / turnTimeNoone);
            if (GetNextPlayerInLine() == 1)
            {
                ui_SwitchPlayerPlayerX.color = Player1Color;
            }
            if (GetNextPlayerInLine() == 2)
            {
                ui_SwitchPlayerPlayerX.color = Player2Color;
            }

        }
        else 
        {
            fadeMuliplierNextPlayerUI = 0f;

            ui_SwitchPlayerNextUp.alpha = 0;
            ui_SwitchPlayerPlayerX.alpha = 0;
            ui_SwitchPlayerBG.color = new Vector4(0f, 0f, 0f, 0.0f);
            ui_SwitchPlayerTimerR.fillAmount = 0f;
            ui_SwitchPlayerTimerV.fillAmount = 0f;
        }
    }

    
    public void ChangeTurn()
    {
        // Give the players new bullets:
        turnObjects[1].GetComponent<PlayerController>().bulletsBlack = 1;
        turnObjects[2].GetComponent<PlayerController>().bulletsBlack = 1;
        turnObjects[1].GetComponent<PlayerController>().bulletsWhite = 1;
        turnObjects[2].GetComponent<PlayerController>().bulletsWhite = 1;

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
            turnTimeCurrentLeft = turnTimeNoone;
            GetComponent<FoodSpawnManager>().SpawnFoodBetweenTurns(numberOfTurns); // Spawn food between turns.
        
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
                numberOfTurns += 1; // Add one to the number of turns had. 
            }
            turnTimeCurrentLeft = turnTimePlayer;
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




