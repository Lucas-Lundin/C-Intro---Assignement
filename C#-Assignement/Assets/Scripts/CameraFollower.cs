using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private float offsetZ;
    private bool ab = false;
    private float diffX;
    private float diffY;
    private float diffZ;

    private float player1posZ;
    private float camposZ;
    private float camplayer1diff;

    private void Awake()
    {
        
    }
    void Start()
    {
        float diffX = transform.position.x - player1.transform.position.x;
        float diffY = transform.position.y;
        float diffZ = player1.transform.position.z - transform.position.z;

        
        ab = true;
    }

    void Update()
    {
        if (ab)
        {
            float player1posZ = player1.transform.position.z;
            float camposZ = transform.position.z;
            float camplayer1diff = player1posZ - camposZ;


            FollowPlayerOne();
            
            Debug.Log("camplayer1diff: " + camplayer1diff + "   player1posZ: " + player1posZ + "    camposZ: " + camposZ);
        }   
    }

    void FollowPlayerOne()
    {
        
        //transform.position = new Vector3(player1.transform.position.x + offsetX, transform.position.y, player1.transform.position.z + offsetZ);
        transform.Translate(0f, 0f, camplayer1diff, Space.World);
    }

}
