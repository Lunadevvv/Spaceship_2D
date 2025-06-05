using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance; //Create an instance to use public variable (Singleton)

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private Collider2D collider2D;
    private SpriteRenderer playerSprite;

    private bool isInvincible = false;

    [SerializeField] private float moveSpeed;
    private Vector2 playerDirection;
    public float boost = 1f;
    private float boostPower = 5f;
    private bool isBoosting;

    private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float regenEnergy;
    [SerializeField] private float useEnergy;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [SerializeField] private GameObject destroyEffect; // Reference to the destroy effect prefab

    private void Awake() //Will run when the script instance is being loaded
    {
        if (Instance != null) //Instance already existed. Make sure not duplicate
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        energy = maxEnergy;
        health = maxHealth;
        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale > 0)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");
            animator.SetFloat("MoveX", directionX);
            animator.SetFloat("MoveY", directionY);
            playerDirection = new Vector2(directionX, directionY).normalized;

            if (Input.GetKeyDown(KeyCode.LeftShift) & energy >= 10)
            {
                EnterBoost();
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                ExitBoost();
            }else if (Input.GetKeyDown(KeyCode.Mouse0)){
                AudioManager.Instance.PlaySound(AudioManager.Instance.shoot); // Play shoot sound
                PhaserWeapon.Instance.Shoot(); // Call the Shoot method from PhaserWeapon
            }
        }
    }
    void FixedUpdate()
    {
        rigidbody2D.linearVelocity = new Vector2(playerDirection.x * moveSpeed, playerDirection.y * moveSpeed);
        if (isBoosting)
        {
            if(energy >= 0.2)
            {
                energy -= useEnergy * Time.deltaTime;
            }
            else
            {
                ExitBoost();
            }
        }
        else
        {
            if (energy < maxEnergy)
            {
                energy += regenEnergy * Time.deltaTime;
            }
        }
            UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
    }

    private void EnterBoost()
    {
        AudioManager.Instance.PlaySound(AudioManager.Instance.boost); // Play boost sound
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
            health -= 1f; // Example damage value
            UIController.Instance.UpdateHealthSlider(health, maxHealth);
            AudioManager.Instance.PlaySound(AudioManager.Instance.playerHit); // Play hit sound
            ActivateInvincibility(1.6f); // Start invincibility mode for 1.6 second
            if (health <= 0)
            {
                boost = 0f; // Reset boost when health is zero or below
                gameObject.SetActive(false); // Disable the player if health is zero or below
                AudioManager.Instance.PlaySound(AudioManager.Instance.spaceBoom); // Play explosion sound
                Instantiate(destroyEffect, transform.position, transform.rotation);
                GameManager.Instance.GameOver(); // Call GameOver method from GameManager
            }
        }
    }

    // Call this when the player is hit
    public void ActivateInvincibility(float duration)
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityCoroutine(duration));
        }
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true;
        float blinkInterval = 0.2f; // Adjust blink speed (smaller = faster)
        float elapsedTime = 0f;
        collider2D.enabled = false; // Disable collider to prevent further collisions
        while (elapsedTime < duration)
        {
            // Toggle sprite visibility for blinking
            playerSprite.enabled = !playerSprite.enabled;
            elapsedTime += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }
        playerSprite.enabled = true ; // Ensure the player is visible at the end
        collider2D.enabled = true; // Re-enable collider after invincibility ends
        isInvincible = false; // Set invincible mode off after 1 second
    }
}
