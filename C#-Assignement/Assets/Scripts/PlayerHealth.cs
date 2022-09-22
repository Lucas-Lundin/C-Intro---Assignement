using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI ui_HP_Number;
    public Image ui_HP_bar;
    private float maxHP = 5;
    private float currentHP = 5;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);

        ui_HP_Number.text = "HP: " +currentHP;
        ui_HP_bar.fillAmount = currentHP / maxHP;

        if (Input.GetKeyDown(KeyCode.T))
        {
            currentHP -= 10;
            
        }
    }

    public void ModifyHP (float changeAmount)
    {
        currentHP += changeAmount;
    }


}
