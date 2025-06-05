using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private FlashWhite flashWhite;

    [SerializeField] private Sprite[] asteroidSprites;
    [SerializeField] private float lives;
    [SerializeField] private GameObject destroyEffect; // Reference to the destroy effect prefab
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashWhite = GetComponent<FlashWhite>();
        spriteRenderer.sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];
        rb = GetComponent<Rigidbody2D>();
        float pushX = Random.Range(-1f, 0);
        float pushY = Random.Range(-1f, 1f);
        rb.linearVelocity = new Vector2(pushX, pushY);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * PlayerController.Instance.boost * Time.deltaTime;
        transform.position += new Vector3(moveX, 0);
        if(transform.position.x < -11f)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float dmg)
    {
        lives -= dmg;
        if (lives > 0)
        {
            flashWhite.Flash(); // Flash the asteroid when it takes damage
        }else {
            AudioManager.Instance.PlaySound(AudioManager.Instance.asteroidDestroy); // Play the asteroid destroy sound
            Destroy(gameObject);
            // Instantiate the destroy effect at the asteroid's position
            Instantiate(destroyEffect, transform.position, transform.rotation);
            if(Random.value < 0.25f) // 25% chance to spawn a star
            {
                Instantiate(ObjectSpawner.Instance.starPrefab, transform.position, transform.rotation);
            }
            GameManager.Instance.AddAsteroidCount(); // Increment the asteroid count in the game manager
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(destroyEffect, transform.position, transform.rotation);
        }
    }
}
