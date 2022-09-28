using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private int belongsToPlayerIndex;
    [SerializeField] private GameObject belongsToPlayerObj;
    [SerializeField] private GameObject scoreIndicatorPrefab;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Food")
        {
            ScoreManager.GetInstance().ModifyScorePlayerIndex(belongsToPlayerIndex, 1);
            

            // Spaawn a scoreing indicator:
            float spawnX = belongsToPlayerObj.transform.position.x + Random.Range(-1f,1f);
            float spawnY = belongsToPlayerObj.transform.position.y;
            float spawnZ = belongsToPlayerObj.transform.position.z + Random.Range(-1f, 1f);
            Vector3 spawnV3 = new Vector3(spawnX, spawnY, spawnZ);

            GameObject scoreIndicatorSpawned;
            scoreIndicatorSpawned = Instantiate(scoreIndicatorPrefab, spawnV3, Quaternion.identity);
            scoreIndicatorSpawned.GetComponent<ScoreIndicator>().ChangeColor(belongsToPlayerIndex);


            //destroy:
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
