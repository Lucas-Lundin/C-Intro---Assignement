using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScoring : MonoBehaviour
{
    [SerializeField] private GameObject scoreAreaP1;
    [SerializeField] private GameObject scoreAreaP2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "ScoreAreaP1")
        {
            ScoreManager.GetInstance().ModifyScorePlayer1(1);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
