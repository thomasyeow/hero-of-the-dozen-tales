using UnityEngine;

public class Equipment : ScriptableObject
{
    public string name;
    public int strength;
    public EquipmentType eqType;


    public Equipment(string Name, int Strength, EquipmentType EquipmentType)
    {
        name = Name;
        strength = Strength;
        eqType = EquipmentType;
    }
}
