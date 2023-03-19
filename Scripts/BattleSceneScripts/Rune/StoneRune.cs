using System.Collections;
using UnityEngine;

public class StoneRune : Rune //Effect: Deals high damage to a single target
{
    private float hitTime = 1;
    //set rune parameters
    private void Awake()
    {
        type = GlobalRune.Type.STONE;
        strength = 15;
        buffDuration = 0;
        runeTargetTag = "Enemy";
        debuffChance = 0;
    }
    public override void runeFX(GameObject target)
    {
        //get stone animation clip which moves stone from user to target
        GameObject stone = GlobalRune.Instance.getFxPrefab(GlobalRune.Type.STONE);
        GameObject stoneInstance = Instantiate(stone, GameObject.FindGameObjectWithTag("Player").transform);
        Animator animator = stoneInstance.GetComponent<Animator>();
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

    //use the rune on a target, then destroy the rune
    public override void use(GameObject target)
    {
        if (target != null)
        {
            runeFX(target);
            StartCoroutine(applyEffects(hitTime, target));
        }
    }

    //apply effects with delay
    public IEnumerator applyEffects(float t, GameObject target)
    {
        yield return new WaitForSeconds(t);
        Unit targetUnit = target.GetComponent<Unit>();
        targetUnit.takeDamage(SkillSystemManager.Instance.IsLevelReached(SkillSet.FIGHTING, 1) ? strength * 1.25f : strength);
        print($"{name} was used into {target.name}");
        BattleNarratorScript.narrator = "You used a stone rune!";
        PlayerLog.addEvent("You used a stone rune!");
        Destroy(gameObject);
    }
}
