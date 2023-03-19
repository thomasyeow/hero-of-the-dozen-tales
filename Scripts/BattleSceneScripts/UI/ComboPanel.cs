using UnityEngine;

public class ComboPanel : MonoBehaviour
{

    public NewBattleSystem nbs;
    RectTransform panelTransform;
    //position of first and second combo slots
    public Transform firstSlot, secondSlot;
    //instantiated runes
    private GameObject rune1;
    //rune types
    private GlobalRune.Type rune1Type, rune2Type;
    //is the first rune combo slot occupied
    public bool firstSlotOccupied;
    void Start()
    {
        panelTransform = GetComponent<RectTransform>();
        hideComboPanel();
    }

    //show combo panel
    public void showComboPanel()
    {
        panelTransform.localScale = new Vector3(7, 7, 1);
    }

    public void hideComboPanel()
    {
        panelTransform.localScale = Vector3.zero;
    }

    //return runes in panel to hand
    private void returnRunes()
    {
        nbs.addRuneToHand(GlobalRune.Instance.getRunePrefab(rune1Type));
        nbs.addRuneToHand(GlobalRune.Instance.getRunePrefab(rune2Type));
        hideComboPanel();

    }

    //spawn runes into combo panel
    public void spawnRune(GlobalRune.Type type)
    {
        //if first combo slot is filled
        if (firstSlotOccupied)
        {
            rune2Type = type;
            //retrieve GlobalRune.Type of combined rune, or Type == ICE if combo doesn't exist
            GlobalRune.Type combinedRune = GlobalRune.getComboType(rune1Type, type);
            //if combo exists, spawn combo rune in hand and clear comboPanel
            if (combinedRune != 0)
            {
                nbs.addRuneToHand(GlobalRune.Instance.getRunePrefab(combinedRune));
                Destroy(rune1);
            }
            //if combo doesn't exist
            else
            {
                Destroy(rune1);
                returnRunes();
            }
            firstSlotOccupied = false;
            hideComboPanel();
        }
        //if first combo slot is NOT filled
        else
        {
            GameObject prefab = GlobalRune.Instance.getRunePrefab(type);
            rune1 = Instantiate(prefab, firstSlot);
            rune1Type = type;
            firstSlotOccupied = true;
            //hideComboPanel();
        }
    }

}
