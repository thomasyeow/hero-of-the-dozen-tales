using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class EquipmentInventoryObject : ScriptableObject, IDataPersistance
{
    public Inv Container;           //container for "Inv" class

    public void AddItem(Item _item, int _amount)                    //adds
    {
        if (Container.Items.Count < 24)                  //24 is max item amount (4 * 6) as a result of UI limitation
        {
            DisplayInventoryEquipment.invDisplayUpdate = true;
            Container.Items.Add(new InventorySlot(_item, _amount));

            LootPopUpManager.instance.PopUpLoot(_item.rarity.ToString() + " " + _item.type.ToString() + "!", 1);
        }
    }



    public void removeItem(InventorySlot slot)
    {
        Container.Items.Remove(slot);
        //      Container.Items.RemoveAll(item => item == null);
    }

    public void LoadData(GameData data)
    {
        this.Container = data.playerInv;
    }

    public void SaveData(ref GameData data)
    {
        data.playerInv = this.Container;
    }
}

[System.Serializable]
public class Inv        //stores a list of inventoryslots
{
    public List<InventorySlot> Items = new List<InventorySlot>();

}

[System.Serializable]
public class InventorySlot              // an individual item "slot", not really used as items dont stack, but it stayed in case we change this
{
    //public int ID;
    public Item item;                               //ho
    public int amount;


    public InventorySlot(Item _item, int _amount)
    {
        //  ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
