using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public PlayerData Stats;

    private void Awake()
    {
        Events.OnBuyUpgrade += BuyUpgrade;
    }
    private void OnDestroy()
    {
        Events.OnBuyUpgrade -= BuyUpgrade;
    }

    public void BuyUpgrade(Upgrade upgrade)
    {
        if (Stats.Gold >= upgrade.GetCost() && upgrade.CurrentLevel < upgrade.MaxLevel)
        {
            print("bought " + upgrade.CurrentLevel);
            Stats.Gold -= upgrade.GetCost();
            
            switch (upgrade.type)
            {
                case UpgradeType.Damage: 
                    Stats.DamageMultiplier = 1 + upgrade.GetValue() / 100;
                    break;
                case UpgradeType.MaxHealth:
                    Stats.MaxHealthMultiplier = 1 + (int)upgrade.GetValue() / 100;
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
        }
    }
    public void OnContinuePressed()
    {
        SceneManager.LoadScene(0);
    }
    public void OnResetPressed()
    {
        Events.ResetStore();
    }
}
