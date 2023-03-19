using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Count", menuName = "Inventory System/Crafting Recipe/Item Count")]
public class ItemCountSO : ScriptableObject
{
    [SerializeField] private EquipmentType type;
    [SerializeField] private int count;

    public EquipmentType ItemType => type;
    public int Count => count;
}
