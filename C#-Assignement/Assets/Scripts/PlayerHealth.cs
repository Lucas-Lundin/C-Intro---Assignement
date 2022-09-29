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
    public float currentHP = 5;
    [SerializeField] private GameObject playerDeathParticle;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfDead();

        //Clamp current health to not go over max or under 0: 
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);
        
        // Update ui:
        ui_HP_Number.text = "HP: " +currentHP;
        ui_HP_bar.fillAmount = currentHP / maxHP;

        //Test to take damage:
        if (Input.GetKeyDown(KeyCode.Y))
        {
            currentHP -= 1;  
        }
    }

    public void ModifyHP (float changeAmount)
    {
        currentHP += changeAmount;
    }

    public void CheckIfDead()
    { 
    if (currentHP <= 0)
        {
            //particle at death position: 
            Instantiate(playerDeathParticle, transform.position, Quaternion.identity);

            transform.position = new Vector3(0, 8, 0);
            ScoreManager.GetInstance().ModifyScorePlayerObj(gameObject, -10);
            currentHP = maxHP;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<PlayerController>().UncontrolablePostDeathSet();

            //particle at respawn position: 
            Instantiate(playerDeathParticle, transform.position + new Vector3(0, -3, 0), Quaternion.identity);

        }
    }



}
