using System.Collections;
using UnityEngine;

public class FrostRune : Rune //Effect: Deals normal damage to one enemy with a 20% chance to freeze for one turn
{
    private float hitTime = 1.0f;
    //set rune parameters
    private void Awake()
    {
        type = GlobalRune.Type.ICE;
        strength = 5;
        buffDuration = 1;
        runeTargetTag = "Enemy";
        debuffChance = 20;
    }
    public override void runeFX(GameObject target)
    {
        //get iceshard animation clip which moves iceshard from user to target
        GameObject iceShard = GlobalRune.Instance.getFxPrefab(GlobalRune.Type.ICE);
        GameObject iceShardInstance = Instantiate(iceShard, GameObject.FindGameObjectWithTag("Player").transform);
        Animator animator = iceShardInstance.GetComponent<Animator>();
        switch (target.transform.position.y)
        {
            case 3:
                animator.SetInteger("direction", 1);
                break;
            case 1:
                animator.SetInteger("direction", 2);
                break;
            case -0.5f:
                animator.SetInteger("direction", 3);
                break;
        }
    }

    //uses the rune on a target, then destroys the rune
    public override void use(GameObject target)
    {
        if (target != null)
        {
            runeFX(target);
            StartCoroutine(applyEffects(hitTime, target));
        }
    }
    public IEnumerator applyEffects(float t, GameObject target)
    {
        yield return new WaitForSeconds(t);
        Unit targetUnit = target.GetComponent<Unit>();
        targetUnit.takeDamage(SkillSystemManager.Instance.IsLevelReached(SkillSet.FIGHTING, 1) ? strength * 1.25f : strength);
        createBuff(target);// adding buff to a list in enemy.unit | createbuff method calls unit addbuff method
        //print($"{this.name} was used into {target.name}");
        BattleNarratorScript.narrator = "You used a frost rune!";
        PlayerLog.addEvent("You used a frost rune!");
        Destroy(gameObject);
    }
}
