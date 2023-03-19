using UnityEngine;

public class HailRune : Rune //Effect: Does low DoT over 3 turns to all enemies
{
    //set rune parameters
    private void Awake()
    {
        type = GlobalRune.Type.FIRE;
        strength = 0;
        buffDuration = 3;
        runeTargetTag = "AllEnemies";
        debuffChance = 100;
    }
    public override void runeFX(GameObject target)
    {
        Instantiate(GlobalRune.Instance.getFxPrefab(GlobalRune.Type.HAIL));
    }

    public override void use(GameObject target)
    {

        runeFX(target);
        var arr = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in arr)
        {
            createBuff(go); ;//adds buff to every enemy unit
        }
        BattleNarratorScript.narrator = "You used a hail rune!";
        PlayerLog.addEvent("You used a hail rune!");
        Destroy(gameObject);
    }
}
