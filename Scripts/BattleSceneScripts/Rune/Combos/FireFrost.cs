using System.Collections;
using UnityEngine;

public class FireFrost : Rune
{
    private float hitTime = 0.7f;
    //set rune parameters
    private void Awake()
    {
        type = GlobalRune.Type.FIRE_ICE;
        strength = 10;
        buffDuration = 2;
        runeTargetTag = "Enemy";
        debuffChance = 40;
    }
    public override void runeFX(GameObject target)
    {
        GameObject fireIceFX = GlobalRune.Instance.getFxPrefab(type);
        GameObject fireIceInstance = Instantiate(fireIceFX, GameObject.FindGameObjectWithTag("Player").transform);
        Animator animator = fireIceInstance.GetComponent<Animator>();
        animator.speed = 1.2f;
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
        targetUnit.takeDamage(strength);
        createBuff(target);
        BattleNarratorScript.narrator = "You used a fire/ice combo!";
        PlayerLog.addEvent("You used a fire/ice combo!");
        Destroy(gameObject);
    }
}
