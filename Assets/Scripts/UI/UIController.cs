using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    public GameObject pausePanel;

    [SerializeField] private TMP_Text timeNumberText; // "00:00" hiển thị đồng hồ


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
        UpdateTimer(PlayerPrefs.GetFloat("CurrentTimer", 0)); // Khởi tạo đồng hồ
    }

    public void UpdateEnergySlider(float energyCurrent, float energyMax)
    {
        energySlider.maxValue = energyMax;
        energySlider.value = Mathf.RoundToInt(energyCurrent);
        energyText.text = $"{energySlider.value}/{energySlider.maxValue}";
    }

    public void UpdateHealthSlider(float healthCurrent, float healthMax)
    {
        healthSlider.maxValue = healthMax;
        healthSlider.value = Mathf.RoundToInt(healthCurrent);
        healthText.text = $"{healthSlider.value}/{healthSlider.maxValue}";
    }

    public void UpdateTimer(float timeElapsed)
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60f);
        int seconds = Mathf.FloorToInt(timeElapsed % 60f);
        timeNumberText.text = $"{minutes:00}:{seconds:00}";
    }

}
