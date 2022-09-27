using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private int belongsToPlayerIndex;
    [SerializeField] private GameObject belongsToPlayerObj;


    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Food")
        {
            ScoreManager.GetInstance().ModifyScorePlayerIndex(belongsToPlayerIndex, 1);
            Destroy(collisionInfo.gameObject);

            
        }
        PlayerDieToMonster("Player1", collisionInfo);
        PlayerDieToMonster("Player2", collisionInfo);
    }



    private void PlayerDieToMonster(string objTag, Collision collisionInfoP)
    {
        if (collisionInfoP.gameObject.tag == objTag) // Dies to monster
        {
            collisionInfoP.gameObject.GetComponent<PlayerHealth>().currentHP = 0;
        }
    }

}
