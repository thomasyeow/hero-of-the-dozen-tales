using System.Collections;
using UnityEngine;

public class TyrRune : Rune //Effect: Evens out health percentages(rounding up) between 2 targets, rounding down. For example, if target A has 50% hp and target B has 75% hp, they both will have 62% hp.
{
    //set rune parameters
    private void Awake()
    {
        type = GlobalRune.Type.TYR;
        strength = 0;
        buffDuration = 0;
        runeTargetTag = "Enemy";
        debuffChance = 0;
    }
    public override void runeFX(GameObject target)
    {
        Instantiate(GlobalRune.Instance.getFxPrefab(GlobalRune.Type.TYR));
    }

    public override void use(GameObject target)
    {
        if (target != null)
        {
            runeFX(target);
            StartCoroutine(applyEffects(1.5f, target));
        }
    }

    public IEnumerator applyEffects(float t, GameObject target)
    {

        yield return new WaitForSeconds(t);
        Unit playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        Unit enemyUnit = target.GetComponent<Unit>();
        float hp = ((playerUnit.currentHP / playerUnit.maxHP) + (enemyUnit.currentHP / enemyUnit.maxHP)) / 2;
        Debug.Log(hp);
        playerUnit.changeCurrentHP(hp * playerUnit.maxHP);
        enemyUnit.changeCurrentHP(hp * enemyUnit.maxHP);
        BattleNarratorScript.narrator = "You used a tyr rune!";
        PlayerLog.addEvent("You used a tyr rune!");
        Destroy(gameObject);
    }
}
