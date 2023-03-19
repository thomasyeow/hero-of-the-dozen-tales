using UnityEngine;

public class SunTyr : Rune
{
    //set rune parameters
    private void Awake()
    {
        type = GlobalRune.Type.SUN_TYR;
        strength = 0;
        buffDuration = 3;
        runeTargetTag = "All";
        debuffChance = 100;
    }
    public override void runeFX(GameObject target)
    {
        //TODO if another eye exists, delete that one to refresh the eye's duration
        //Instantiate(GlobalRune.Instance.getFxPrefab(GlobalRune.Type.SUN_TYR));
    }

    public override void use(GameObject target)
    {
        createBuff(GameObject.FindGameObjectWithTag("Player"));
        BattleNarratorScript.narrator = "You have summoned TYR himself!";
        PlayerLog.addEvent("You used a Sun/Tyr combo!");
        Destroy(gameObject);
    }
}
