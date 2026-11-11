using System;
using UnityEngine;

public static class Events
{
    public static event Action<Upgrade> OnBuyUpgrade;
    public static void BuyUpgrade(Upgrade upgrade) => OnBuyUpgrade?.Invoke(upgrade);
    public static event Action<Upgrade> OnUpgradeBought;
    public static void UpgradeBought(Upgrade upgrade) => OnUpgradeBought?.Invoke(upgrade);
    public static event Action OnResetStore;
    public static void ResetStore() => OnResetStore?.Invoke();
}
