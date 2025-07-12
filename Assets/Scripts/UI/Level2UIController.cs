
using UnityEngine;
using UnityEngine.UI;

public class Level2UIController : MonoBehaviour
{
    public static Level2UIController Instance;
    [SerializeField] private GameObject bossHealth;
    [SerializeField] private Slider bossHealthSlider;

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

    public void UpdateBossHealthSlider(float healthCurrent, float healthMax)
    {
        bossHealthSlider.maxValue = healthMax;
        bossHealthSlider.value = Mathf.RoundToInt(healthCurrent);
    }

    public void EnableBossHealthSlider()
    {
        bossHealth.SetActive(true);
    }
}
