using TMPro;
using UnityEngine;

public class Level1UIController : MonoBehaviour
{
    public static Level1UIController Instance;
    [SerializeField] private TMP_Text objective1;
    [SerializeField] private TMP_Text objective2;

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

    void Start()
    {
        objective1.text = $"Destroy Asteroid: 0/{Level1Manager.Instance.maxAsteroidCount}";
        objective2.text = $"Collect {Level1Manager.Instance.scoreToWin} Stars: 0/{Level1Manager.Instance.scoreToWin}";
    }
    public void UpdateObjective1(int currentObjective, int maxObjective)
    {
        if (currentObjective < maxObjective)
        {
            objective1.text = $"Destroy Asteroid: {currentObjective}/{maxObjective}";
        }
        else
        {
            Level1Manager.Instance.obj1Complete = true;
            objective1.color = Color.green;
            objective1.text = $"Destroy Asteroid: Complete";
        }
    }

    public void UpdateObjective2(int currentObjective, int maxObjective)
    {
        if (currentObjective >= maxObjective)
        {
            Level1Manager.Instance.obj2Complete = true;
            objective2.color = Color.green;
            objective2.text = $"Collect Star: Complete";
        }
        else
        {
            objective2.text = $"Collect {maxObjective} Stars: {currentObjective}/{maxObjective}";
        }
    }
}
