using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float worldSpeed; 
    private int score = 0;
    private int asteroidCount;

    public int scoreToWin;
    public int scoreToAdd;
    public bool obj1Complete = false; // Objective 1 complete flag
    public bool obj2Complete = false; // Objective 2 complete flag

    public int maxAsteroidCount; // Maximum number of asteroids to spawn

    private void Awake() //Will run when the script instance is being loaded
    {
        if (Instance != null) //Instance already existed. Make sure not duplicate
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (obj2Complete && obj1Complete)
        {
            StartCoroutine(Level1CompleteCoroutine());
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UIController.Instance.UpdateScore(score);
        UIController.Instance.UpdateObjective2(score, scoreToWin); // Assuming 100 is the max score objective
    }

    public void AddAsteroidCount()
    {
        asteroidCount ++;
        UIController.Instance.UpdateObjective1(asteroidCount, maxAsteroidCount); // Assuming 10 is the max objective
    }

    public void Pause()
    {
        if(UIController.Instance.pausePanel.activeSelf == false)
        {
            UIController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
            AudioManager.Instance.PlaySound(AudioManager.Instance.pause);
        }
        else
        {
            UIController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
            AudioManager.Instance.PlaySound(AudioManager.Instance.pause);
            PlayerController.Instance.ExitBoost();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }
    IEnumerator Level1CompleteCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level1Complete");
    }
}
