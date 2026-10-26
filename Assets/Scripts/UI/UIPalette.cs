using UnityEngine;

public enum UIColorRole
{
    Charcoal,
    DarkBrown,
    Parchment,
    ForestGreen,
    Sage,
    Brown,
    Red,
    DarkRed
}

[CreateAssetMenu(fileName = "UIPalette", menuName = "Castlebound/UI Palette")]
public class UIPalette : ScriptableObject
{
    public Color charcoal = Hex("#3A3A3A");
    public Color darkBrown = Hex("#3B2E24");
    public Color parchment = Hex("#C7B89A");
    public Color forestGreen = Hex("#2F5D3A");
    public Color sage = Hex("#6B8E5B");
    public Color brown = Hex("#5A3E2B");
    public Color red = Hex("#8B2E2E");
    public Color darkRed = Hex("#5A1F1F");

    public Color Get(UIColorRole role)
    {
        switch (role)
        {
            case UIColorRole.Charcoal: return charcoal;
            case UIColorRole.DarkBrown: return darkBrown;
            case UIColorRole.Parchment: return parchment;
            case UIColorRole.ForestGreen: return forestGreen;
            case UIColorRole.Sage: return sage;
            case UIColorRole.Brown: return brown;
            case UIColorRole.Red: return red;
            case UIColorRole.DarkRed: return darkRed;
            default: return Color.magenta;
        }
    }

    public static Color Hex(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);
        return color;
    }

    private void OnValidate()
    {
        foreach (UIColor element in FindObjectsByType<UIColor>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            element.Apply();
        }
    }
}
