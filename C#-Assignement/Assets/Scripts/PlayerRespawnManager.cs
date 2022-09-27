using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnManager : MonoBehaviour
{
    // Singelton stuff 
    private static PlayerRespawnManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public static PlayerRespawnManager GetInstance()
    {
        return instance;
    }
    // End of singleton stuff.








}
