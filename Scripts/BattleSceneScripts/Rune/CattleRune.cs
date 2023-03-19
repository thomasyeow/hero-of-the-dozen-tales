using UnityEngine;

public class CattleRune : Rune //Effect: The enemy cannot take any actions in cow form
{
    //set rune parameters
    private void Awake()
    {
        type = GlobalRune.Type.CATTLE;
        strength = 0;
        buffDuration = 2;
        runeTargetTag = "Enemy";
        debuffChance = 100;
    }
    public override void runeFX(GameObject target)
    {
        GameObject cow = GlobalRune.Instance.getFxPrefab(GlobalRune.Type.CATTLE);
        SpriteRenderer sr = target.GetComponentInChildren<SpriteRenderer>();
        sr.material.color = Color.clear;
        Instantiate(cow, target.transform);
    }

    public override void use(GameObject target) //TODO: this
    {
        if (target != null)
        {
            runeFX(target);
            createBuff(target);
            BattleNarratorScript.narrator = "You used a cattle rune!";
            PlayerLog.addEvent("You used a cattle rune!");
            Destroy(gameObject);
        }

    }
}
