using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HealPowerUp : MonoBehaviour
{
    public PlayerHealth health;
    public float healAmount = 30f;
    public float cooldown = 15f;
    public Image cooldownOverlay;

    private float readyTime;

    void Update()
    {
        if (!LevelManager.Instance.IsPlaying || Keyboard.current == null) return;

        float remaining = readyTime - Time.time;
        cooldownOverlay.fillAmount = Mathf.Clamp01(remaining / cooldown);

        if (Keyboard.current.qKey.wasPressedThisFrame && remaining <= 0 && health.Current < health.maxHealth)
        {
            health.Heal(healAmount);
            readyTime = Time.time + cooldown;        
        }
    }
}
