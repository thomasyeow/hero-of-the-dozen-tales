using System;
using System.Collections.Generic;
using UnityEngine;

public enum SkillSet { FIGHTING, CRAFTING, ENCHANTING, TIMBER, MINING }

public class SkillSystemManager : MonoBehaviour
{
    public delegate void SkillEvent(SkillSet skill, float exp);
    public static SkillEvent AddExpEvent;

    //zmienne przechowujace ilosc expa dla danego "drzewka"? 
    //podstawowy zestaw metod, domyslnie 'tick' dodania exp to 10

    private static SkillSystemManager _instance; public static SkillSystemManager Instance { get { return _instance; } }

    private Dictionary<SkillSet, float> skillDict = new Dictionary<SkillSet, float>();

    private Dictionary<SkillSet, List<bool>> skillProgress = new Dictionary<SkillSet, List<bool>>();

    //private float fightingExp, craftingExp, enchantExp, timberExp, miningExp;
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(transform.root);
        }

        InitSkillDict();
    }


    private void OnEnable()
    {
        AddExpEvent += AddExp;
    }

    private void OnDisable()
    {
        AddExpEvent -= AddExp;
    }

    void InitSkillDict()
    {
        foreach (var skill in (SkillSet[])Enum.GetValues(typeof(SkillSet)))
        {
            skillDict.Add(skill, 0);
            skillProgress.Add(skill, new List<bool>(new bool[10]));
        }
    }

    void AddExp(SkillSet skill, float xp)
    {
        if (!skillDict.ContainsKey(skill))
            return;

        //Debug.Log($"Added exp at: {this}, in {skill}, for: {xp}");
        skillDict[skill] += xp;
    }

    /// <summary>
    /// Returns amount of exp for certain level
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    public float GetExp(SkillSet skill)
    {
        return skillDict[skill];
    }

    public bool isEnoughExp(SkillSet skill, float xp)
    {

        return (skillDict[skill] / xp) >= 1;
    }

    public int getLvl(SkillSet skill)
    {

        return (int)(skillDict[skill] / 100);
    }

    public void add10expFighting()
    {
        //skillDict[SkillSet.FIGHTING] += 10;
        AddExpEvent?.Invoke(SkillSet.FIGHTING, 15f);
        AddExpEvent?.Invoke(SkillSet.CRAFTING, 500f);

    }

    public List<bool> getSkillProgress(SkillSet skill)
    {
        return skillProgress[skill];
    }

    public void unlockSkill(SkillSet skill, int index)
    {
        skillProgress[skill][index] = true;
    }

    /// <summary>
    /// Takes skill as input, multiplayer, if level is reached skill is activated
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="muliplier"></param>
    /// <returns></returns>
    public float SkillValueMultiplier(SkillSet skill, float muliplier)
    {
        switch (skill)
        {
            case SkillSet.FIGHTING:
                return getLvl(skill) > 1 ? muliplier : 1f;

            case SkillSet.CRAFTING:
                return getLvl(skill) > 1 ? muliplier : 1f;

            case SkillSet.ENCHANTING:
                return getLvl(skill) > 1 ? muliplier : 1f;

            case SkillSet.TIMBER:
                return getLvl(skill) > 1 ? muliplier : 1f;

            case SkillSet.MINING:
                return getLvl(skill) > 1 ? muliplier : 1f;
            default:
                return 1f;
        }
    }

    public float SkillValueMultiplier(SkillSet skill, int muliplier)
    {
        switch (skill)
        {
            case SkillSet.FIGHTING:
                return getLvl(skill) > 1 ? muliplier : 1f;

            case SkillSet.CRAFTING:
                return getLvl(skill) > 1 ? muliplier : 1f;

            case SkillSet.ENCHANTING:
                return getLvl(skill) > 1 ? muliplier : 1f;

            case SkillSet.TIMBER:
                return getLvl(skill) > 1 ? muliplier : 1f;

            case SkillSet.MINING:
                return getLvl(skill) > 1 ? muliplier : 1f;
            default:
                return 1f;
        }
    }

    /// <summary>
    /// Takes skill and checking level as input, if certain level for wanted skill is reached returns true
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public bool IsLevelReached(SkillSet skill, int level)
    {
        switch (skill)
        {
            case SkillSet.FIGHTING:

                return getLvl(skill) >= level ? true : false;

            case SkillSet.CRAFTING:

                return getLvl(skill) >= level ? true : false;

            case SkillSet.ENCHANTING:

                return getLvl(skill) >= level ? true : false;

            case SkillSet.TIMBER:

                return getLvl(skill) >= level ? true : false;

            case SkillSet.MINING:

                return getLvl(skill) >= level ? true : false;
            default:
                return false;
        }
    }





}
