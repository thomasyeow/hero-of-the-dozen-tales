using System.Collections.Generic;
using UnityEngine;


//public enum ItemType{Healing, Equipment, Default}


public class ItemObject : ScriptableObject          //item object is used for item prefabs (e.g. helmet prefab, amulet prefab, etc.)
{
    public int Id;
    public string Name;
    public Sprite uiDesplay;
    //  public EquipmentType type;
    [TextArea(15, 20)]
    public string description;

}

[System.Serializable]
public class Item                                   //item is used for the actual item (helmet item is made from helmet prefab, then stats are added)
{
    public int Id;
    public string Name;
    public Sprite uiDesp;
    public EquipmentType type;
    public Rarity rarity;
    public int strength;


    public Item(ItemObject item)
    {
        int randomLVL = Random.Range(SkillSystemManager.Instance.getLvl(SkillSet.FIGHTING) - 3, SkillSystemManager.Instance.getLvl(SkillSet.FIGHTING) + 3);
        if (randomLVL < 0)
            randomLVL = 0;
        //name, equipment type, rarity, strength
        List<string> generatedItem = ItemGenerator.GenerateEquipment(item.Id, randomLVL);

        uiDesp = item.uiDesplay;
        Name = generatedItem[0];
        Id = item.Id;
        type = decodingItemGeneratorType(item.uiDesplay.name);
        //  type = decodingItemGeneratorType(generatedItem[1]);
        rarity = decodingItemGeneratorRarity(generatedItem[2]);
        strength = int.Parse(generatedItem[3]);
    }

    public Rarity decodingItemGeneratorRarity(string rar)
    {
        return rar == "1"
            ? Rarity.Common
            : rar == "2"
            ? Rarity.Uncommon
            : rar == "3" ? Rarity.Rare : rar == "4" ? Rarity.Epic : rar == "5" ? Rarity.Heroic : Rarity.Common;
    }
    public EquipmentType decodingItemGeneratorType(string typ)
    {
        //if (typ == "1")
        //    return EquipmentType.Amulet;
        //if (typ == "2")
        //    return EquipmentType.Helmet;
        //if (typ == "3")
        //    return EquipmentType.Chest;
        //if (typ == "4")
        //    return EquipmentType.Leggings;
        //if (typ == "5")
        //    return EquipmentType.Boots;

        //return EquipmentType.Amulet;
        return typ == "amulet_icon"
            ? EquipmentType.Amulet
            : typ == "helmet_icon"
            ? EquipmentType.Helmet
            : typ == "chestpiece_icon"
            ? EquipmentType.Chest
            : typ == "leggings_icon" ? EquipmentType.Leggings : typ == "boots_icon" ? EquipmentType.Boots : EquipmentType.Chest;
    }

}
