using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Castlebound/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int Gold;
    public float DamageMultiplier = 1;
    public int MaxHealthMultiplier = 1;
    public float DrawSpeedMultiplier = 1;
    public float GoldMultiplier = 1;

    private void Awake()
    {
        Events.OnResetStore += Reset;
    }
    private void OnDestroy()
    {
        Events.OnResetStore += Reset;
    }

    public void Reset()
    {
        DamageMultiplier = 1;
        MaxHealthMultiplier = 1;
        DrawSpeedMultiplier = 1;
        GoldMultiplier = 1;
    }

}
