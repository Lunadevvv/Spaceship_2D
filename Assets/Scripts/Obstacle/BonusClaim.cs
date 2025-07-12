using UnityEngine;

public class BonusClaim : MonoBehaviour
{
    PhaserWeapon weapon;

    private void Start()
    {
        weapon = PhaserWeapon.Instance;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //AudioManager.Instance.PlaySound(AudioManager.Instance.starCollect); // Play the star collect sound
            if (weapon.weaponLevel < weapon.stats.Count - 1)
            {
                weapon.weaponLevel++;
            }
            AudioManager.Instance.PlaySound(AudioManager.Instance.collectBonus); // Âm thanh phá hủy
            Destroy(gameObject); // Destroy the star object
        }
    }
}
