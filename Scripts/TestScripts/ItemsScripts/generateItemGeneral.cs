using UnityEngine;

public class generateItemGeneral : MonoBehaviour
{
    public EquipmentInventoryObject inventory;

    public ItemObject amulet;
    public ItemObject helmet;
    public ItemObject chestpiece;
    public ItemObject leggings;
    public ItemObject boots;

    public void generateItemGeneralMethod()
    {
        int rand = Random.Range(1, 6);

        if (rand == 1)
            inventory.AddItem(new Item(amulet), 1);
        if (rand == 2)
            inventory.AddItem(new Item(helmet), 1);
        if (rand == 3)
            inventory.AddItem(new Item(chestpiece), 1);
        if (rand == 4)
            inventory.AddItem(new Item(leggings), 1);
        if (rand == 5)
            inventory.AddItem(new Item(boots), 1);
    }
}
