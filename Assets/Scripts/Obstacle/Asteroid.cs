using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private FlashWhite flashWhite;

    [SerializeField] private Sprite[] asteroidSprites;
    [SerializeField] private float lives = 3f;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private GameObject starPrefab;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashWhite = GetComponent<FlashWhite>();
        spriteRenderer.sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];

        rb = GetComponent<Rigidbody2D>();
        float pushX = Random.Range(-1f, 0f);
        float pushY = Random.Range(-1f, 1f);
        rb.linearVelocity = new Vector2(pushX, pushY);

    }

    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * PlayerController.Instance.boost * Time.deltaTime;
        transform.position += new Vector3(moveX, 0f, 0f);

        if (transform.position.x < -11f)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float dmg)
    {
        lives -= dmg;

        if (lives > 0)
        {
            flashWhite.Flash(); // Nháy trắng khi bị bắn
        }
        else
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.asteroidDestroy); // Âm thanh phá hủy
            Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(gameObject);

            // 25% có sao
            if (Random.value < 0.25f && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level1")
            {
                Instantiate(starPrefab, transform.position, transform.rotation);
            }
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level1")
                Level1Manager.Instance.AddAsteroidCount(); // Cộng số asteroid bị phá
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
