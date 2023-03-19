using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public abstract class Rune : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
    , IPointerDownHandler
    , IPointerUpHandler
{
    private ComboPanel comboPanel;

    public string description;
    //RUNE PARAMETERS(set in each rune class)

    protected string runeTargetTag = "";//is target Player or Enemy, allEnemies or All
    protected GlobalRune.Type type;
    protected int buffDuration;
    protected int strength;
    protected int debuffChance;
    //END OF RUNE PARAMETERS
    private bool comboPanelUsed = false;
    private void Start()
    {
        comboPanel = FindObjectOfType<ComboPanel>();
    }
    public abstract void use(GameObject target);

    //abstract rune animation method, implemented by each rune
    public abstract void runeFX(GameObject target);

    public void createBuff(GameObject target)
    {
        int rnd = UnityEngine.Random.Range(1, 100);
        if (rnd <= debuffChance)
        {
            BuffSystem buff = target.AddComponent<BuffSystem>();
            buff.counter = buffDuration;
            buff.setBuffName(type);
            target.GetComponent<Unit>().addBuff(buff);
        }
    }

    //choose target with the right tag
    public GameObject chooseTarget()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        //if rune is dropped on rune combo panel, comboPanelUsed = true and rune is moved into ComboPanel
        if (hit.collider != null && hit.collider.gameObject.CompareTag("ComboPanel"))
        {
            comboPanelUsed = true;
            return null;
        }
        //if the rune is supposed to target player or enemies, AND something was targeted
        else if ((runeTargetTag.Equals("Enemy") || runeTargetTag.Equals("Player")) && hit.collider != null)
        {
            GameObject target = null;
            //if target is valid for this rune
            if (hit.collider.gameObject.CompareTag(runeTargetTag))
            {
                target = hit.collider.gameObject;
            }
            return target;
        }

        else return null;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //show combo panel when holding a rune
        comboPanel.showComboPanel();
        HighlightPossibleTargets();
        HideOtherRunes();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        try
        {
            //if the combo panel is visible, don't use rune
            GameObject target = chooseTarget();
            if (comboPanelUsed)
            {

                comboPanel.spawnRune(type);
                Destroy(gameObject);
            }
            else
            {
                use(target);
                //hide combo panel if a rune was used
                //comboPanel.hideComboPanel();
            }

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        StopHighlightPossibleTargets();
        ShowOtherRunes();


    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        EnlargeRune();
        //do stuff with card on mouse enter(IE increase scale)
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ReduceRune();
        //do stuff with card on mouse exit(IE lower scale)
    }

    public void HighlightPossibleTargets()//highlights possible targets when when the rune is getting used
    {
        var arrEnemyGO = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        switch (runeTargetTag)
        {
            case "All":
                foreach (GameObject go in arrEnemyGO)
                {
                    highlight(go, Color.red);
                    //go.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                }
                highlight(playerGO, Color.red);
                //playerGO.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                break;
            case "AllEnemies":
                foreach (GameObject go in arrEnemyGO)
                {
                    highlight(go, Color.red);
                    //go.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                }
                break;
            case "Enemy":
                foreach (GameObject go in arrEnemyGO)
                {
                    highlight(go, Color.green);
                    //go.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    go.GetComponentInChildren<Unit>().canHighlight = true;
                }
                break;
            case "Player":
                highlight(playerGO, Color.green);
                //playerGO.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                playerGO.GetComponentInChildren<Unit>().canHighlight = true;
                break;

        }
    }

    public void StopHighlightPossibleTargets() //stops highlighting targets after the rune was used
    {
        var arrEnemyGO = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        switch (runeTargetTag)
        {
            case "All":
                foreach (GameObject go in arrEnemyGO)
                {
                    highlight(go, Color.white);
                    //go.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
                highlight(playerGO, Color.white);
                //playerGO.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                break;
            case "AllEnemies":
                foreach (GameObject go in arrEnemyGO)
                {
                    highlight(go, Color.white);
                    //go.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                }
                break;
            case "Enemy":
                foreach (GameObject go in arrEnemyGO)
                {
                    highlight(go, Color.white);
                    //go.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    go.GetComponentInChildren<Unit>().canHighlight = false;
                }
                break;
            case "Player":
                highlight(playerGO, Color.white);
                //playerGO.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                playerGO.GetComponentInChildren<Unit>().canHighlight = false;
                break;

        }
    }

    public void HideOtherRunes()
    {
        var arr = GameObject.FindGameObjectsWithTag("Rune");
        foreach (GameObject go in arr)
        {
            if (!go.Equals(gameObject))
            {
                go.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.1f);
            }
        }
    }
    public void ShowOtherRunes()
    {
        var arr = GameObject.FindGameObjectsWithTag("Rune");
        foreach (GameObject go in arr)
        {
            if (!go.Equals(gameObject))
            {
                go.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
    public void EnlargeRune()
    {

        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);
    }

    public void ReduceRune()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
    }

    //highlight object sprite renderers with colors
    private void highlight(GameObject go, Color color)
    {
        foreach (SpriteRenderer sr in go.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.color = color;
        }
    }
}
