using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{

    public void Start()
    {
        GameManager.Instance.timer = PlayerPrefs.GetFloat("CurrentTimer", 0);
        PhaserWeapon.Instance.weaponLevel = PlayerPrefs.GetInt("WeaponLevel", 0);
    }

    public void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Boss").Length <= 0 && WaveObjectSpawner.Instance.isSpawnBoss)
        {
            StartCoroutine(Level2CompleteCoroutine());
        }
    }

    IEnumerator Level2CompleteCoroutine()
    {
        GameManager.Instance.isGameRunning = false;
        PlayerPrefs.SetFloat("CurrentTimer", GameManager.Instance.timer);
        if (GameManager.Instance.timer < PlayerPrefs.GetFloat("HighestScore", 0))
        {
            PlayerPrefs.SetFloat("HighestScore", GameManager.Instance.timer);
            PlayerPrefs.Save();
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level2Complete");
    }
}
