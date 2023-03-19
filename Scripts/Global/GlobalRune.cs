using System.Collections.Generic;
using UnityEngine;

//Singleton class for storing current rune inventory
public class GlobalRune : MonoBehaviour
{
    private GlobalRune() { }

    public static GlobalRune Instance { get; private set; }

    //globally available rune enum
    public enum Type
    {
        //SINGLE RUNES
        ICE = 0,
        FIRE = 1,
        CATTLE = 2,
        GIFT = 3,
        HAIL = 4,
        HORSE = 5,
        STONE = 6,
        SUN = 7,
        TYR = 8,
        YEW = 9,
        //COMBO RUNES
        FIRE_ICE = 10,
        FIRE_CATTLE = 11,
        SUN_TYR = 12,
        //FX
        FREEZE_FX = 13,
        BURNING = 14

    }

    //list of available COMBOs
    private static List<AvailableCombo> availableCombos;

    //list of runestoneUI prefabs
    public List<GameObject> runePrefabList;

    //runestoneUI prefab dictionary(for easy retrieval in code)
    private static Dictionary<Type, GameObject> prefabDict;

    //list of rune fx prefabs
    public List<GameObject> fxPrefabList;

    //Dictionary containing animation prefabs for every rune's effect
    private static Dictionary<Type, GameObject> fxDict;

    //dictionary storing current rune inventory
    private static Dictionary<Type, int> runeInv;
    //dictionary storing deck of runes used in battle
    private static Dictionary<Type, int> deckList;

