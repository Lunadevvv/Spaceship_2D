using UnityEngine;

public class PhaserBullet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(PhaserWeapon.Instance.speed * Time.deltaTime, 0f);
        if (transform.position.x > 9f) // Assuming 10f is the right boundary of the game area
        {
            //Destroy(gameObject); // Destroy the bullet when it goes out of bounds
            gameObject.SetActive(false); // Deactivate the bullet instead of destroying it
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid)
            {
                asteroid.TakeDamage(PhaserWeapon.Instance.damage);
            }

            gameObject.SetActive(false); // Deactivate the bullet instead of destroying it
        }
    }
}
