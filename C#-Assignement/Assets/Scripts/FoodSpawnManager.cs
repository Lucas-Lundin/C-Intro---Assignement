using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class FoodSpawnManager : MonoBehaviour
{
    private int defualtAmount = 5;
    //private int extraAmount = 10;
    private int arenaRadius = 20;
    [SerializeField] private GameObject prefabToSpawn;
    private int spawnWhenBehindAmountSet = 12;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnFoodBetweenTurns(int amountToSpawnFromTurns)
    {
        int spawnWhenBehindAmount = 0;
        // If the next turn is played by someone who si behind in score, spawn more food:
        if (TurnManager.GetInstance().GetNextPlayerInLine() == 1)
        {
            if (ScoreManager.GetInstance().scorePlayer1 < ScoreManager.GetInstance().scorePlayer2)
            {
                spawnWhenBehindAmount = spawnWhenBehindAmountSet;
            }
        }

        if (TurnManager.GetInstance().GetNextPlayerInLine() == 2)
        {
            if (ScoreManager.GetInstance().scorePlayer2 < ScoreManager.GetInstance().scorePlayer1)
            {
                spawnWhenBehindAmount = spawnWhenBehindAmountSet;
            }
        }


        Vector3 cluster_diraction_random = new  Vector3(0f, Random.Range(0f, 360f), 0f);
        float cluster_range_from_mid_random = Random.Range(0, arenaRadius-3);

        for (int i = 0; i < (defualtAmount + amountToSpawnFromTurns + spawnWhenBehindAmount); i++)
        {
            
            GameObject spawnedPrefab;
            spawnedPrefab = Instantiate(prefabToSpawn, new Vector3(0,10,0), transform.rotation);
            
            // Set a random direction:
            //spawnedPrefab.transform.rotation = Quaternion.Euler( new Vector3(0, Random.Range(0,360), 0));
            spawnedPrefab.transform.rotation = Quaternion.Euler(cluster_diraction_random);


            // Set a random length from middle:
            spawnedPrefab.transform.Translate(Vector3.forward * cluster_range_from_mid_random);

            // Random a small different position around the cluster center:
            spawnedPrefab.transform.position = spawnedPrefab.transform.position + new Vector3(Random.Range(-3, 3), Random.Range(-1, 1), Random.Range(-3, 3));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
}
