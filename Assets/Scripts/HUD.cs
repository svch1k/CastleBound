using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance;

    public Text scoreText;
    public RectTransform progressFill;
    public RectTransform chargeFill;
    public RectTransform healthFill;
    public Text healthText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetScore(0);
        SetLevelProgress(0);
        SetCharge(0);
    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetLevelProgress(float progress)
    {
        SetFill(progressFill, progress);
    }

    public void SetCharge(float charge)
    {
        SetFill(chargeFill, charge);
    }

    public void SetHealth(int health, int maxHealth)
    {
        SetFill(healthFill, (float)health / maxHealth);
        healthText.text = health + " / " + maxHealth;
    }

    private void SetFill(RectTransform fill, float amount)
    {
        fill.anchorMax = new Vector2(Mathf.Clamp01(amount), 1);
    }
}
