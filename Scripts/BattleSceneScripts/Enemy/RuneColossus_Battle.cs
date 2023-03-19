using System.Collections;
using UnityEngine;

public class RuneColossus_Battle : Unit
{
    Animator colossusAnimator;
    //runeBeam prefab for charge and attack animations
    public GameObject runeBeam;
    //attack damage multiplier for rune colossus' charge
    private int attackMultiplier = 1;
    private void Awake()
    {
        unitName = "Rune Colossus";
        unitLevel = 2;
        maxHP = 90;
        currentHP = maxHP;
    }
    private void Start()
    {
        colossusAnimator = gameObject.GetComponent<Animator>();
    }

    public override float takeAction(Unit target)
    {
        //if hp is above half and attack multiplier is below 5, use CHARGE action
        if (currentHP > maxHP / 2 && attackMultiplier < 5)
        {
            Debug.Log("colossus charge");
            colossusAnimator.SetTrigger("Charge");
            attackMultiplier += 1;
            //instantiate runeBeam prefab for charge FX
            GameObject runeBeamObj1 = Instantiate(runeBeam, gameObject.transform);

            StartCoroutine(waitToDespawnBeam(runeBeamObj1, 1.0f, target));
            return 1.2f;
        }
        //if hp is below half or attackMulltiplier >= 5, use RUNEBEAM action
        Debug.Log("colossus rune beam");
        colossusAnimator.SetTrigger("Charge");

        GameObject runeBeamObj = Instantiate(runeBeam, gameObject.transform);
        runeBeamObj.transform.localScale *= 4;
        runeBeamObj.GetComponent<Animator>().SetTrigger("runeBeamFire");
        StartCoroutine(waitToDespawnBeam(runeBeamObj, 4.0f, target));

        return 4.5f;
    }

    private IEnumerator waitToDespawnBeam(GameObject runeBeamObj1, float duration, Unit target)
    {

        yield return new WaitForSeconds(duration);
        runeBeamObj1.GetComponent<Animator>().SetTrigger("runeBeamDespawn");
        //if wait time is for 4.0f(passed to this function when using runebeam),
        //make player take damage
        if (duration == 4.0f)
        {
            target.takeDamage(7 * attackMultiplier);
        }

    }

    //on kill, update quest logs
    public void OnDestroy()
    {
        //niepotrzebne
        /*foreach (Quest q in QuestManager.GetInstance().activeQuestsSO.activeQuests)
        {
            q.goal.EnemyKilled(KillType.RuneColossus);
        }*/
    }


}
