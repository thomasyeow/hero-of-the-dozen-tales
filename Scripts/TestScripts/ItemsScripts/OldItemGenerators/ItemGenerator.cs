using System;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { Helmet, Chest, Leggings, Boots, Amulet, Wood, Metal, Stone, Rune, Leather }           //What equipment can we make
public enum Rarity { Common, Uncommon, Rare, Epic, Heroic }
public enum GeneralAttributes { Broken, Weak, Fragile, Sturdy, Unbreakable, Unyielding }           //Armor
public enum AccessoryAttributes { Faint, Dim, Distinct, Firm, Resolute, Unrelenting }            //Power


public class ItemGenerator : MonoBehaviour
{
    private static int level = 0;

    public static List<string> GenerateEquipment(int eqtyp, int randomLvl)
    {
        level = randomLvl;
        Rarity rarity = getRarity();
        EquipmentType eq = GetEquipmentType(eqtyp);

        int strength = getStrength(rarity);
        int att = getAttribute(rarity);
        int attributestrength = getAttributeStrength(strength, att);

        string name = getName(rarity, eq, strength, att, attributestrength);
        ScriptableObject.CreateInstance<Equipment>();
        return itemListConverter(name, eq, rarity, strength, attributestrength);


        //    return new Equipment(name, strength, eq);///////
    }

    private static Rarity getRarity()         //What rarity is generated?
    {
        int chance = UnityEngine.Random.Range(1, 1000);

        return 1000 >= chance && chance >= 451
            ? Rarity.Common
            : 450 >= chance && chance >= 201
            ? Rarity.Uncommon
            : 200 >= chance && chance >= 51
            ? Rarity.Rare
            : 50 >= chance && chance >= 11 ? Rarity.Epic : 10 >= chance && chance >= 1 ? Rarity.Heroic : Rarity.Common;
    }

    //private static EquipmentType GetEquipmentType()             //What equipment are we using?
    //{
    //    int chance = UnityEngine.Random.Range(1, 500);
    //    if (500 >= chance && chance >= 401)
    //        return EquipmentType.Helmet;
    //    if (400 >= chance && chance >= 301)
    //        return EquipmentType.Chest;
    //    if (300 >= chance && chance >= 201)
    //        return EquipmentType.Leggings;
    //    if (200 >= chance && chance >= 101)
    //        return EquipmentType.Boots;
    //    if (100 >= chance && chance >= 1)
    //        return EquipmentType.Amulet;

    //    return EquipmentType.Chest;
    //}
    private static EquipmentType GetEquipmentType(int eqtyp)
    {
        //We use this instead of the above GetEquipmentType(), because equipment type is generated in the class "HeroInventory" to allow inventory icons
        return eqtyp == 1
                                                                                                                     ? EquipmentType.Amulet
                                                                                                                     : eqtyp == 2
            ? EquipmentType.Helmet
            : eqtyp == 3
            ? EquipmentType.Chest
            : eqtyp == 4 ? EquipmentType.Leggings : eqtyp == 5 ? EquipmentType.Boots : EquipmentType.Chest;
    }


    private static int getStrength(Rarity rarity)     //What are the chances of attributes/armor/power?
    {
        return rarity == Rarity.Common
            ? (int)(UnityEngine.Random.Range(2, 8) + Math.Round(level * 1.0))
            : rarity == Rarity.Uncommon
            ? (int)(UnityEngine.Random.Range(6, 12) + Math.Round(level * 1.25))
            : rarity == Rarity.Rare
            ? (int)(UnityEngine.Random.Range(10, 16) + Math.Round(level * 1.5))
            : rarity == Rarity.Epic
            ? (int)(UnityEngine.Random.Range(14, 20) + Math.Round(level * 1.75))
            : rarity == Rarity.Heroic ? (int)(UnityEngine.Random.Range(18, 24) + Math.Round(level * 2.0)) : 0;
    }

    private static int getAttributeStrength(int power, int attribute)
    {
        if (attribute == 1)
            return -(int)Math.Round(power * 0.45);
        return attribute == 2
            ? -(int)Math.Round(power * 0.30)
            : attribute == 3
            ? -(int)Math.Round(power * 0.15)
            : attribute == 4
            ? (int)Math.Round(power * 0.15)
            : attribute == 5 ? (int)Math.Round(power * 0.30) : attribute == 6 ? (int)Math.Round(power * 0.45) : 0;
    }

