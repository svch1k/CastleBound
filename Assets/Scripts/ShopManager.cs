using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public PlayerData Stats;
    public TextMeshProUGUI GoldText;

    private void Awake()
    {
        Events.OnBuyUpgrade += BuyUpgrade;
    }
    private void OnDestroy()
    {
        Events.OnBuyUpgrade -= BuyUpgrade;
    }
    private void Start()
    {
        SetGoldText();
    }

    public void BuyUpgrade(Upgrade upgrade)
    {
        if (Stats.Gold >= upgrade.GetCost() && upgrade.CurrentLevel < upgrade.MaxLevel)
        {
            Stats.Gold -= upgrade.GetCost();
            
            switch (upgrade.type)
            {
                case UpgradeType.Damage: 
                    Stats.DamageMultiplier = 1 + upgrade.GetValue() / 100;
                    break;
                case UpgradeType.MaxHealth:
                    Stats.MaxHealthMultiplier = 1 + upgrade.GetValue() / 100;
                    break;
                case UpgradeType.DrawSpeed:
                    Stats.DrawSpeedMultiplier = 1 + upgrade.GetValue() / 100;
                    break;
                case UpgradeType.GoldMultiplier:
                    Stats.GoldMultiplier = 1 + upgrade.GetValue() / 100;
                    break;
            }
            upgrade.CurrentLevel += 1;

            Events.UpgradeBought(upgrade);
            SetGoldText();
        }
    }
    public void OnContinuePressed()
    {
        SceneManager.LoadScene(0);
    }
    public void OnResetPressed()
    {
        Stats.Reset();
        Events.ResetStore();
    }
    public void SetGoldText()
    {
        GoldText.text = "Gold: " + Stats.Gold.ToString();
    }
}
