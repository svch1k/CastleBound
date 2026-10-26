using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class UIColor : MonoBehaviour
{
    public UIPalette palette;
    public UIColorRole role = UIColorRole.Parchment;
    [Range(0, 1)] public float alpha = 1;

    void OnEnable()
    {
        Apply();
    }

    void OnValidate()
    {
        Apply();
    }

    public void Apply()
    {
        if (palette == null) return;

        Color color = palette.Get(role);
        color.a = alpha;

        Button button = GetComponent<Button>();
        if (button != null)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = color;
            colors.highlightedColor = palette.sage;
            colors.pressedColor = palette.sage;
            colors.selectedColor = color;
            colors.disabledColor = color;
            colors.colorMultiplier = 1;
            button.colors = colors;
            if (button.targetGraphic != null)
            {
                button.targetGraphic.color = Color.white;
            }
            return;
        }

        Graphic graphic = GetComponent<Graphic>();
        if (graphic != null)
        {
            graphic.color = color;
        }
    }
}
