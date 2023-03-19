using System.Collections;
using UnityEngine;

public class FireCattle : Rune
{
    //set rune parameters
    private void Awake()
    {
        type = GlobalRune.Type.FIRE_CATTLE;
        strength = 8;
        buffDuration = 2;
        runeTargetTag = "AllEnemies";
        debuffChance = 30;
    }
    private float hitTime = 1.5f;
    public override void runeFX(GameObject target)
    {
        GameObject fireCattleFX = GlobalRune.Instance.getFxPrefab(type);
        Instantiate(fireCattleFX, GameObject.FindGameObjectWithTag("Player").transform);
    }

    public override void use(GameObject target)
    {
        runeFX(target);
        StartCoroutine(applyEffects(hitTime));
    }
    public IEnumerator applyEffects(float t)
    {
        yield return new WaitForSeconds(t);
        var arr = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in arr)
        {
            createBuff(go);
            //damages all active enemies units to list
            go.GetComponent<Unit>().takeDamage(strength);
        }
        BattleNarratorScript.narrator = "You used a fire/cattle combo!";
        PlayerLog.addEvent("You used a fire/cattle combo!");
        Destroy(gameObject);
    }

}
