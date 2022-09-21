using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI ui_HP_Number;
    public Image ui_HP_bar;
    private float startingHP = 100;
    private float currentHP = 50;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = startingHP;
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = Mathf.Clamp(currentHP, 0f, startingHP);

        ui_HP_Number.text = "HP: " +currentHP;
        ui_HP_bar.fillAmount = currentHP / 100;

        if (Input.GetKeyDown(KeyCode.T))
        {
            currentHP -= 10;
            
        }
    }
}
