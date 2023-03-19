using System.Collections;
using UnityEngine;

public class FireRune : Rune //Effect: Deals normal damage, with a 20% chance to deal low DoT for 3 turns
{
    //when does the projectile hit, affect targets and despawn
    private float hitTime = 0.55f;
    private PlayerLog eventLog;

    //set rune parameters
    private void Awake()
    {
        type = GlobalRune.Type.FIRE;
        strength = 5;
        buffDuration = 2;
        runeTargetTag = "Enemy";
        debuffChance = 20;
        //debuffChance = 30;
    }
    public override void runeFX(GameObject target)
    {
        //get fireball animation clip which moves fireball from user to target
        GameObject fireball = GlobalRune.Instance.getFxPrefab(GlobalRune.Type.FIRE);
        
        GameObject fireballInstance = Instantiate(fireball, GameObject.FindGameObjectWithTag("Player").transform);
        Animator fireballAnimator = fireballInstance.GetComponent<Animator>();
        fireballAnimator.speed = 1.5f;
        switch (target.transform.position.y)
        {
            case 3:
                fireballAnimator.SetInteger("direction", 1);
                break;
            case 1:
                fireballAnimator.SetInteger("direction", 2);
                break;
            case -0.5f:
                fireballAnimator.SetInteger("direction", 3);
                break;
        }

        /*
        AnimationClip clip = fireball.GetComponent<Animator>().runtimeAnimatorController.animationClips[1];
        //x position transform
        Keyframe[] keys = new Keyframe[2];
        keys[0] = new Keyframe(0.0f, 0.0f);
        keys[1] = new Keyframe(hitTime, target.transform.position.x + 7);
        Debug.Log("X: " + target.transform.position.x + 7);
        AnimationCurve curveX = new AnimationCurve(keys);
        //y position transform
        Keyframe[] yKeys = new Keyframe[2];
        yKeys[0] = new Keyframe(0.0f, 0.0f);
        yKeys[1] = new Keyframe(hitTime, target.transform.position.y);
        Debug.Log("Y: " + target.transform.position.y);
        AnimationCurve curveY = new AnimationCurve(yKeys);
        //assign curves to clip
        clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
        */
        
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

    //apply effects with delay
    public IEnumerator applyEffects(float t, GameObject target)
    {
        yield return new WaitForSeconds(t);
        Unit targetUnit = target.GetComponent<Unit>();
        targetUnit.takeDamage(SkillSystemManager.Instance.IsLevelReached(SkillSet.FIGHTING, 1) ? strength * 1.25f : strength);

        createBuff(target);// adding buff to a list in enemy.unit | createbuff method calls unit addbuff method
        //print($"{this.name} was used into {target.name}");
        BattleNarratorScript.narrator = "You used a fire rune!";
        PlayerLog.addEvent("You used a fire rune!");
        Destroy(gameObject);
    }
}
