using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventoryEquipment : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public EquipmentInventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    public static bool invDisplayUpdate = false;

    // public GameObject infoPanel;
    private bool panelShown = false;
    public TextMeshProUGUI infoText;

    private InventorySlot selectedItem;
    public UnityEngine.UI.Button equipbutton;
    public UnityEngine.UI.Button dropbutton;
    public UnityEngine.UI.Button unequipallbutton;


    private void Start()
    {
        UpdateDisplay();
        equipbutton.onClick.AddListener(() => adjustPrefabs(selectedItem));
        dropbutton.onClick.AddListener(() => dropItem(selectedItem));
        unequipallbutton.onClick.AddListener(() => unequipAllEQ());

        initializeEquippedEQ();

        try
        {
            headSlot.transform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => unequipItem(headSlot));
            chestSlot.transform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => unequipItem(chestSlot));
            leggingsSlot.transform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => unequipItem(leggingsSlot));
            bootsSlot.transform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => unequipItem(bootsSlot));
            amuletSlot.transform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => unequipItem(amuletSlot));

        }
        catch { }
    }

    private void Update()
    {
        if (invDisplayUpdate)                    //only update when an item is added
        {
            //  Debug.Log("updated disp");
            UpdateDisplay();
        }

    }

    public void CreateDisplay()
    {
        // UpdateDisplay();
        //for (int i = 0; i < inventory.Container.Items.Count; i++)
        //{
        //    InventorySlot slot = inventory.Container.Items[i];
        //    var obj = Instantiate(inventoryPrefab, transform);

        //    UnityEngine.UI.Button buttonObj = obj.transform.GetComponent<UnityEngine.UI.Button>();
        //    buttonObj.onClick.AddListener(() => showItemInfoText(slot));

        //    obj.transform.GetChild(1).GetComponentInChildren<Image>().sprite = slot.item.uiDesp;
        //    showRarity(slot, obj);
        //    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        //    obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
        //    itemsDisplayed.Add(slot, obj);
        //}
    }

    public void UpdateDisplay()
    {
        clearDisplay();


        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];
            if (itemsDisplayed.ContainsKey(slot))
            {
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventoryPrefab, transform);

                UnityEngine.UI.Button buttonObj = obj.transform.GetComponent<UnityEngine.UI.Button>();
                buttonObj.onClick.AddListener(() => showItemInfoText(slot));

                //  var equipbutton = GameObject.Find("EquipButton").GetComponent<UnityEngine.UI.Button>();
                //  var dropbutton = GameObject.Find("DropButton").GetComponent<UnityEngine.UI.Button>();

                obj.transform.GetChild(1).GetComponentInChildren<Image>().sprite = inventory.Container.Items[i].item.uiDesp;
                showRarity(slot, obj);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
                itemsDisplayed.Add(slot, obj);
            }
        }
        invDisplayUpdate = false;
        //Debug.Log("created stuff");
    }

    void showItemInfoText(InventorySlot slot)
    {
        infoText.text = slot.item.Name;
        selectedItem = slot;
    }

    void equipItem(InventorySlot slot)
    {
        adjustPrefabs(slot);
    }
    void dropItem(InventorySlot slot)
    {
        foreach (var itt in itemsDisplayed)
        {
            if (itt.Key.Equals(slot))
                Destroy(itt.Value);
        }
        inventory.removeItem(slot);
        droppedEQ(slot);
        invDisplayUpdate = true;
    }

    public void clearDisplay()
    {
        // Debug.Log("got here");
        foreach (var itt in itemsDisplayed)
        {
            Destroy(itt.Value);
        }
        itemsDisplayed.Clear();
    }



    private void showRarity(InventorySlot slot, GameObject obj)
    {
        obj.transform.GetChild(0).GetComponentInChildren<Image>().rectTransform.Rotate(new Vector3(0, 0, Random.Range(0, 180)));
        if (slot.item.rarity == Rarity.Common)
        {
            obj.transform.GetChild(0).GetComponentInChildren<Image>().rectTransform.sizeDelta = new Vector2(95, 86);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 35);
        }
        if (slot.item.rarity == Rarity.Uncommon)
        {
            obj.transform.GetChild(0).GetComponentInChildren<Image>().rectTransform.sizeDelta = new Vector2(115, 106);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(0, 255, 0, 55);
        }
        if (slot.item.rarity == Rarity.Rare)
        {
            obj.transform.GetChild(0).GetComponentInChildren<Image>().rectTransform.sizeDelta = new Vector2(135, 126);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(0, 0, 255, 121);
        }
        if (slot.item.rarity == Rarity.Epic)
        {
            obj.transform.GetChild(0).GetComponentInChildren<Image>().rectTransform.sizeDelta = new Vector2(155, 136);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(96, 38, 171, 194);
        }
        if (slot.item.rarity == Rarity.Heroic)
        {
            obj.transform.GetChild(0).GetComponentInChildren<Image>().rectTransform.sizeDelta = new Vector2(175, 156);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(202, 158, 54, 220);
        }
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }







    /// <summary>
    /// //////////////////////////////////////////////////////////////
    /// </summary>


    public GameObject headSlot;
    public GameObject chestSlot;
    public GameObject leggingsSlot;
    public GameObject bootsSlot;
    public GameObject amuletSlot;

    public TextMeshProUGUI armorText;
    public TextMeshProUGUI powerText;

    Dictionary<InventorySlot, GameObject> EQitems = new Dictionary<InventorySlot, GameObject>();

    public EquipmentInventoryObject equippedinventorystorage;

    private int armorAmt;
    private int powerAmt;

    public void adjustPrefabs(InventorySlot slot)               //essentially equips the item
    {
        try
        {
            checkEQ(slot);                  //removes item from EQitems if an item of that type is already equipped
            equippedinventorystorage.Container.Items.Add(slot);


            //Equip item to item slot
            if (slot.item.type == EquipmentType.Helmet)
            {
                headSlot.transform.GetChild(1).GetComponentInChildren<Image>().sprite = slot.item.uiDesp;

                var tempcolor = headSlot.transform.GetChild(1).GetComponentInChildren<Image>().color;
                tempcolor.a = 1f;
                headSlot.transform.GetChild(1).GetComponentInChildren<Image>().color = tempcolor;

                showRarity(slot, headSlot);
                headSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
                EQitems.Add(slot, headSlot);
            }
            else if (slot.item.type == EquipmentType.Chest)
            {
                chestSlot.transform.GetChild(1).GetComponentInChildren<Image>().sprite = slot.item.uiDesp;

                var tempcolor = chestSlot.transform.GetChild(1).GetComponentInChildren<Image>().color;
                tempcolor.a = 1f;
                chestSlot.transform.GetChild(1).GetComponentInChildren<Image>().color = tempcolor;

                showRarity(slot, chestSlot);
                chestSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
                EQitems.Add(slot, chestSlot);
            }
            else if (slot.item.type == EquipmentType.Leggings)
            {
                leggingsSlot.transform.GetChild(1).GetComponentInChildren<Image>().sprite = slot.item.uiDesp;

                var tempcolor = leggingsSlot.transform.GetChild(1).GetComponentInChildren<Image>().color;
                tempcolor.a = 1f;
                leggingsSlot.transform.GetChild(1).GetComponentInChildren<Image>().color = tempcolor;

                showRarity(slot, leggingsSlot);
                leggingsSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
                EQitems.Add(slot, leggingsSlot);
            }
            else if (slot.item.type == EquipmentType.Boots)
            {
                bootsSlot.transform.GetChild(1).GetComponentInChildren<Image>().sprite = slot.item.uiDesp;

                var tempcolor = bootsSlot.transform.GetChild(1).GetComponentInChildren<Image>().color;
                tempcolor.a = 1f;
                bootsSlot.transform.GetChild(1).GetComponentInChildren<Image>().color = tempcolor;

                showRarity(slot, bootsSlot);
                bootsSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
                EQitems.Add(slot, bootsSlot);
            }
            else if (slot.item.type == EquipmentType.Amulet)
            {
                amuletSlot.transform.GetChild(1).GetComponentInChildren<Image>().sprite = slot.item.uiDesp;

                var tempcolor = amuletSlot.transform.GetChild(1).GetComponentInChildren<Image>().color;
                tempcolor.a = 1f;
                amuletSlot.transform.GetChild(1).GetComponentInChildren<Image>().color = tempcolor;

                showRarity(slot, amuletSlot);
                amuletSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
                EQitems.Add(slot, amuletSlot);
            }

            calculateEQ();
            //      equippedinventorystorage.AddItem(slot.item,1);
            // Debug.Log("added: " + slot);
            //      Debug.Log(equippedinventorystorage.Container.Items.Count + " counted items equipped");
        #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(inventory);
            UnityEditor.EditorUtility.SetDirty(equippedinventorystorage);
        #endif
        }
        catch { }
    }
    void checkEQ(InventorySlot slot)
    {
        if (EQitems.Count > 0)
        {
            foreach (var itt in EQitems)
            {
                if (itt.Key.item.type == slot.item.type)
                {
                    EQitems.Remove(itt.Key);
                    equippedinventorystorage.removeItem(itt.Key);
                //    equippedinventorystorage.Container.Items.Remove(slot);
                //foreach(InventorySlot slt in equippedinventorystorage.Container.Items)
                //{
                //        if (slt.Equals(itt))
                //            equippedinventorystorage.Container.Items.Remove(slt);
                //}
                break;
                }

            }
        }
    }
    void calculateEQ()              //calculate armor and power
    {
        int calc = 0;               //for armor
        int pow = 0;                //for amulet
        try
        {
            if (EQitems.Count > 0)              //check if the inventory isn't empty
            {

                foreach (var itt in EQitems)        //loop for every equipped item
                {
                    if (itt.Value == amuletSlot)        //if it is the amulet, then add its power to the total
                        pow = itt.Key.item.strength;
                    else                                //if it is not an amulet, then it must be armor - add the strength of every piece to total
                        calc += itt.Key.item.strength;
                }
            }
            armorAmt = calc;                            //store the armor total for later use (in combat)
            powerAmt = pow;                             //store the power total for later use (in combat)

            armorText.text = calc.ToString();           //display armor info (bottom of the left EQ side)
            powerText.text = pow.ToString();            //display power info (also bottom of the left EQ side)
        }
        catch { }
    }
    void droppedEQ(InventorySlot slot)
    {
        try
        {
            foreach (var itt in EQitems)
            {
                if (itt.Key == slot)
                {
                    itt.Value.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 61);
                    itt.Value.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 0);
                    itt.Value.GetComponentInChildren<TextMeshProUGUI>().text = "0";

                    EQitems.Remove(itt.Key);
                    equippedinventorystorage.removeItem(itt.Key);
                    break;
                }
            }
        }
        catch { }
        calculateEQ();
    }
    public void unequipAllEQ()
    {
        try
        {
            foreach (var itt in EQitems)
            {
                itt.Value.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 61);
                itt.Value.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 0);
                itt.Value.GetComponentInChildren<TextMeshProUGUI>().text = "0";
            }
        }
        catch {}
        EQitems.Clear();
        equippedinventorystorage.Container.Items.Clear();
        calculateEQ();
    }

    public void unequipItem(GameObject objj)
    {
        Debug.Log("dropped single " + EQitems.Count);
        if (EQitems.ContainsValue(objj))
        {
            try
            {
                foreach (var itt in EQitems)
                {
                    if (itt.Value == objj)
                    {
                        itt.Value.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 61);
                        itt.Value.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 0);
                        itt.Value.GetComponentInChildren<TextMeshProUGUI>().text = "0";

                        EQitems.Remove(itt.Key);
                        equippedinventorystorage.removeItem(itt.Key);
                        break;
                    }
                }
            }
            catch { }
        }
        calculateEQ();

    }

    void initializeEquippedEQ()                 //initializes EQ from equipped inventory SO (to store info between scenes)
    {
        //foreach(Item item in equippedinventorystorage.Container.Items)
        //{

        //}

        if(equippedinventorystorage.Container.Items != null && equippedinventorystorage.Container.Items.Count > 0)
        {
            for (int i = 0; i < equippedinventorystorage.Container.Items.Count; i++)
            {
                adjustPrefabsINIT(equippedinventorystorage.Container.Items[i]);
            }
        }
    }

    public void adjustPrefabsINIT(InventorySlot slot)                   //same as adjustprefabs, but doesn't add item to equippedinventory SO
    {
        try
        {

            checkEQ(slot);                  //removes item from EQitems if an item of that type is already equipped


            //Equip item to item slot
            if (slot.item.type == EquipmentType.Helmet)
            {
                headSlot.transform.GetChild(1).GetComponentInChildren<Image>().sprite = slot.item.uiDesp;

                var tempcolor = headSlot.transform.GetChild(1).GetComponentInChildren<Image>().color;
                tempcolor.a = 1f;
                headSlot.transform.GetChild(1).GetComponentInChildren<Image>().color = tempcolor;

                showRarity(slot, headSlot);
                headSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
                EQitems.Add(slot, headSlot);
            }
            else if (slot.item.type == EquipmentType.Chest)
            {
                chestSlot.transform.GetChild(1).GetComponentInChildren<Image>().sprite = slot.item.uiDesp;

                var tempcolor = chestSlot.transform.GetChild(1).GetComponentInChildren<Image>().color;
                tempcolor.a = 1f;
                chestSlot.transform.GetChild(1).GetComponentInChildren<Image>().color = tempcolor;

                showRarity(slot, chestSlot);
                chestSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
                EQitems.Add(slot, chestSlot);
            }
            else if (slot.item.type == EquipmentType.Leggings)
            {
                leggingsSlot.transform.GetChild(1).GetComponentInChildren<Image>().sprite = slot.item.uiDesp;

                var tempcolor = leggingsSlot.transform.GetChild(1).GetComponentInChildren<Image>().color;
                tempcolor.a = 1f;
                leggingsSlot.transform.GetChild(1).GetComponentInChildren<Image>().color = tempcolor;

                showRarity(slot, leggingsSlot);
                leggingsSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
                EQitems.Add(slot, leggingsSlot);
            }
            else if (slot.item.type == EquipmentType.Boots)
            {
                bootsSlot.transform.GetChild(1).GetComponentInChildren<Image>().sprite = slot.item.uiDesp;

                var tempcolor = bootsSlot.transform.GetChild(1).GetComponentInChildren<Image>().color;
                tempcolor.a = 1f;
                bootsSlot.transform.GetChild(1).GetComponentInChildren<Image>().color = tempcolor;

                showRarity(slot, bootsSlot);
                bootsSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
                EQitems.Add(slot, bootsSlot);
            }
            else if (slot.item.type == EquipmentType.Amulet)
            {
                amuletSlot.transform.GetChild(1).GetComponentInChildren<Image>().sprite = slot.item.uiDesp;

                var tempcolor = amuletSlot.transform.GetChild(1).GetComponentInChildren<Image>().color;
                tempcolor.a = 1f;
                amuletSlot.transform.GetChild(1).GetComponentInChildren<Image>().color = tempcolor;

                showRarity(slot, amuletSlot);
                amuletSlot.GetComponentInChildren<TextMeshProUGUI>().text = slot.item.strength.ToString("n0");
                EQitems.Add(slot, amuletSlot);
            }

            calculateEQ();
        }
        catch { }
    }
}
