using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTurn : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    public bool IsMyTurn()
    {
        if (TurnManager.GetInstance().GetTurnIndex() == playerIndex)
           
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool PausedButMyTurnNext()
    {
        if (TurnManager.GetInstance().GetTurnIndex() == playerIndex)
        {
            return true;
        }
        else if (TurnManager.GetInstance().GetNextPlayerInLine() == playerIndex && TurnManager.GetInstance().GetTurnIndex() == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }




}
