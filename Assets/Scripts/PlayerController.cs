using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D col;
    private SpriteRenderer playerSprite;

    private bool isInvincible = false;

    [SerializeField] private float moveSpeed;
    private Vector2 playerDirection;
    public float boost = 1f;
    private float boostPower = 5f;
    public bool isBoosting;

    private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float regenEnergy;
    [SerializeField] private float useEnergy;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private GameObject shield;

    [SerializeField] private float fireRate = 0.2f;
    private float nextFireTime;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        playerSprite = GetComponent<SpriteRenderer>();

        energy = maxEnergy;
        health = maxHealth;

        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
    }

    void Update()
    {
        if (Time.timeScale <= 0) return;

        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");
        playerDirection = new Vector2(directionX, directionY).normalized;

        animator.SetFloat("MoveX", directionX);
        animator.SetFloat("MoveY", directionY);

        if (Input.GetKeyDown(KeyCode.LeftShift) && energy >= 10)
        {
            EnterBoost();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitBoost();
        }

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.shoot);
            PhaserWeapon.Instance.Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = playerDirection * moveSpeed;

        if (isBoosting)
        {
            if (energy >= 0.2f)
                energy -= useEnergy * Time.deltaTime;
            else
                ExitBoost();
        }
        else
        {
            if (energy < maxEnergy)
                energy += regenEnergy * Time.deltaTime;
        }

        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
    }

    private void EnterBoost()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.boost);
        animator.SetBool("boosting", true);
        boost = boostPower;
        isBoosting = true;
    }

    public void ExitBoost()
    {
        animator.SetBool("boosting", false);
        boost = 1f;
        isBoosting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            health -= 1f;
            if(PhaserWeapon.Instance.weaponLevel > 0)
                PhaserWeapon.Instance.weaponLevel--;
            UIController.Instance.UpdateHealthSlider(health, maxHealth);
            AudioManager.Instance.PlaySound(AudioManager.Instance.playerHit);
            ActivateInvincibility(1.6f);

            if (health <= 0)
            {
                PlayerDeath();
            }
        }else if (collision.gameObject.CompareTag("Critter"))
        {
            Critter critter = collision.gameObject.GetComponent<Critter>();
            if (critter != null) critter.OnHitByBullet();
        }else if (collision.gameObject.CompareTag("Boss"))
        {
            PlayerDeath();
        }
        else if (collision.gameObject.CompareTag("BossBullet"))
        {
            Destroy(collision.gameObject); // Xóa đạn
            TakeDamage(1f); // Trừ máu player
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BossBullet"))
        {
            TakeDamage(1f);
            Destroy(collision.gameObject);
        }
    }


    private void PlayerDeath()
    {
        boost = 0f;
        gameObject.SetActive(false);
        AudioManager.Instance.PlaySound(AudioManager.Instance.spaceBoom);
        Instantiate(destroyEffect, transform.position, transform.rotation);
        if (GameManager.Instance.timer < PlayerPrefs.GetFloat("HighestScore", 0))
        {
            PlayerPrefs.SetFloat("HighestScore", GameManager.Instance.timer);
            PlayerPrefs.Save();
        } 
        else
            PlayerPrefs.SetFloat("CurrentTimer", GameManager.Instance.timer);
        GameManager.Instance.GameOver();
    }

    public void ActivateInvincibility(float duration)
    {
        if (!isInvincible)
            StartCoroutine(InvincibilityCoroutine(duration));
    }
    public void TakeDamage(float amount)
    {
        if (isInvincible) return;

        health -= amount;
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
        AudioManager.Instance.PlaySound(AudioManager.Instance.playerHit);

        if (health <= 0)
        {
            PlayerDeath();
        }
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true;
        float blinkInterval = 0.2f;
        float elapsedTime = 0f;
        col.enabled = false;
        shield.SetActive(true);
        while (elapsedTime < duration)
        {
            playerSprite.enabled = !playerSprite.enabled;
            elapsedTime += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        playerSprite.enabled = true;
        col.enabled = true;
        isInvincible = false;
        shield.SetActive(false);
    }
}
