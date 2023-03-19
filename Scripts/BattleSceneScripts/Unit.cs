using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int ID = 0;
    public string unitName;
    public int unitLevel;
    public float damage;
    public float strength = 1;//multiplier for rune damage
    public float maxHP;
    public float currentHP;
    public float damageResistance = 1;//1 means that unit takes full damage, 0 - no damage

    public float armor = 0;           //"armor" of a unit (mostly used for hero - it is set in NBS)
    public float power = 0;           //additional power of a unit

    public HealthBar healthBar;
    public bool canAttack = true;
    public KillType killType;
    //list of this unit's currently active (de)buffs
    List<BuffSystem> activeBuffs = new List<BuffSystem>();

    //Reference to NewBattleSystem to detect battle win/loss
    public NewBattleSystem nbs;

    public bool canHighlight = false;

    private void Start()
    {
        nbs = FindObjectOfType<NewBattleSystem>();
        healthBar.SetMaxHealth(maxHP);
        healthBar.SetHealth(currentHP);

       // damageResistance = 1 - (armor/100);             //we dont currently use this per se (instead, we directly use armor)
    }
    //do damage to this unit(renamed from "attack" - TY)
    public void takeDamage(float damage)
    {
        float finaldamage = damage - (armor/10);         //final damage takes into account resistances

        finaldamage += finaldamage * (power * 2 / 100);                      //final damage takes into account power of player (items, perhaps skills?)

        if (finaldamage < damage * 0.2)                         //if damage is less than 20% of original, change to 10% of original damage (so there is a minimum dmg)
            finaldamage = (float)(damage * 0.2);                

        currentHP = currentHP - finaldamage;
        //on enemy killed
        if (currentHP <= 0)
        {
            
            foreach (Quest q in QuestManager.GetInstance().activeQuests)
            {
                q.goal.EnemyKilled(killType);
            }
            //nbs.victoryScreen(true); //<-doesnt work
            //nbs.replaceEnemy(gameObject);
            //Debug.Log("hi, " + nbs.activeEnemies.Keys.Count + " enemies left");
            Destroy(gameObject);
        }
        if (unitName != "Enemy")
            PlayerLog.addEvent("You were attacked, and sustained " + finaldamage + " damage!");
        healthBar.SetHealth(currentHP);
        healthBar.SetMaxHealth(maxHP);
        gameObject.GetComponent<TextBlink>().Blink("-" + finaldamage.ToString(), 2);
        SFXManager.PlaySFX?.Invoke(SoundGenere.HIT);
    }
    //enemy action, takes reference to player gameObject in NBS,
    //returns action duration to determine delay
    public virtual float takeAction(Unit target)
    {
        Debug.Log("Parent class attack");
        return 0.0f;
    }

    public void heal(float amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        healthBar.SetHealth(currentHP);
        healthBar.SetMaxHealth(maxHP);
    }

    public void changeCurrentHP(float amount)
    {
        currentHP = amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        healthBar.SetHealth(currentHP);
        healthBar.SetMaxHealth(maxHP);
    }

    public bool isAlive(Unit unit)
    {
        return unit.currentHP > 0;
    }


    public void execBuffs()
    {
        foreach (BuffSystem bn in activeBuffs.ToArray())
        {
            if (bn.Equals(null))
            {
                activeBuffs.Remove(bn);
            }
            else
            {
                bn.execBuff(this);
            }
        }
    }

    public void addBuff(BuffSystem buff)
    {
        activeBuffs.Add(buff);
    }

    private void OnMouseEnter()
    {
        if (canHighlight)
        {

            foreach (SpriteRenderer sr in gameObject.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = Color.red;
            }
            //gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        if (canHighlight)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
    }


}

