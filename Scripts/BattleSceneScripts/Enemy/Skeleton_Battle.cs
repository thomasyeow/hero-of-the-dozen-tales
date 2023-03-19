using UnityEngine;

public class Skeleton_Battle : Unit
{
    public GameObject attack1FX;
    Animator animator;

    private void Awake()
    {
        unitName = "Skeleton";
        unitLevel = 1;
        maxHP = 20;
        currentHP = maxHP;
    }
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public override float takeAction(Unit target)
    {
        animator.SetTrigger("Push");
        //instantiate slash gameObject on player
        Instantiate(attack1FX, target.transform);
        target.takeDamage(5);

        //return animation delay to NBS for delay between enemy turns
        //Debug.Log("Child class attack");
        return 0.5f;
    }

    public void OnDestroy()
    {
        //niepotrzebne
        /*foreach (Quest q in QuestManager.GetInstance().activeQuestsSO.activeQuests)
        {
            q.goal.EnemyKilled(KillType.Skeleton);
        }*/
    }
}
