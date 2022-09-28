using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void GotoPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
