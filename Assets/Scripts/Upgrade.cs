using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Castlebound/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string Name;
    public UpgradeType type;
    public string Description;
    public Sprite Sprite;
    public int MaxLevel;
    public int CurrentLevel;
    public int BaseCost;
    public float BaseValue;
    public float ValueIncrease;

    private void Awake()
    {
        Events.OnResetStore += Reset;
    }
    private void OnDestroy()
    {
        Events.OnResetStore += Reset;
    }

    public int GetCost()
    {
        return (int)(BaseCost + CurrentLevel * 100);
    }
    public float GetValue()
    {
        return BaseValue + CurrentLevel * ValueIncrease;
    }
    public void Reset()
    {
        CurrentLevel = 0;
    }
}
