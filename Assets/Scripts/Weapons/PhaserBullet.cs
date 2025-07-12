using UnityEngine;

public class PhaserBullet : MonoBehaviour
{
    PhaserWeapon weapon;
    private float speed;
    private float maxDistance = 11f;
    private Vector3 startPos;
    private Vector3 direction;

    private void OnEnable()
    {
        weapon = PhaserWeapon.Instance;
        speed = weapon.stats[weapon.weaponLevel].speed;
        startPos = transform.position;
        direction = Vector3.right; // luôn bay bên phải
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * direction;

        if (transform.position.x > maxDistance)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;
        if (target.CompareTag("Obstacle"))
        {
            Asteroid asteroid = target.GetComponent<Asteroid>();
            if (asteroid)
                asteroid.TakeDamage(weapon.stats[weapon.weaponLevel].damage);
        }
        else if (target.CompareTag("Critter"))
        {
            Critter critter = target.GetComponent<Critter>();
            if (critter != null) critter.OnHitByBullet();
        }
        else if (target.CompareTag("Boss"))
        {
            Boss1 boss = target.GetComponent<Boss1>();
            if (boss != null) boss.TakeDamage(weapon.stats[weapon.weaponLevel].damage);
        }
        gameObject.SetActive(false); // Deactivate the bullet instead of destroying it
    }
}
