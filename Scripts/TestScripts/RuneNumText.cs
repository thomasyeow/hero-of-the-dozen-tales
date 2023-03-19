using System;
using UnityEngine;
using UnityEngine.UI;

public class RuneNumText : MonoBehaviour
{
    private Text runeNumText;
    public int runeNum;
    [SerializeField]
    private RunesInventory runesAmtSO;



    void Start()
    {
        runeNumText = GetComponent<Text>();
    }


    void Update()
    {
        runeNumText.text = Convert.ToString(runesAmtSO.runesAmt[runeNum]);
    }
}
