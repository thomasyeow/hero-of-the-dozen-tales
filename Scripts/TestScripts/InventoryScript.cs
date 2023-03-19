using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    // [SerializeField]
    public genericResourceScript wood;
    public genericResourceScript stone;
    public genericResourceScript metal;



    public Text WoodText;
    public Text StoneText;
    public Text MetalText;

    public void Update()
    {
        WoodText.text = wood.getAmount().ToString();
        StoneText.text = stone.getAmount().ToString();
        MetalText.text = metal.getAmount().ToString();
    }
}
