using UnityEngine;
using UnityEngine.SceneManagement;

public class InstrucionMenuManager : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f; // Ensure time scale is normal when starting the game
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
