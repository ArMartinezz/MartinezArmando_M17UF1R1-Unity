using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameManager gameManager;
    public void StartGame()
    {
        gameManager.ChangeScene("Level-1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
