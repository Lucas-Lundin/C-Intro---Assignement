using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreIndicator : MonoBehaviour
{
    public TextMeshProUGUI ui_ScoreIndicatorNumberTMP;



    // Update is called once per frame
    void ChangeColor(int playerIndex)
    {
        if (playerIndex == 1)
        {
            ui_ScoreIndicatorNumberTMP.color = TurnManager.GetInstance().Player1Color;
        }
        if (playerIndex == 2)
        {
            ui_ScoreIndicatorNumberTMP.color = TurnManager.GetInstance().Player1Color;
        }
    }
}
