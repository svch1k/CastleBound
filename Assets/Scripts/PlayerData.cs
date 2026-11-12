using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Castlebound/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int Gold;
    public float DamageMultiplier = 1;
    public float MaxHealthMultiplier = 1;
    public float DrawSpeedMultiplier = 1;
    public float GoldMultiplier = 1;


    public void Reset()
    {
        DamageMultiplier = 1;
        MaxHealthMultiplier = 1;
        DrawSpeedMultiplier = 1;
        GoldMultiplier = 1;
    }

}
