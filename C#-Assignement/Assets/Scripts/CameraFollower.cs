using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject areanaCenter;

    [SerializeField] private float playerOffsetX;
    [SerializeField] private float playerOffsetY;
    [SerializeField] private float playerOffsetZ;

    [SerializeField] private float ArenaOffsetX;
    [SerializeField] private float ArenaOffsetY;
    [SerializeField] private float ArenaOffsetZ;




    void Update()
    {
       

        if (0 == TurnManager.GetInstance().GetTurnIndex())
        { 
            Follow(TurnManager.GetInstance().GetCurrentPlayerObjElseNext(), 0.008f);
        }
        else
        {
            Follow(TurnManager.GetInstance().GetCurrentPlayerObjElseNext(), 0.02f);
        }
    }

    void Follow(GameObject targetObject, float lerpSpeed)
    {
         

        float targetX = targetObject.transform.position.x + playerOffsetX;
        float targetY = transform.position.y + playerOffsetY;
        float targetZ = targetObject.transform.position.z + playerOffsetZ;

        Vector3 targetV3 = new Vector3(targetX, targetY, targetZ);

        transform.position = Vector3.Lerp(transform.position, targetV3, lerpSpeed);

    }




}
