using UnityEngine;

public class Critter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject zappedEffect;

    private float moveSpeed;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float moveTimer;
    private float moveInterval;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        moveSpeed = Random.Range(0.5f, 3f);
        moveInterval = Random.Range(1f, 3f);
        moveTimer = moveInterval;
        transform.position += new Vector3(1f, 0f, 0f);
        GenerateRandomTargetPosition();
    }

    void Update()
    {
        if (moveTimer > 0)
            moveTimer -= Time.deltaTime;
        else
        {
            GenerateRandomTargetPosition();
            moveInterval = Random.Range(0.5f, 2f);
            moveTimer = moveInterval;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        Vector3 relativePos = targetPosition - transform.position;
        if (relativePos != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1080 * Time.deltaTime);
        }
    }

    private void GenerateRandomTargetPosition()
    {
        float randomX = Random.Range(-10f, 0f);
        float randomY = Random.Range(-4.5f, 4.5f);
        targetPosition = new Vector2(randomX, randomY);
    }

    public void OnHitByBullet()
    {
        if (zappedEffect != null)
            Instantiate(zappedEffect, transform.position, Quaternion.identity);

        AudioManager.Instance.PlaySound(AudioManager.Instance.squished);
        Destroy(gameObject);
    }
}
