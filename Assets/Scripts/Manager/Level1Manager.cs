using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager Instance;    

    [Header("Objective Settings")]
    public int scoreToWin;         // số sao cần đạt
    public int scoreToAdd;         // số sao cộng mỗi lần
    public int maxAsteroidCount;   // số thiên thạch cần phá

    private int starScore = 0;
    private int asteroidCount = 0;

    [NonSerialized] public bool obj1Complete = false;
    [NonSerialized] public bool obj2Complete = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PlayerPrefs.SetFloat("CurrentTimer", 0);
        PlayerPrefs.SetInt("WeaponLevel", 0);
    }

    private void Update()
    {
        if (obj1Complete && obj2Complete)
        {
            GameManager.Instance.isGameRunning = false;
            PlayerPrefs.SetFloat("CurrentTimer", GameManager.Instance.timer);
            PlayerPrefs.SetInt("WeaponLevel", PhaserWeapon.Instance.weaponLevel);
            StartCoroutine(Level1CompleteCoroutine());
        }
    }

    public void AddScore(int amount)
    {
        starScore += amount;
        Level1UIController.Instance.UpdateObjective2(starScore, scoreToWin);
    }

    public void AddAsteroidCount()
    {
        asteroidCount++;
        Level1UIController.Instance.UpdateObjective1(asteroidCount, maxAsteroidCount);
    }

    IEnumerator Level1CompleteCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level1Complete");
    }
}