using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance;

    public TMP_Text scoreValue;
    public TMP_Text coinsValue;
    public Image levelProgress;

    public Image chargeBar;

    public Image healthBar;
    public TMP_Text healthText;

    public GameObject[] weaponHighlights;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetScore(0);
        SetCoins(0);
        SetLevelProgress(0f);
        SetCharge(0f);
        SetHealth(100, 100);
        SelectWeapon(0);
    }

    public void SetScore(int score)
    {
        scoreValue.text = score.ToString();
    }

    public void SetCoins(int coins)
    {
        coinsValue.text = coins.ToString();
    }

    public void SetLevelProgress(float progress)
    {
        levelProgress.fillAmount = Mathf.Clamp01(progress);
    }

    public void SetCharge(float charge)
    {
        chargeBar.fillAmount = Mathf.Clamp01(charge);
    }

    public void SetHealth(int health, int maxHealth)
    {
        healthBar.fillAmount = Mathf.Clamp01((float)health / maxHealth);
        healthText.text = health + " / " + maxHealth;
    }

    public void SelectWeapon(int index)
    {
        for (int i = 0; i < weaponHighlights.Length; i++)
        {
            weaponHighlights[i].SetActive(i == index);
        }
    }
}
