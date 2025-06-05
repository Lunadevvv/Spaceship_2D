using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner Instance;

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

    [SerializeField] private GameObject asteroidPrefab;
    public GameObject starPrefab;
    [SerializeField] private float spawnInterval;
    private float spawnTimer;

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnObject();
            spawnTimer = 0f;
        }
    }

    void SpawnObject()
    {
        float randomY = Random.Range(-4.3f, 4.1f);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);
        Instantiate(asteroidPrefab, spawnPosition, transform.rotation);
        if (Random.value < 0.1f) // 10% chance to spawn a star
        {
            Instantiate(starPrefab, spawnPosition, transform.rotation);
        }
    }
}
