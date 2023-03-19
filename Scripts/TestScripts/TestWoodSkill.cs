using UnityEditor;
using UnityEngine;

public class TestWoodSkill : MonoBehaviour
{
    //Basic implementation of woodchopping skill & experience

    private float WoodSkillLevel = 1;       //current woodcutting level
    private float WoodSkillExp = 0;         //Current woodcutting experience
    private float WoodExpLevelReq = 5;      //Current requirement for woodcutting level up

    private double timer = 0;               //Timer (used for chopping cooldown)
    private double timerCap = 2;            //Chopping cooldown time (as level goes up, this goes down)

    //  private float WoodAmt = 0;              //Current amount of wood
    //instead we now use InventoryScript.WoodAmount

    private Rigidbody2D rb;

    private bool isTouchingTree = false;    //Checks if the player is close to any tree

    [SerializeField]
    private ResourcesSO woodSO;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (timer > 0)                                   //Needed for chopping cooldown
            timer -= Time.deltaTime;

        ChopWood();
        CheckStats();
    }

    private void WoodLevelUp()                          //Skill level up
    {
        WoodExpLevelReq += 5;
        WoodSkillExp = 0;
        WoodSkillLevel++;
        if (timerCap >= 0.5)
            timerCap -= 0.1;
        // Debug.Log("Level up! you are now level " + WoodSkillLevel);
    }
    private void ChopWood()
    {
        if (isTouchingTree == true)
        {
            if (Input.GetKeyDown("e") && timer <= 0)
            {
                OverWorldManager.instance.playerState = OverWorldManager.PlayerState.COLLECTING;

                //SkillSystemManager.Instance.addExp(SkillSystemManager.SkillSet.TIMBER, 10f);
                SkillSystemManager.AddExpEvent(SkillSet.TIMBER, 10f);
                SkillSystemManager.Instance.GetExp(SkillSet.TIMBER);
                //     InventoryScript.WoodAmount++;
                woodSO.Value++;
            #if UNITY_EDITOR
                EditorUtility.SetDirty(woodSO);
            #endif
                //     WoodAmt++;
                WoodSkillExp++;

                timer = timerCap;

                if (WoodSkillExp >= WoodExpLevelReq)
                    WoodLevelUp();

                //    Debug.Log("You have " + InventoryScript.WoodAmount + " wooden logs!");
                OverWorldManager.instance.playerState = OverWorldManager.PlayerState.PLAYING;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("tree"))
        {
            isTouchingTree = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        isTouchingTree = false;
    }

    private void CheckStats()                           //Check current skill stats
    {
        if (Input.GetKeyDown("p"))
        {
            Debug.Log("Wood chopping level: " + WoodSkillLevel);
            Debug.Log("Current Wood chopping experience: " + WoodSkillExp);
            Debug.Log("Experience needed for level up: " + (WoodExpLevelReq - WoodSkillExp));
        }
    }

}
