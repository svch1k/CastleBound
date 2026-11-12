using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerData Stats;
    public int maxHealth = 100;

    public float Current { get; private set; }

    public AudioClipGroup grunt_sound;

    void Awake()
    {
        maxHealth = Mathf.RoundToInt(maxHealth * Stats.MaxHealthMultiplier);
        Current = maxHealth;
    }

    void Start()
    {
        HUD.Instance.SetHealth(maxHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (!LevelManager.Instance.IsPlaying || Current <= 0) return;

        Current = Mathf.Max(0, Current - amount);
        AudioSourcePool.Instance.Play(grunt_sound);
        HUD.Instance.SetHealth(Mathf.CeilToInt(Current), maxHealth);

        if (Current <= 0)
        {
            LevelManager.Instance.FailLevel();
        }
    }
}
