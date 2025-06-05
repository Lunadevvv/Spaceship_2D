using UnityEngine;

public class PhaserWeapon : Weapon
{
    public static PhaserWeapon Instance; // Create an instance to use public variable (Singleton)

    //[SerializeField] private GameObject phaserPrefab; // Reference to the Phaser prefab
    [SerializeField] private ObjectPool bulletPool; // Reference to the bullet pool for Phaser bullets

    public void Awake()
    {
        if (Instance != null) // Instance already existed. Make sure not duplicate
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Shoot()
    {
        //Instantiate(phaserPrefab, transform.position, transform.rotation);
        GameObject bullet = bulletPool.GetPooledObject(); // Get a bullet from the pool
        bullet.transform.position = transform.position; // Set the bullet's position to the weapon's position
        bullet.SetActive(true); // Activate the bullet
    }
}
