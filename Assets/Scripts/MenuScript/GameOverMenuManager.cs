using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        Time.timeScale = 1f; // Ensure time scale is normal when starting the game
        UpdateHighScore(PlayerPrefs.GetFloat("CurrentTimer", 0));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void UpdateHighScore(float highScore)
    {
        int minutes = Mathf.FloorToInt(highScore / 60f);
        int seconds = Mathf.FloorToInt(highScore % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
