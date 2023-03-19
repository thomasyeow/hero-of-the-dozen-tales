using System.Collections;
using UnityEngine;

public class BanditBattle : Unit
{
    public GameObject slashFX;
    public GameObject coinExplosionFX;
    public GameObject smokeBombFX;
    Animator anim;

    Animator animator;
    public Money money;
    public int stolenMoney = 0;
    private bool chargedUp = false;
    private int turnCounter = 0;

    private void Awake()
    {
        unitName = "Bandit";
        unitLevel = 2;
        maxHP = 25;
        currentHP = maxHP;
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    //TODO implement teamwork attack boost
    public override float takeAction(Unit target)
    {
        turnCounter++;

        //after 5 turns, escape
        if (turnCounter == 7)
        {
            Instantiate(smokeBombFX, gameObject.transform);
            Debug.Log("Burp");
            StartCoroutine(smokeBomb());

        }
        if (currentHP > maxHP / 2)
        {
            if (anim.GetBool("charged"))
            {
                animator.SetTrigger("Push");
                Instantiate(slashFX, target.transform);
                //spawn coin explosion fx prefab
                Instantiate(coinExplosionFX, target.transform);
                target.takeDamage(10);
                stolenMoney += 10;
                anim.SetBool("charged", false);
                return 0.5f;
            }
            else
            {
                anim.SetBool("charged", true);
                return 0.5f;
            }
        }
        animator.SetTrigger("Push");
        Instantiate(slashFX, target.transform);
        target.takeDamage(5);
        return 0.5f;
    }

    IEnumerator smokeBomb()
    {
        yield return new WaitForSeconds(0.5f);
        //TEMP need a better way to remove enemies
        Debug.Log("Boop");
        money.money -= stolenMoney;
        Destroy(gameObject);
    }

}
