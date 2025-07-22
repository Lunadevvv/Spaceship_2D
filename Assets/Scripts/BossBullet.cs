using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private float speed = 20f;
    public GameObject explosionEffect;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * speed;
        Destroy(gameObject, 3f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("BossBullet hit: Player");

            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1); // Trừ máu player
            }

            Destroy(gameObject); // Xoá viên đạn sau va chạm
        }
    }
}
