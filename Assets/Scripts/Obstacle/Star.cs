using UnityEngine;

public class Star : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //AudioManager.Instance.PlaySound(AudioManager.Instance.starCollect); // Play the star collect sound
            GameManager.Instance.AddScore(GameManager.Instance.scoreToAdd); // Add score for collecting the star
            Destroy(gameObject); // Destroy the star object
        }
    }
}