    private static int getAttribute(Rarity rarity)                      //What attributes?
    {
        int attchance = UnityEngine.Random.Range(1, 1000);
        if (1000 >= attchance && attchance >= 601)
            return 0;                                               //0 means no attribute
        if (600 >= attchance && attchance >= 501)
            return 1;                                               //1 means Broken/Feint
        if (500 >= attchance && attchance >= 401)
            return 2;                                               //2 means Weak/Dim
        if (400 >= attchance && attchance >= 301)
            return 3;                                               //3 means Fragile/Distinct
        if (300 >= attchance && attchance >= 201)
            return 4;                                               //4 means Sturdy/Firm
        if (200 >= attchance && attchance >= 101)
            return 5;                                               //5 means Unbreaking/resolute
        if (100 >= attchance && attchance >= 1)
            return 6;                                               //6 means Unyielding/Unrelenting
        return 0;
    }

    private static string getName(Rarity rarity, EquipmentType equipment, int strength, int attribute, int attstrength)          //Generate name based on information
    {
        string name = "";
        name += "[Lv." + level + "] ";

        //Set rarity name
        if (rarity == Rarity.Common)
            name += "Common ";
        if (rarity == Rarity.Uncommon)
            name += "Uncommon ";
        if (rarity == Rarity.Rare)
            name += "Rare ";
        if (rarity == Rarity.Epic)
            name += "Epic ";
        if (rarity == Rarity.Heroic)
            name += "Heroic ";

        //Set equipment name
        if (equipment == EquipmentType.Helmet)
            name += "helmet ";
        if (equipment == EquipmentType.Chest)
            name += "chest ";
        if (equipment == EquipmentType.Leggings)
            name += "leggings ";
        if (equipment == EquipmentType.Boots)
            name += "boots ";
        if (equipment == EquipmentType.Amulet)
            name += "amulet ";

        if (attribute != 0)
            name += "the ";

        if (equipment == EquipmentType.Amulet)
        {
            if (attribute == 1)
                name += "faint ";
            if (attribute == 2)
                name += "dim ";
            if (attribute == 3)
                name += "distinct ";
            if (attribute == 4)
                name += "firm ";
            if (attribute == 5)
                name += "resolute ";
            if (attribute == 6)
                name += "unrelenting ";
        }
        else
        {
            if (attribute == 1)
                name += "broken ";
            if (attribute == 2)
                name += "weak ";
            if (attribute == 3)
                name += "fragile ";
            if (attribute == 4)
                name += "sturdy ";
            if (attribute == 5)
                name += "unbreakable ";
            if (attribute == 6)
                name += "unyielding ";
        }

        if (attribute == 0 || attstrength == 0)
            name += "(" + strength + ")";
        else if (attribute < 4 && attribute > 0)
            name += "(" + strength + "" + attstrength + ")";
        else
            name += "(" + strength + "+" + attstrength + ")";


        return name;
    }




    public static List<string> itemListConverter(string name, EquipmentType eq, Rarity rarity, int strength, int attributestrength)
    {
        List<string> retList = new List<string>();
        retList.Add(name);

        if (eq == EquipmentType.Amulet)
            retList.Add("1");
        else if (eq == EquipmentType.Helmet)
            retList.Add("2");
        else if (eq == EquipmentType.Chest)
            retList.Add("3");
        else if (eq == EquipmentType.Leggings)
            retList.Add("4");
        else if (eq == EquipmentType.Boots)
            retList.Add("5");


        if (rarity == Rarity.Common)
            retList.Add("1");
        else if (rarity == Rarity.Uncommon)
            retList.Add("2");
        else if (rarity == Rarity.Rare)
            retList.Add("3");
        else if (rarity == Rarity.Epic)
            retList.Add("4");
        else if (rarity == Rarity.Heroic)
            retList.Add("5");

        retList.Add((strength + attributestrength).ToString());

        return retList;
    }
}
