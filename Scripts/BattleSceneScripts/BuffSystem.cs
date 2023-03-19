using UnityEngine;

public enum BuffName
{
    //buffs from runes
    frost, fire, yew, hail, cattle,
    fire_ice, fire_cattle, sun_tyr,
};

public class BuffSystem : MonoBehaviour

{
    //BUFF FX PREFABS
    public GameObject sunTyrEye;
    public int counter;
    public BuffName activeBuff;

    //execute buff based on this.activeBuff
    public void execBuff(Unit unit)
    {
        switch (activeBuff)
        {
            //RUNE BUFFS
            case BuffName.frost:
                frostEffect(unit);
                //Debug.Log("ALE MROZI");
                break;
            case BuffName.fire:
                fireEffect(unit);
                //Debug.Log("Gorąco Dziś");
                break;
            case BuffName.yew:
                yewEffect(unit);
                //Debug.Log("Twardy");
                break;
            case BuffName.hail:
                hailEffect(unit);
                //Debug.Log("hailstorm");
                break;
            case BuffName.cattle:
                cattleEffect(unit);
                //Debug.Log("MOOO");
                break;
            case BuffName.fire_ice:
                fireIceEffect(unit);
                break;
            case BuffName.fire_cattle:
                fireCattleEffect(unit);
                break;
            case BuffName.sun_tyr:
                sunTyrEffect(unit);
                break;
        }
        counter -= 1;
        if (counter < 0)
        {
            //Debug.Log($"destroying: {this}");
            Destroy(this);
        }

    }

    private void frostEffect(Unit unit)
    {

        unit.canAttack = counter <= 0;
        if (!unit.canAttack)
        {
            Instantiate(GlobalRune.Instance.getFxPrefab(GlobalRune.Type.FREEZE_FX), unit.transform);
        }
    }
    private void fireEffect(Unit unit)
    {
        if (unit != null)
        {
            if (counter > 0)
            {
                Debug.Log("Fire rune burn applied");
                //spawn burning fx a small random distance away from unit's transform
                Vector3 burnPos = unit.transform.position;
                burnPos.x += Random.Range(-0.5f, 0.5f);
                burnPos.y += Random.Range(-0.5f, 0.5f);
                Instantiate(GlobalRune.Instance.getFxPrefab(GlobalRune.Type.BURNING),
                    burnPos, Quaternion.Euler(0, 0, 270), unit.transform);
                //Instantiate(GlobalRune.Instance.getFxPrefab(GlobalRune.Type.BURNING),
                //unit.transform);
                unit.takeDamage(4);
            }
        }
    }
    private void yewEffect(Unit unit)
    {
        unit.damageResistance = counter > 0 ? 0.33f : 1;
    }
    private void hailEffect(Unit unit)
    {
        if (counter > 0)
        {
            unit.takeDamage(4);
        }
    }
    private void cattleEffect(Unit unit)
    {
        unit.canAttack = counter <= 0;
        unit.gameObject.GetComponentInChildren<SpriteRenderer>().material.color = Color.white;
        GameObject cow = GlobalRune.Instance.getFxPrefab(GlobalRune.Type.CATTLE);
        //Debug.Log("Boop");
        Destroy(FindObjectOfType(cow.GetType()));
    }

    //COMBO effects
    private void fireIceEffect(Unit unit)
    {
        unit.canAttack = counter <= 0;
        if (counter > 0)
        {
            unit.takeDamage(4);
        }
    }
    private void fireCattleEffect(Unit unit)
    {
        if (counter > 0)
        {
            Debug.Log("fire cattle effect");
            unit.takeDamage(4);
        }
    }

    private void sunTyrEffect(Unit unit)
    {
        Debug.Log("sun tyr effect");
        //if effect just started, spawn in eye

        if (counter > 0)
        {
            sunTyrEye.GetComponent<Animator>().SetTrigger("Blink");
            Debug.Log("using sun rune");
            Instantiate(GlobalRune.Instance.getRunePrefab(GlobalRune.Type.SUN)).GetComponent<Rune>().use(null);
        }
        else
        {
            Destroy(sunTyrEye);
        }

    }

    public void setBuffName(GlobalRune.Type type)
    {
        switch (type)
        {
            case GlobalRune.Type.ICE:
                activeBuff = BuffName.frost;
                break;
            case GlobalRune.Type.FIRE:
                activeBuff = BuffName.fire;
                break;
            case GlobalRune.Type.YEW:
                activeBuff = BuffName.yew;
                break;
            case GlobalRune.Type.HAIL:
                activeBuff = BuffName.hail;
                break;
            case GlobalRune.Type.CATTLE:
                activeBuff = BuffName.cattle;
                break;
            case GlobalRune.Type.FIRE_ICE:
                activeBuff = BuffName.fire_ice;
                break;
            case GlobalRune.Type.FIRE_CATTLE:
                activeBuff = BuffName.fire_cattle;
                break;
            case GlobalRune.Type.SUN_TYR:
                activeBuff = BuffName.sun_tyr;
                //instantiate sun_tyr eye
                Destroy(sunTyrEye);
                sunTyrEye = Instantiate(GlobalRune.Instance.getFxPrefab(GlobalRune.Type.SUN_TYR));
                break;
        }
    }
}
