using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DeckBuilderManager : MonoBehaviour
{
    public Dictionary<GlobalRune.Type, int> deckList = new Dictionary<GlobalRune.Type, int>();
    public Dictionary<GlobalRune.Type, int> runeInv = new Dictionary<GlobalRune.Type, int>();
    public GameObject deckBuilderUI;
    public int minNrOfRunes = 6;
    public int maxNrofRuneType = 5;


    public GameObject IceRuneInDeckUI;
    public GameObject FireRuneInDeckUI;
    public GameObject CattleRuneInDeckUI;
    public GameObject GiftRuneInDeckUI;
    public GameObject HailRuneInDeckUI;
    public GameObject HorseRuneInDeckUI;
    public GameObject StoneRuneInDeckUI;
    public GameObject SunRuneInDeckUI;
    public GameObject TyrRuneInDeckUI;
    public GameObject YewRuneInDeckUI;

    public List<GameObject> deckListObjects;

    //public GameObject IceRuneInInvUI;
    //public GameObject FireRuneInInvUI;
    //public GameObject CattleRuneInInvUI;
    //public GameObject GiftRuneInInvUI;
    //public GameObject HailRuneInInvUI;
    //public GameObject HorseRuneInInvUI;
    //public GameObject StoneRuneInInvUI;
    //public GameObject SunRuneInInvUI;
    //public GameObject TyrRuneInInvUI;
    //public GameObject YewRuneInInvUI;


    private void Start()
    {
        runeInv = GlobalRune.runeInventory();
        deckBuilderUI.SetActive(false);
    }
    public void OpenDeckBuilder()
    {
        if (!deckBuilderUI.activeSelf)
        {
            runeInv = GlobalRune.runeInventory();
            deckList = GlobalRune.getDeckList();
            //UpdateRuneInvUI();
            updateRuneInvList();
            updateDeckList();
            Time.timeScale = 0;
            deckBuilderUI.SetActive(true);
        } else
        {
            CloseDeckBuilder();
        }
    }
    public void CloseDeckBuilder()
    {
        deckBuilderUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void addRuneToDeckList(int i)
    {

        if (runeInv[deckList.Keys.ToArray()[i]] > deckList[deckList.Keys.ToArray()[i]] && deckList[deckList.Keys.ToArray()[i]] < maxNrofRuneType)
        {
            deckList[deckList.Keys.ToArray()[i]]++;
            updateRuneInvList();
            updateDeckList();
            GlobalRune.setDeckList(deckList.Values.ToList());
        }
    }
    public void removeRuneFromDeckList(int i)
    {
        int nrOfRunesInDeck = getNrOfRunesInDeck();
        if (deckList[deckList.Keys.ToArray()[i]] > 0 && nrOfRunesInDeck > minNrOfRunes)
        {
            deckList[deckList.Keys.ToArray()[i]]--;
            updateRuneInvList();
            updateDeckList();
            GlobalRune.setDeckList(deckList.Values.ToList());
        }
    }
    public int getNrOfRunesInDeck()
    {
        int temp = 0;
        foreach (GlobalRune.Type type in deckList.Keys)
        {
            temp += deckList[type];
        }
        return temp;
    }

    public void updateDeckList()
    {
        foreach (GlobalRune.Type type in deckList.Keys)
        {
            switch (type)
            {
                case GlobalRune.Type.ICE:
                    for (int i = 0; i < maxNrofRuneType; i++)
                    {
                        if (i < deckList[type])
                        {
                            deckListObjects[i].SetActive(true);
                        }
                        else
                        {
                            deckListObjects[i].SetActive(false);
                        }
                    }
                    break;
                case GlobalRune.Type.FIRE:
                    for (int i = maxNrofRuneType; i < 2 * maxNrofRuneType; i++)
                    {
                        if (i - maxNrofRuneType < deckList[type])
                        {
                            deckListObjects[i].SetActive(true);
                        }
                        else
                        {
                            deckListObjects[i].SetActive(false);
                        }
                    }
                    break;
                case GlobalRune.Type.CATTLE:
                    for (int i = 2 * maxNrofRuneType; i < 3 * maxNrofRuneType; i++)
                    {
                        if (i - (2 * maxNrofRuneType) < deckList[type])
                        {
                            deckListObjects[i].SetActive(true);
                        }
                        else
                        {
                            deckListObjects[i].SetActive(false);
                        }
                    }
                    break;
                case GlobalRune.Type.GIFT:
                    for (int i = 3 * maxNrofRuneType; i < 4 * maxNrofRuneType; i++)
                    {
                        if (i - (3 * maxNrofRuneType) < deckList[type])
                        {
                            deckListObjects[i].SetActive(true);
                        }
                        else
                        {
                            deckListObjects[i].SetActive(false);
                        }
                    }
                    break;
                case GlobalRune.Type.HAIL:
                    for (int i = 4 * maxNrofRuneType; i < 5 * maxNrofRuneType; i++)
                    {
                        if (i - (4 * maxNrofRuneType) < deckList[type])
                        {
                            deckListObjects[i].SetActive(true);
                        }
                        else
                        {
                            deckListObjects[i].SetActive(false);
                        }
                    }
                    break;
                case GlobalRune.Type.HORSE:
                    for (int i = 5 * maxNrofRuneType; i < 6 * maxNrofRuneType; i++)
                    {
                        if (i - (5 * maxNrofRuneType) < deckList[type])
                        {
                            deckListObjects[i].SetActive(true);
                        }
                        else
                        {
                            deckListObjects[i].SetActive(false);
                        }
                    }
                    break;
                case GlobalRune.Type.STONE:
                    for (int i = 6 * maxNrofRuneType; i < 7 * maxNrofRuneType; i++)
                    {
                        if (i - (6 * maxNrofRuneType) < deckList[type])
                        {
                            deckListObjects[i].SetActive(true);
                        }
                        else
                        {
                            deckListObjects[i].SetActive(false);
                        }
                    }
                    break;
                case GlobalRune.Type.SUN:
                    for (int i = 7 * maxNrofRuneType; i < 8 * maxNrofRuneType; i++)
                    {
                        if (i - (7 * maxNrofRuneType) < deckList[type])
                        {
                            deckListObjects[i].SetActive(true);
                        }
                        else
                        {
                            deckListObjects[i].SetActive(false);
                        }
                    }
                    break;
                case GlobalRune.Type.TYR:
                    for (int i = 8 * maxNrofRuneType; i < 9 * maxNrofRuneType; i++)
                    {
                        if (i - (8 * maxNrofRuneType) < deckList[type])
                        {
                            deckListObjects[i].SetActive(true);
                        }
                        else
                        {
                            deckListObjects[i].SetActive(false);
                        }
                    }
                    break;
                case GlobalRune.Type.YEW:
                    for (int i = 9 * maxNrofRuneType; i < 10 * maxNrofRuneType; i++)
                    {
                        if (i - (9 * maxNrofRuneType) < deckList[type])
                        {
                            deckListObjects[i].SetActive(true);
                        }
                        else
                        {
                            deckListObjects[i].SetActive(false);
                        }
                    }
                    break;
            }
        }
    }
    /*public void UpdateRuneInvUI()
    {
        foreach (GlobalRune.Type type in runeInv.Keys)
        {
            switch (type)
            {
                case GlobalRune.Type.ICE:
                    if (runeInv[type] > 0)
                    {
                        IceRuneInInvUI.GetComponentInChildren<TextMeshProUGUI>().text = "Ice Rune: " + runeInv[type].ToString();
                        IceRuneInInvUI.SetActive(true);
                    }
                    else IceRuneInInvUI.SetActive(false);
                    break;
                case GlobalRune.Type.FIRE:
                    if (runeInv[type] > 0)
                    {
                        FireRuneInInvUI.GetComponentInChildren<TextMeshProUGUI>().text = "Fire Rune: " + runeInv[type].ToString();
                        FireRuneInInvUI.SetActive(true);
                    }
                    else FireRuneInInvUI.SetActive(false);
                    break;
                case GlobalRune.Type.CATTLE:
                    if (runeInv[type] > 0)
                    {
                        CattleRuneInInvUI.GetComponentInChildren<TextMeshProUGUI>().text = "Cattle Rune: " + runeInv[type].ToString();
                        CattleRuneInInvUI.SetActive(true);
                    }
                    else CattleRuneInInvUI.SetActive(false);
                    break;
                case GlobalRune.Type.GIFT:
                    if (runeInv[type] > 0)
                    {
                        GiftRuneInInvUI.GetComponentInChildren<TextMeshProUGUI>().text = "Gift Rune: " + runeInv[type].ToString();
                        GiftRuneInInvUI.SetActive(true);
                    }
                    else GiftRuneInInvUI.SetActive(false);
                    break;
                case GlobalRune.Type.HAIL:
                    if (runeInv[type] > 0)
                    {
                        HailRuneInInvUI.GetComponentInChildren<TextMeshProUGUI>().text = "Hail Rune: " + runeInv[type].ToString();
                        HailRuneInInvUI.SetActive(true);
                    }
                    else HailRuneInInvUI.SetActive(false);
                    break;
                case GlobalRune.Type.HORSE:
                    if (runeInv[type] > 0)
                    {
                        HorseRuneInInvUI.GetComponentInChildren<TextMeshProUGUI>().text = "Horse Rune: " + runeInv[type].ToString();
                        HorseRuneInInvUI.SetActive(true);
                    }
                    else HorseRuneInInvUI.SetActive(false);
                    break;
                case GlobalRune.Type.STONE:
                    if (runeInv[type] > 0)
                    {
                        StoneRuneInInvUI.GetComponentInChildren<TextMeshProUGUI>().text = "Stone Rune: " + runeInv[type].ToString();
                        StoneRuneInInvUI.SetActive(true);
                    }
                    else StoneRuneInInvUI.SetActive(false);
                    break;
                case GlobalRune.Type.SUN:
                    if (runeInv[type] > 0)
                    {
                        SunRuneInInvUI.GetComponentInChildren<TextMeshProUGUI>().text = "Sun Rune: " + runeInv[type].ToString();
                        SunRuneInInvUI.SetActive(true);
                    }
                    else SunRuneInInvUI.SetActive(false);
                    break;
                case GlobalRune.Type.TYR:
                    if (runeInv[type] > 0)
                    {
                        TyrRuneInInvUI.GetComponentInChildren<TextMeshProUGUI>().text = "Tyr Rune: " + runeInv[type].ToString();
                        TyrRuneInInvUI.SetActive(true);
                    }
                    else TyrRuneInInvUI.SetActive(false);
                    break;
                case GlobalRune.Type.YEW:
                    if (runeInv[type] > 0)
                    {
                        YewRuneInInvUI.GetComponentInChildren<TextMeshProUGUI>().text = "Yew Rune: " + runeInv[type].ToString();
                        YewRuneInInvUI.SetActive(true);
                    }
                    else YewRuneInInvUI.SetActive(false);
                    break;
            }
        }
    }*/
    public void updateRuneInvList()
    {
        foreach (GlobalRune.Type type in runeInv.Keys)
        {
            switch (type)
            {
                case GlobalRune.Type.ICE:
                    if (runeInv[type] > 0)
                    {
                        IceRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Ice Rune: " + deckList[type].ToString() + "/" + runeInv[type].ToString();
                        IceRuneInDeckUI.SetActive(true);
                    }
                    else IceRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.FIRE:
                    if (runeInv[type] > 0)
                    {
                        FireRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Fire Rune: " + deckList[type].ToString() + "/" + runeInv[type].ToString();
                        FireRuneInDeckUI.SetActive(true);
                    }
                    else FireRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.CATTLE:
                    if (runeInv[type] > 0)
                    {
                        CattleRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Cattle Rune: " + deckList[type].ToString() + "/" + runeInv[type].ToString();
                        CattleRuneInDeckUI.SetActive(true);
                    }
                    else CattleRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.GIFT:
                    if (runeInv[type] > 0)
                    {
                        GiftRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Gift Rune: " + deckList[type].ToString() + "/" + runeInv[type].ToString();
                        GiftRuneInDeckUI.SetActive(true);
                    }
                    else GiftRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.HAIL:
                    if (runeInv[type] > 0)
                    {
                        HailRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Hail Rune: " + deckList[type].ToString() + "/" + runeInv[type].ToString();
                        HailRuneInDeckUI.SetActive(true);
                    }
                    else HailRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.HORSE:
                    if (runeInv[type] > 0)
                    {
                        HorseRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Horse Rune: " + deckList[type].ToString() + "/" + runeInv[type].ToString();
                        HorseRuneInDeckUI.SetActive(true);
                    }
                    else HorseRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.STONE:
                    if (runeInv[type] > 0)
                    {
                        StoneRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Stone Rune: " + deckList[type].ToString() + "/" + runeInv[type].ToString();
                        StoneRuneInDeckUI.SetActive(true);
                    }
                    else StoneRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.SUN:
                    if (runeInv[type] > 0)
                    {
                        SunRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Sun Rune: " + deckList[type].ToString() + "/" + runeInv[type].ToString();
                        SunRuneInDeckUI.SetActive(true);
                    }
                    else SunRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.TYR:
                    if (runeInv[type] > 0)
                    {
                        TyrRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Tyr Rune: " + deckList[type].ToString() + "/" + runeInv[type].ToString();
                        TyrRuneInDeckUI.SetActive(true);
                    }
                    else TyrRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.YEW:
                    if (runeInv[type] > 0)
                    {
                        YewRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Yew Rune: " + deckList[type].ToString() + "/" + runeInv[type].ToString();
                        YewRuneInDeckUI.SetActive(true);
                    }
                    else YewRuneInDeckUI.SetActive(false);
                    break;
            }
        }
    }
    /*public void updateDeckList(GlobalRune.Type type)
    {
            switch (type)
            {
                case GlobalRune.Type.ICE:
                    if (deckList[type] > 0)
                    {
                        IceRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Ice Rune: " + deckList[type].ToString();
                        IceRuneInDeckUI.SetActive(true);
                    }
                    else IceRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.FIRE:
                    if (deckList[type] > 0)
                    {
                        FireRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Fire Rune: " + deckList[type].ToString();
                        FireRuneInDeckUI.SetActive(true);
                    }
                    else FireRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.CATTLE:
                    if (deckList[type] > 0)
                    {
                        CattleRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Cattle Rune: " + deckList[type].ToString();
                        CattleRuneInDeckUI.SetActive(true);
                    }
                    else CattleRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.GIFT:
                    if (deckList[type] > 0)
                    {
                        GiftRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Gift Rune: " + deckList[type].ToString();
                        GiftRuneInDeckUI.SetActive(true);
                    }
                    else GiftRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.HAIL:
                    if (deckList[type] > 0)
                    {
                        HailRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Hail Rune: " + deckList[type].ToString();
                        HailRuneInDeckUI.SetActive(true);
                    }
                    else HailRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.HORSE:
                    if (deckList[type] > 0)
                    {
                        HorseRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Horse Rune: " + deckList[type].ToString();
                        HorseRuneInDeckUI.SetActive(true);
                    }
                    else HorseRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.STONE:
                    if (deckList[type] > 0)
                    {
                        StoneRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Stone Rune: " + deckList[type].ToString();
                        StoneRuneInDeckUI.SetActive(true);
                    }
                    else StoneRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.SUN:
                    if (deckList[type] > 0)
                    {
                        SunRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Sun Rune: " + deckList[type].ToString();
                        SunRuneInDeckUI.SetActive(true);
                    }
                    else SunRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.TYR:
                    if (deckList[type] > 0)
                    {
                        TyrRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Tyr Rune: " + deckList[type].ToString();
                        TyrRuneInDeckUI.SetActive(true);
                    }
                    else TyrRuneInDeckUI.SetActive(false);
                    break;
                case GlobalRune.Type.YEW:
                    if (deckList[type] > 0)
                    {
                        YewRuneInDeckUI.GetComponentInChildren<TextMeshProUGUI>().text = "Yew Rune: " + deckList[type].ToString();
                        YewRuneInDeckUI.SetActive(true);
                    }
                    else YewRuneInDeckUI.SetActive(false);
                    break;
            }
        }*/
}
