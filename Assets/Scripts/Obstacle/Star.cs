using UnityEngine;

public class Star : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //AudioManager.Instance.PlaySound(AudioManager.Instance.starCollect); // Play the star collect sound
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level1")
                Level1Manager.Instance.AddScore(Level1Manager.Instance.scoreToAdd); // Add score for collecting the star
                AudioManager.Instance.PlaySound(AudioManager.Instance.collectStar); // Âm thanh phá hủy
            Destroy(gameObject); // Destroy the star object
        }
    }
}
