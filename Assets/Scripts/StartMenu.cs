using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void LaunchGame()
    {
        SceneManager.LoadScene("Lvl 1");
    }
}
