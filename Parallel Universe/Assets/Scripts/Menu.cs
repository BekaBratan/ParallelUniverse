using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
