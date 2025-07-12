using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    [Header("Prefabs to Spawn")]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject bonusClaimPrefab;
    public GameObject starPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnX = 10f;
    [SerializeField] private float spawnInterval = 0.35f; // spawn nhanh hơn
    private float spawnTimer;
    [SerializeField] private int asteroidPerSpawn = 1; // ít asteroid hơn

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnAsteroids(asteroidPerSpawn);
            SpawnStars();
            SpawnBonusClaim();
            spawnTimer = 0f;
        }
    }

    void SpawnAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float randomY = Random.Range(-4.5f, 4.5f);
            float offsetX = Random.Range(0f, 2f); // spawn bên phải màn hình
            Vector3 spawnPos = new Vector3(spawnX + offsetX, randomY, 0f);
            Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
        }
    }

    void SpawnStars()
    {
        if (Random.value < 0.2f) // 20% xác suất spawn sao
        {
            float randomY = Random.Range(-4.5f, 4.5f);
            Vector3 spawnPos = new Vector3(spawnX + 1f, randomY, 0f);
            Instantiate(starPrefab, spawnPos, Quaternion.identity);
        }
    }

    void SpawnBonusClaim()
    {
        if (Random.value < 0.05f) // 5% xác suất spawn bonus
        {
            float randomY = Random.Range(-4.5f, 4.5f);
            Vector3 spawnPos = new Vector3(spawnX + 1f, randomY, 0f);
            Instantiate(bonusClaimPrefab, spawnPos, Quaternion.identity);
        }
    }
}
