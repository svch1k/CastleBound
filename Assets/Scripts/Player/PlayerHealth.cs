using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;

    public float Current { get; private set; }

    void Awake()
    {
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
        HUD.Instance.SetHealth(Mathf.CeilToInt(Current), maxHealth);

        if (Current <= 0)
        {
            LevelManager.Instance.FailLevel();
        }
    }
}
