using System.Collections;
using UnityEngine;

public class GiftRune : Rune //Effect: Heals a target a normal amount
{
    //set rune parameters
    private void Awake()
    {
        type = GlobalRune.Type.GIFT;
        strength = 10;
        buffDuration = 0;
        runeTargetTag = "Player";
        debuffChance = 100;
    }
    //function used for FX instantiation in battlescene
    public override void runeFX(GameObject target)
    {
        Instantiate(GlobalRune.Instance.getFxPrefab(GlobalRune.Type.GIFT), target.transform);
    }

    //uses the rune on a target, then destroys the rune
    public override void use(GameObject target)
    {
        if (target != null)
        {
            runeFX(target);
            StartCoroutine(applyEffects(1, target));

        }
    }

    //function that allows for delay before applying rune effects
    public IEnumerator applyEffects(float t, GameObject target)
    {
        yield return new WaitForSeconds(t);
        Unit targetUnit = target.GetComponent<Unit>();
        targetUnit.heal(SkillSystemManager.Instance.IsLevelReached(SkillSet.FIGHTING, 1) ? strength * 1.25f : strength);
        //print(targetUnit.currentHP);
        //print($"{this.name} was used into {target.name}");
        BattleNarratorScript.narrator = "You used a gift rune!";
        PlayerLog.addEvent("You used a gift rune!");
        Destroy(gameObject);
    }
}

