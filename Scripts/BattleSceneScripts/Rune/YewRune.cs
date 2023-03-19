using UnityEngine;

public class YewRune : Rune //Effect: Take 1/3 damage for 2 turns
{
    //set rune parameters
    private void Awake()
    {
        type = GlobalRune.Type.YEW;
        strength = 0;
        buffDuration = 2;
        runeTargetTag = "Player";
        debuffChance = 100;
    }
    public override void runeFX(GameObject target)
    {
        throw new System.NotImplementedException();
    }

    public override void use(GameObject target)
    {
        if (target != null)
        {
            createBuff(target);
            BattleNarratorScript.narrator = "You used a yew rune!";
            PlayerLog.addEvent("You used a yew rune!");
            Destroy(gameObject);
        }
    }
}
