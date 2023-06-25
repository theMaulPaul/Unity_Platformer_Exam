using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExit : MonoBehaviour
{
    public void GameFinished()
    {
        Application.Quit();
    }
}
