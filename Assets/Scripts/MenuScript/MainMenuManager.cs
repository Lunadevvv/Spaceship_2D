using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text highestScoreText;

    private void Start()
    {
        Time.timeScale = 1f; // Ensure time scale is normal when starting the game
        UpdateHighScore(PlayerPrefs.GetFloat("HighestScore", 0));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void UpdateHighScore(float highScore)
    {
        int minutes = Mathf.FloorToInt(highScore / 60f);
        int seconds = Mathf.FloorToInt(highScore % 60f);
        highestScoreText.text = $"{minutes:00}:{seconds:00}";
    }
}
