using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreIndicator : MonoBehaviour
{
    public TextMeshProUGUI ui_ScoreIndicatorNumberTMP;
    private float lifeTimerCurrent = 1f;
    private float moveSpeed = 2;

    private void Start()
    {
        //lifeTimerCurrent = 1f;
    }

    private void Update()
    {
        lifeTimerCurrent -= 1 * Time.deltaTime;

        if (lifeTimerCurrent <= 0)
        {
            Destroy(gameObject);
        }

        transform.position = transform.position + new Vector3(0, moveSpeed * Time.deltaTime, 0);


    }


    // Update is called once per frame
    public void ChangeColor(int playerIndex)
    {
        if (playerIndex == 1)
        {
            ui_ScoreIndicatorNumberTMP.color = TurnManager.GetInstance().Player1Color;
        }
        if (playerIndex == 2)
        {
            ui_ScoreIndicatorNumberTMP.color = TurnManager.GetInstance().Player2Color;
        }
    }
}
