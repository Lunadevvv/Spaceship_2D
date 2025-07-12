using System.Collections.Generic;
using UnityEngine;

public class WaveObjectSpawner : MonoBehaviour
{
    public static WaveObjectSpawner Instance;

    [SerializeField] private float spawnX = 10f;
    [SerializeField] private GameObject bonusClaimPrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private int waveNumber;
    [SerializeField] private List<Wave> waves;

    public bool isSpawnBoss = false;

    private float bonusSpawnTimer;
    private float bonusSpawnInterval = 1f;

    [System.Serializable]
    public class Wave
    {
        public GameObject prefab;
        public float spawnTimer;
        public float spawnInterval;
        public int objectsPerWave;
        public int spawnedObjectCount;
    }

    public void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Update()
    {
        bonusSpawnTimer += Time.deltaTime;
        if (bonusSpawnTimer >= bonusSpawnInterval)
        {
            SpawnBonusClaim();
            bonusSpawnTimer = 0f;
        }

        if (waveNumber < waves.Count)
        {
            waves[waveNumber].spawnTimer -= Time.deltaTime;
            if (waves[waveNumber].spawnTimer <= 0)
            {
                waves[waveNumber].spawnTimer += waves[waveNumber].spawnInterval;
                SpawnObject();
            }
            if (waves[waveNumber].spawnedObjectCount >= waves[waveNumber].objectsPerWave)
            {
                if (IsWaveClear())
                {
                    waveNumber++;
                }
            }
        }
        else
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        if (!isSpawnBoss)
        {
            Instantiate(bossPrefab, new Vector3((float)10.6, 0, 0), Quaternion.Euler(0,0,-90));
            Level2UIController.Instance.EnableBossHealthSlider();
            isSpawnBoss = true;
        }
    }

    private bool IsWaveClear()
    {
        bool isClear = false;
        if (GameObject.FindGameObjectsWithTag("Obstacle").Length <= 0 && GameObject.FindGameObjectsWithTag("Critter").Length <= 0)
        {
            isClear = true;
        }

        return isClear;
    }

    private void SpawnObject()
    {
        if (waves[waveNumber].spawnedObjectCount >= waves[waveNumber].objectsPerWave)
            return;
        Instantiate(waves[waveNumber].prefab, RandomSpawnPoint(), transform.rotation, transform);
        waves[waveNumber].spawnedObjectCount++;
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        float randomY = Random.Range(-4.5f, 4.5f);
        float offsetX = Random.Range(0f, 0.5f); // spawn bên phải màn hình
        spawnPoint = new Vector3(spawnX + offsetX, randomY);

        return spawnPoint;
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
