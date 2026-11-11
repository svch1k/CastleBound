using TMPro;
using Unity.AppUI.UI;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public Upgrade Upgrade;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI PriceText;
    public Button BuyButton;
    private void Awake()
    {
        Events.OnUpgradeBought += UpgradePurchased;
    }
    private void OnDestroy()
    {
        Events.OnUpgradeBought -= UpgradePurchased;
    }

    void Start()
    {
        SetTexts();
    }

    public void PurchaseUpgrade()
    {
        Events.BuyUpgrade(Upgrade);
    }
    public void UpgradePurchased(Upgrade upgrade)
    {
        if (upgrade == Upgrade)
        {
            SetTexts();
        }
    }
    public void SetTexts()
    {
        NameText.text = Upgrade.Name;
        LevelText.text = Upgrade.CurrentLevel.ToString() + "/" + Upgrade.MaxLevel.ToString();
        DescriptionText.text = Upgrade.Description + Upgrade.BaseValue + "%";
        if (Upgrade.CurrentLevel == Upgrade.MaxLevel)
        {
            PriceText.text = "MAX";
        }
        else
        {
            PriceText.text = (Upgrade.BaseCost + Upgrade.CurrentLevel * 100).ToString();
        }
    }
}
