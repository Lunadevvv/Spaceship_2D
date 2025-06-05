using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance; 

    [SerializeField] private Slider energySlider;
    [SerializeField] private TMP_Text energyText;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    public GameObject pausePanel;
    [SerializeField] private TMP_Text scoreText; // Assuming you have a TextMeshPro text for score

    [SerializeField] private TMP_Text objective1;
    [SerializeField] private TMP_Text objective2;

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

    private void Start()
    {
        objective1.text = $"Destroy Asteroid: 0/{GameManager.Instance.maxAsteroidCount}";
        objective2.text = $"Collect {GameManager.Instance.scoreToWin} Stars: 0/{GameManager.Instance.scoreToWin}";
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

    public void UpdateScore(int score)
    {
        // Assuming you have a UI Text element to display the score
        // You can add a TMP_Text field and update it here
        scoreText.text = $"{score}";
    }

    public void UpdateObjective1(int currentObjective, int maxObjective)
    {
        if(currentObjective < maxObjective)
        {
            objective1.text = $"Destroy Asteroid: {currentObjective}/{maxObjective}";
        }
        else
        {
            GameManager.Instance.obj1Complete = true; // Mark objective 1 as complete
            objective1.color = Color.green;
            objective1.text = $"Destroy Asteroid: Complete";
        }
    }

    public void UpdateObjective2(int currentObjective, int maxObjective)
    {
        if (currentObjective >= maxObjective)
        {
            GameManager.Instance.obj2Complete = true; // Mark objective 2 as complete
            objective2.color = Color.green;
            objective2.text = $"Collect Star: Complete";
        }
        else
        {
            objective2.text = $"Collect {maxObjective} Stars: {currentObjective}/{maxObjective}";
        }
    }

}
