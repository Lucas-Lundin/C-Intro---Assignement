using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour
{
    private bool touchingTerrain;


    
     void OnCollisionStay(Collision collisionInfo)
    {
        Debug.LogError("touchingTerrain: " + touchingTerrain);
        if (collisionInfo.gameObject.layer == 3)
        {
            touchingTerrain = true;
        }
        else 
        {
            touchingTerrain = false;
        }
        
    }




    private void Update()
    {
        
    }
}
