using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private Animator animator;
    private FlashWhite flashWhite;
    private float speedX;
    private float directX;
    private float speedY;
    private bool charging;

    private float switchInterval;
    private float switchTimer;

    [SerializeField] private float maxLives = 100;
    private float health;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    private float fireRate = 2f;
    private float fireTimer = 0f;

    void Start()
    {
        health = maxLives;
        flashWhite = GetComponent<FlashWhite>();
        animator = GetComponent<Animator>();
        EnterChargeState(); // Gọi ngay từ đầu để boss bắn liền
        fireTimer = 0f;     // Cho phép bắn ngay lập tức
    }

    void Update()
    {
        float playerPosition = PlayerController.Instance.transform.position.x;

        switchTimer -= Time.deltaTime;
        if (switchTimer <= 0f)
        {
            if (charging)
                EnterPatrolState();
            else
                EnterChargeState();
        }

        if (transform.position.y > 3 || transform.position.y < -3)
            speedY *= -1;
        else if (transform.position.x < -1)
        {
            EnterChargeState();
            directX = -1;
        }
        else if (transform.position.x > 9)
            directX = 1;

        float moveX;
        if (PlayerController.Instance.isBoosting && !charging)
            moveX = GameManager.Instance.worldSpeed * Time.deltaTime * -0.5f * directX;
        else
            moveX = speedX * Time.deltaTime * directX;

        float moveY = speedY * Time.deltaTime;
        transform.position += new Vector3(moveX, moveY);

        // Bắn liên tục 1s 1 lần
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            FireBullet();
            fireTimer = 1f / fireRate;
        }
    }


    void FireBullet()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("Missing bulletPrefab or firePoint in Boss1!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Boss fired bullet at: " + firePoint.position);
    }
    void EnterPatrolState()
    {
        speedX = 4f; // tăng nhẹ từ 3f → 4f
        speedY = Random.Range(-2f, 2f); // tăng biên độ dọc
        switchInterval = Random.Range(1f, 2f);
        switchTimer = switchInterval;
        charging = false;
        animator.SetBool("Charging", false);
    }

    void EnterChargeState()
    {
        speedX = -5f;
        speedY = Random.Range(-1f, 1f); // <-- Cho phép bay lên/xuống khi charge
        switchInterval = Random.Range(1f, 2f);
        switchTimer = switchInterval;
        charging = true;
        fireTimer = 0f;
        animator.SetBool("Charging", true);
        AudioManager.Instance.PlaySound(AudioManager.Instance.bossCharge);
    }

    public void TakeDamage(float damage)
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.hitArmor);
        health -= damage;
        if (health <= 0)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.spaceBoom);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        flashWhite.Flash();
        Level2UIController.Instance.UpdateBossHealthSlider(health, maxLives);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.hitArmor);
            Destroy(collision.gameObject);
        }
    }
}
