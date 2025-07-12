using Unity.VisualScripting;
using UnityEngine;

public class PhaserWeapon : Weapon
{
    public static PhaserWeapon Instance;

    [SerializeField] private ObjectPool bulletPool;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void Shoot()
    {
        for (int i = 0; i < stats[weaponLevel].amount; i++)
        {
            GameObject bullet = bulletPool.GetPooledObject(); // Get a bullet from the pool
            float yPos = transform.position.y;
            if (stats[weaponLevel].amount > 1)
            {
                float spacing = stats[weaponLevel].range / (stats[weaponLevel].amount - 1);
                yPos = transform.position.y - (stats[weaponLevel].range / 2) + i * spacing;
            }
            bullet.transform.position = new Vector2(transform.position.x, yPos); // Set the bullet's position to the weapon's position
            bullet.SetActive(true); // Activate the bullet
            
        }
    }
}