    //initialize singleton
    private void Awake()
    {
        //ensure this is the only instance of this singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Do not destroy this script's gameObject between scenes
            DontDestroyOnLoad(gameObject);
            //this is the only instance of GlobalRuneInventory
            Instance = this;
        }
    }

    private void Start()
    {
        //TODO: initialize rune inventory from savefile
        //TEMP: rune inventory is set explicitly, but runes can be picked up from the overworld
        runeInv = new Dictionary<Type, int>()
        {
            {Type.ICE, 0},
            {Type.FIRE, 0},
            {Type.CATTLE, 0},
            {Type.GIFT, 0},
            {Type.HAIL, 0},
            {Type.HORSE, 0},
            {Type.STONE, 0},
            {Type.SUN, 0},
            {Type.TYR, 0},
            {Type.YEW, 0}
        };
        //deck used for battle
        deckList = new Dictionary<Type, int>()
        {
            {Type.ICE, 0},
            {Type.FIRE, 0},
            {Type.CATTLE, 0},
            {Type.GIFT, 0},
            {Type.HAIL, 0},
            {Type.HORSE, 0},
            {Type.STONE, 0},
            {Type.SUN, 0},
            {Type.TYR, 0},
            {Type.YEW, 0}
        };
        prefabDict = new Dictionary<Type, GameObject>()
        {
            {Type.ICE, runePrefabList[0]},
            {Type.FIRE, runePrefabList[1]},
            {Type.CATTLE, runePrefabList[2]},
            {Type.GIFT, runePrefabList[3]},
            {Type.HAIL, runePrefabList[4]},
            {Type.HORSE, runePrefabList[5]},
            {Type.STONE, runePrefabList[6]},
            {Type.SUN, runePrefabList[7]},
            {Type.TYR, runePrefabList[8]},
            {Type.YEW, runePrefabList[9]},
            //rune COMBOS
            {Type.FIRE_ICE, runePrefabList[10]},
            {Type.FIRE_CATTLE, runePrefabList[11]},
            {Type.SUN_TYR, runePrefabList[12]}
        };
        fxDict = new Dictionary<Type, GameObject>()
        {
            {Type.ICE, fxPrefabList[0]},
            {Type.FIRE, fxPrefabList[1]},
            {Type.CATTLE, fxPrefabList[2]},
            {Type.GIFT, fxPrefabList[3]},
            {Type.HAIL, fxPrefabList[4]},
            {Type.HORSE, fxPrefabList[5]},
            {Type.STONE, fxPrefabList[6]},
            {Type.SUN, fxPrefabList[7]},
            {Type.TYR, fxPrefabList[8]},
            {Type.YEW, fxPrefabList[9]},
            //rune fx COMBOS
            {Type.FIRE_ICE, fxPrefabList[10]},
            {Type.FIRE_CATTLE, fxPrefabList[11]},
            {Type.SUN_TYR, fxPrefabList[12]},
            //FX
            {Type.FREEZE_FX, fxPrefabList[13]},
            {Type.BURNING, fxPrefabList[14]}
        };
        //set available combos
        availableCombos = new List<AvailableCombo>(){
            //0
            new AvailableCombo(Type.FIRE, Type.ICE, Type.FIRE_ICE),
            //1
            new AvailableCombo(Type.FIRE, Type.CATTLE, Type.FIRE_CATTLE),
            //2
            new AvailableCombo(Type.SUN, Type.TYR, Type.SUN_TYR)
        };
    }

    //get number of a rune in inventory
    public static int getRuneCount(Type type)
    {
        return runeInv[type];
    }

    //add runes to inv
    public static void addRune(Type type, int amount)
    {
        runeInv[type] += amount;
        if (deckList[type] + amount <= 5)
        {
            deckList[type] += amount;
        }
        LootPopUpManager.instance.PopUpLoot(type.ToString() + " Rune", amount);
    }

    //discard runes from inv
    public static void discardRune(Type type, int amount)
    {
        runeInv[type] -= amount;
    }

    //return a copy of rune inventory dictionary
    public static Dictionary<Type, int> runeInventory()
    {
        Dictionary<Type, int> result = new Dictionary<Type, int>();
        foreach (Type type in runeInv.Keys)
        {
            result.Add(type, runeInv[type]);
        }
        return result;
    }
    //For loading a saved game
    public static void setRuneInventory(List<int> list)
    {
        var temp = new Dictionary<Type, int>()
        {
            {Type.ICE, 0},
            {Type.FIRE, 0},
            {Type.CATTLE, 0},
            {Type.GIFT, 0},
            {Type.HAIL, 0},
            {Type.HORSE, 0},
            {Type.STONE, 0},
            {Type.SUN, 0},
            {Type.TYR, 0},
            {Type.YEW, 0}
        };
        for (int i = 0; i < list.Count; i++)
        {
            temp[(GlobalRune.Type)i] = list[i];
        }
        runeInv = temp;
    }
    //return a copy of rune deck dictionary
    public static Dictionary<Type, int> getDeckList()
    {
        Dictionary<Type, int> result = new Dictionary<Type, int>();
        foreach (Type type in deckList.Keys)
        {
            result.Add(type, deckList[type]);
        }
        return result;
    }
    public static void setDeckList(List<int> list)
    {
        var temp = new Dictionary<Type, int>()
        {
            {Type.ICE, 0},
            {Type.FIRE, 0},
            {Type.CATTLE, 0},
            {Type.GIFT, 0},
            {Type.HAIL, 0},
            {Type.HORSE, 0},
            {Type.STONE, 0},
            {Type.SUN, 0},
            {Type.TYR, 0},
            {Type.YEW, 0}
        };
        for (int i = 0; i < list.Count; i++)
        {
            temp[(GlobalRune.Type)i] = list[i];
        }
        deckList = temp;
    }

    //return rune prefab of given type
    public GameObject getRunePrefab(Type type)
    {
        return prefabDict[type];
    }

    //return rune deck as list of prefabs
    public List<GameObject> getInvAsList()
    {
        List<GameObject> prefabList = new List<GameObject>();
        foreach (Type type in deckList.Keys)
        {
            for (int i = 0; i < deckList[type]; i++)
            {
                prefabList.Add(getRunePrefab(type));
            }

        }
        return prefabList;
    }

    public GameObject getFxPrefab(Type type)
    {
        return fxDict[type];
    }

    //return GlobalRune.Type of combo if combo exists, else return 0
    public static Type getComboType(Type type1, Type type2)
    {
        for (int i = 0; i < availableCombos.Count; i++)
        {
            if (availableCombos[i].isCombo(type1, type2))
            {
                return availableCombos[i].getComboType();
            }
        }
        return 0;
    }
    //nested class for storing valid rune combos
    class AvailableCombo
    {
        //the 2 types of this combo
        private Type type1, type2;
        private Type comboType;
        public AvailableCombo(Type type1, Type type2, Type comboType)
        {
            this.type1 = type1;
            this.type2 = type2;
            this.comboType = comboType;
        }

        public Type getComboType()
        {
            return comboType;
        }

        //is this object this combo?
        public bool isCombo(Type arg1, Type arg2)
        {
            if (arg1 == type1)
            {
                if (arg2 == type2)
                {
                    return true;
                }
            }
            else if (arg1 == type2)
            {
                if (arg2 == type1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
