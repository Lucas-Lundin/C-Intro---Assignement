using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaRotateButton : MonoBehaviour
{
    [SerializeField] private GameObject ScoreAreas;
    [SerializeField] private int rotateNegativeOrPosative = 1;
    private float rotationSpeed = 15f;

    void OnTriggerStay(Collider collisionInfo)
    {
        Debug.Log("hej");
        if (collisionInfo.gameObject.tag == "Player1" || collisionInfo.gameObject.tag == "Player2")
        {
            ScoreAreas.transform.Rotate(0f, rotationSpeed * rotateNegativeOrPosative * Time.deltaTime, 0, Space.World);
        }
    }


        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
