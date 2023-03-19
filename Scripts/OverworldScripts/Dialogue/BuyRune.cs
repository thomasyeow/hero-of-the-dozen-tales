using TMPro;
using UnityEngine;

public class BuyRune : MonoBehaviour
{
    [SerializeField] int price;
    [SerializeField] GlobalRune.Type runeType;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI AmountText;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePriceText();
        UpdateAmountText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyThisRune()
    {
        Character_Controller player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Controller>();
        if (player.GetMoney() >= price)
        {
            player.AddMoney(-price);
            GlobalRune.addRune(runeType, 1);
            UpdateAmountText();
        }

    }

    void UpdatePriceText()
    {
        priceText.text = price.ToString();
    }
    void UpdateAmountText()
    {
        AmountText.text = "In inv: " + GlobalRune.getRuneCount(runeType);
    }
}
