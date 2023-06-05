using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesMethods : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}