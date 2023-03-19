using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI text;
    public void SetMoney(string amount)
    {
        text.text = amount;
    }
}
