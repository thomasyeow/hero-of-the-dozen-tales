using UnityEngine;

public class HeroInventory : MonoBehaviour
{
    public EquipmentInventoryObject inventory;
    public EquipmentInventoryObject equippedinventory;

    public void OnTriggerEnter(Collider other)
    {
        int rand = Random.Range(1, 6);

        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            if (rand == 1)
                inventory.AddItem(new Item(item.amulet), 1);
            if (rand == 2)
                inventory.AddItem(new Item(item.helmet), 1);
            if (rand == 3)
                inventory.AddItem(new Item(item.chestpiece), 1);
            if (rand == 4)
                inventory.AddItem(new Item(item.leggings), 1);
            if (rand == 5)
                inventory.AddItem(new Item(item.boots), 1);

            //inventory.AddItem(new Item(item.amulet), 1);
            //inventory.AddItem(new Item(item.amulet), 1);
            //inventory.AddItem(new Item(item.amulet), 1);
            //inventory.AddItem(new Item(item.amulet), 1);
            //inventory.AddItem(new Item(item.amulet), 1);
            //inventory.AddItem(new Item(item.amulet), 1);
            //inventory.AddItem(new Item(item.amulet), 1);
            //inventory.AddItem(new Item(item.amulet), 1);
            //inventory.AddItem(new Item(item.amulet), 1);

            Destroy(other.gameObject);
        }
    }


    //EXTREMELY IMPORTANT: this is so that we dont have to update the scriptable object everytime we import something. This should be removed in the final product
    private void OnApplicationQuit()
    {
        inventory.Container.Items.Clear();
        equippedinventory.Container.Items.Clear();
    }
}
