using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private float maxLives = 100; // Số mạng của Boss
    private float health;
    [SerializeField] private GameObject destroyEffect;
    void Start()
    {
        health = maxLives;
        flashWhite = GetComponent<FlashWhite>();
        animator = GetComponent<Animator>();
        EnterChargeState();
    }

    void Update()
    {
        float playerPosition = PlayerController.Instance.transform.position.x;

        if (switchTimer > 0)
        {
            switchTimer -= Time.deltaTime;
        }
        else
        {
            if (charging && transform.position.x > playerPosition)
            {
                EnterPatrolState();
            }
            else
            {
                EnterChargeState();
            }
        }

        if (transform.position.y > 3 || transform.position.y < -3)
        {
            speedY *= -1;
        }
        else if (transform.position.x < -1)
        {
            EnterChargeState();
            directX = -1;
        }
        else if (transform.position.x > 9)
        {
            directX = 1;
        }

        bool boost = PlayerController.Instance.isBoosting;
        float moveX;
        if (boost && !charging)
        {
            moveX = GameManager.Instance.worldSpeed * Time.deltaTime * -0.5f * directX;
        }
        else
        {
            moveX = speedX * Time.deltaTime * directX;
        }
        float moveY = speedY * Time.deltaTime;

        transform.position += new Vector3(moveX, moveY);
    }

    void EnterPatrolState()
    {
        speedX = 0;
        speedY = Random.Range(-0.5f, 0.5f); // trước là ±2f → giờ giảm
        switchInterval = Random.Range(5f, 10f);
        switchTimer = switchInterval;
        charging = false;
        animator.SetBool("Charging", false);
    }


    void EnterChargeState()
    {
        speedX = -2f; // trước là -5f, giờ giảm tốc
        speedY = 0;
        switchInterval = Random.Range(2f, 2.5f);
        switchTimer = switchInterval;
        charging = true;
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
        GameObject target = collision.gameObject;

        if (target.CompareTag("Bullet"))
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.hitArmor); // Âm thanh phá hủy
            Destroy(target); // Hủy đạn sau va chạm
        }
    }
    
    
}
