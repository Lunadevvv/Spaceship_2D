using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource shoot;
    public AudioSource spaceBoom;
    public AudioSource playerHit;
    public AudioSource pause;
    public AudioSource menuButton;
    public AudioSource boost;
    public AudioSource asteroidDestroy;
    public AudioSource squished;
    public AudioSource burn;
    public AudioSource hitArmor;
    public AudioSource bossCharge;
    public AudioSource collectStar;
    public AudioSource collectBonus;

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

    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }
}
