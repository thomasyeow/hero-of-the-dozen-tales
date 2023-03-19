using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    //singleton of tooltip, so that we can easily change the tooltip
    private static ToolTipSystem current;
    public ToolTip tooltip;

    private void Awake()
    {
        current = this;
    }

    public static void Show(string content, string header = "")
    {
        current.tooltip.SetText(content, header);
        current.tooltip.gameObject.SetActive(true);
    }
    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}
