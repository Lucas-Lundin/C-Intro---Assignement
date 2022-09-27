using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Singelton stuff 
    private static ScoreManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public static ScoreManager GetInstance()
    {
        return instance;
    }
    // End of singleton stuff.

    private float scorePlayer1 = 10;
    private float scorePlayer2 = 10;
    public TextMeshProUGUI ui_P1ScoreText;
    public TextMeshProUGUI ui_P2ScoreText;
    public Image ui_P1ScoreBar;
    public Image ui_P2ScoreBar;
    private float scoreToWin = 100.0f;


    public void ModifyScorePlayerIndex(int playerIndex, int amount)
    {
        if (playerIndex == 1)
        {
            scorePlayer1 += amount;
        }
        if (playerIndex == 2)
        {
            scorePlayer2 += amount;
        }

    }

    public void ModifyScorePlayerObj(GameObject playerObj, int amount)
    {
        if (playerObj == TurnManager.GetInstance().GetTurnObjectByIndex(1))
        {
            scorePlayer1 += amount;
        }
        if (playerObj == TurnManager.GetInstance().GetTurnObjectByIndex(2))
        {
            scorePlayer2 += amount;
        }

    }

    void Update()
    {   
        // Player 1 Score:
        ui_P1ScoreText.text = (scoreToWin - scorePlayer1) + " left!";
        ui_P1ScoreBar.fillAmount = scorePlayer1 / scoreToWin;
        // Player 2 Score:
        ui_P2ScoreText.text = (scoreToWin - scorePlayer2) + " left!";
        ui_P2ScoreBar.fillAmount = scorePlayer2 / scoreToWin;
    }






}
