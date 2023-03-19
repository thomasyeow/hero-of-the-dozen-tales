using UnityEngine;

public class HorseRune : Rune //TODO:Effect: Gain the ability to cast 2 attack in one turn(after this turn) for 2 turns
{
    public override void runeFX(GameObject target)
    {
        throw new System.NotImplementedException();
    }

    public override void use(GameObject target) //TODO: this
    {
        if (target != null)
        {
            Destroy(gameObject);
            BattleNarratorScript.narrator = "You used a horse rune!";
            PlayerLog.addEvent("You used a horse rune!");
        }

    }
}
