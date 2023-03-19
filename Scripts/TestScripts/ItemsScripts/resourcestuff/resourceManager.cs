using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;



public class resourceManager : MonoBehaviour
{
  //  private static resourceManager _instance; public static resourceManager Instance { get { return _instance; } }

    public genericResourceScript wood;
    public genericResourceScript stone;
    public genericResourceScript metal;

    public RawImage woodimg;
    public RawImage stoneimg;

    public float range = 4;

    


    //public GameObject[] trees;
    //public GameObject hero;


    private void Awake()
    {
        #if UNITY_EDITOR
            EditorUtility.SetDirty(wood);
            EditorUtility.SetDirty(stone);
            EditorUtility.SetDirty(metal);
        #endif
        woodimg.color = new Color(woodimg.color.r, woodimg.color.b, woodimg.color.g, 0);
        stoneimg.color = new Color(stoneimg.color.r, stoneimg.color.b, stoneimg.color.g, 0);
    }



    private double timer = 0;               //Timer (used for chopping cooldown)
    private double timerCap = 3;            //Chopping cooldown time 

    public bool isTouchingTree = false;    //Checks if the player is close to any tree
    public bool isTouchingRock = false;


    void Update()
    {
        if (timer > 0)                                   //Needed for chopping cooldown
        {
            timer -= Time.deltaTime;
            
        }
        else
        {
            woodimg.color = new Color(woodimg.color.r, woodimg.color.b, woodimg.color.g, 0);
            stoneimg.color = new Color(stoneimg.color.r, stoneimg.color.b, stoneimg.color.g, 0);
        }
        //  if(woodimg.color.a == 255)


        //This is meant for a "transparency" fade in effect. Doesn't work in editor unless you manually set transparency to 255, but maybe will work in final product?
        woodimg.color = new Color(woodimg.color.r, woodimg.color.b, woodimg.color.g, woodimg.color.a - 3 * Time.deltaTime);
        stoneimg.color = new Color(stoneimg.color.r, stoneimg.color.b, stoneimg.color.g, stoneimg.color.a - 3 * Time.deltaTime);

        Vector3 direction = Vector3.back;       //so it's in front of hero

        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range));

        if (Physics.Raycast(theRay, out RaycastHit hit, range))
        {
            if (hit.collider.tag == "tree")
            {
                isTouchingTree = true;
            }
            else if (hit.collider.tag == "rock")
            {
                isTouchingRock = true;
            }
            else
            {
                isTouchingRock = false;
                isTouchingTree = false;
            }
        }

        ChopWood();
        MineRock();

        isTouchingTree = false;
        isTouchingRock = false;
    }

    private void ChopWood()
    {
        if (isTouchingTree == true)
        {
            if (Input.GetKeyDown("e") && timer <= 0)
            {
          //      Debug.Log("chopped");
                OverWorldManager.instance.playerState = OverWorldManager.PlayerState.COLLECTING;

                SkillSystemManager.AddExpEvent.Invoke(SkillSet.TIMBER, 10);

                if (SkillSystemManager.Instance.getLvl(SkillSet.TIMBER) > 1)
                {
                    wood.addAmount(SkillSystemManager.Instance.getLvl(SkillSet.TIMBER));

                    LootPopUpManager.instance.PopUpLoot("WOOD", SkillSystemManager.Instance.getLvl(SkillSet.TIMBER));

                    foreach (Quest q in QuestManager.GetInstance().activeQuests)
                    {
                        // add amount collected to quest
                        for (int i = 1; i <= SkillSystemManager.Instance.getLvl(SkillSet.TIMBER); i++)
                        {
                            q.goal.ItemCollected(GatherType.Wood);
                        }
                    }
                    GameEvents.instance.UpdateTrackedQuestTrigger();
                }
                else
                {
                    wood.addAmount(1);

                    LootPopUpManager.instance.PopUpLoot("WOOD", 1);
                    // add amount collected to quest
                    foreach (Quest q in QuestManager.GetInstance().activeQuests)
                    {
                        q.goal.ItemCollected(GatherType.Wood);
                    }
                    GameEvents.instance.UpdateTrackedQuestTrigger();
                }

                timer = timerCap;

                OverWorldManager.instance.playerState = OverWorldManager.PlayerState.PLAYING;

                woodimg.color = new Color(woodimg.color.r, woodimg.color.b, woodimg.color.g, 255);
            }
        }
    }

    private void MineRock()
    {
        if (isTouchingRock == true)
        {
            if (Input.GetKeyDown("e") && timer <= 0)
            {
              //  Debug.Log("mined");
                OverWorldManager.instance.playerState = OverWorldManager.PlayerState.COLLECTING;

                SkillSystemManager.AddExpEvent.Invoke(SkillSet.MINING, 10);
                if (SkillSystemManager.Instance.getLvl(SkillSet.MINING) > 1)
                {
                    stone.addAmount(SkillSystemManager.Instance.getLvl(SkillSet.MINING));

                    LootPopUpManager.instance.PopUpLoot("STONE", SkillSystemManager.Instance.getLvl(SkillSet.MINING));
                    foreach (Quest q in QuestManager.GetInstance().activeQuests)
                    {
                        // add amount collected to quest
                        for (int i = 1; i <= SkillSystemManager.Instance.getLvl(SkillSet.MINING); i++)
                        {
                            q.goal.ItemCollected(GatherType.Stone);
                        }
                    }
                    GameEvents.instance.UpdateTrackedQuestTrigger();
                }
                else
                {
                    stone.addAmount(1);
                    // add amount collected to quest
                    LootPopUpManager.instance.PopUpLoot("STONE", 1);
                    foreach (Quest q in QuestManager.GetInstance().activeQuests)
                    {
                        q.goal.ItemCollected(GatherType.Stone);
                    }
                    GameEvents.instance.UpdateTrackedQuestTrigger();
                }

                timer = timerCap;

                OverWorldManager.instance.playerState = OverWorldManager.PlayerState.PLAYING;

                stoneimg.color = new Color(stoneimg.color.r, stoneimg.color.b, stoneimg.color.g, 255);
            }
        }
    }

    private void OnApplicationQuit()
    {
        wood.resetAmt();
        stone.resetAmt();
        metal.resetAmt();
    }

}

