using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class FoodSpawnManager : MonoBehaviour
{
    private int defualtAmount = 5;
    //private int extraAmount = 10;
    private int arenaRadius = 20;
    [SerializeField] private GameObject prefabToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnFoodBetweenTurns(int amountToSpawnFromTurns)
    {
        for (int i = 0; i < (defualtAmount + amountToSpawnFromTurns); i++)
        {
            
            GameObject spawnedPrefab;
            spawnedPrefab = Instantiate(prefabToSpawn, new Vector3(0,10,0), transform.rotation);
            // Set a random direction:
            spawnedPrefab.transform.rotation = Quaternion.Euler( new Vector3(0, Random.Range(0,360), 0));
            // Set a random length from middle:
            spawnedPrefab.transform.Translate(Vector3.forward * Random.Range(0,arenaRadius));

        }
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
}
