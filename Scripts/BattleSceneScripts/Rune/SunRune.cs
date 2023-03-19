using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRune : Rune //Effect: Deals normal damage to everyone on the battlefield(including the player)
{
    //set rune parameters
    private void Awake()
    {
        type = GlobalRune.Type.SUN;
        strength = 10;
        buffDuration = 0;
        runeTargetTag = "All";
        debuffChance = 0;
    }
    public override void runeFX(GameObject target)
    {
        Instantiate(GlobalRune.Instance.getFxPrefab(GlobalRune.Type.SUN));
    }

    public override void use(GameObject target)
    {
        runeFX(target);
        StartCoroutine(applyEffects(2));
    }

    public IEnumerator applyEffects(float t)
    {
        yield return new WaitForSeconds(t);
        List<Unit> listofUnits = new List<Unit>();
        listofUnits.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>());//adds player unit
        var arr = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in arr)
        {
            listofUnits.Add(go.GetComponent<Unit>());//adds all active enemies units
        }
        foreach (Unit u in listofUnits)
        {
            u.takeDamage(SkillSystemManager.Instance.IsLevelReached(SkillSet.FIGHTING, 1) ? strength * 1.25f : strength);
        }
        BattleNarratorScript.narrator = "You used a sun rune!";
        PlayerLog.addEvent("You used a sun rune!");
        Destroy(gameObject);
    }
}
