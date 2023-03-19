using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

//Singleton class for simplified saving system with no additional scripts
public class NewSaveSystem : MonoBehaviour
{
    
    //Nested class for storing save data
    [Serializable]
    public class SaveData
    {
        public Vector3 playerPos;
        public List<int> destroyedEnemyIdList;
        public List<Quest> allQuests;
        public List<Quest> activeQuests;
        public Quest trackedQuest;
        public List<int> runeInv;
        public List<int> deckList;
        public GameState gameState;
    }

    //prepare destination file
    const String SAVENAME = "/HotDT.wassup";
    
    //Referenced game objects
    public GameObject player;
    public OverWorldManager overWorldManager;

    public void saveGame()
    {
        string saveFile = Application.persistentDataPath + SAVENAME;
        SaveData saveData = new SaveData();

        //assign game state to saveData
        saveData.playerPos = player.transform.position;
        saveData.destroyedEnemyIdList = OverWorldManager.instance.destroyedEnemyIDList;
        Debug.Log("dead enemies at save: " + OverWorldManager.instance.destroyedEnemyIDList.Count);
        //all quests
        saveData.allQuests = QuestManager.GetInstance().allQuests;
        //active quests
        saveData.activeQuests = QuestManager.GetInstance().activeQuests;
        //tracked quest
        saveData.trackedQuest = QuestManager.GetInstance().trackedQuest;
        //runes in inventory
        Dictionary<GlobalRune.Type, int> runeInvDict;
        runeInvDict = GlobalRune.runeInventory();
        saveData.runeInv = new List<int>();
           for (int i = 0; i < runeInvDict.Keys.Count; i++)
            {
                saveData.runeInv.Add(runeInvDict[(GlobalRune.Type)i]);
            }
        //runes in deck
        Dictionary<GlobalRune.Type, int> deckListDict;
        deckListDict = GlobalRune.getDeckList();
        saveData.deckList = new List<int>();
            for (int i = 0; i < deckListDict.Keys.Count; i++)
            {
                saveData.deckList.Add(deckListDict[(GlobalRune.Type)i]);
            }
        //state of the game
        saveData.gameState = GameStateMachine.GetInstance().GetGameState();

        //convert saveData to Json
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveFile, json);
        Debug.Log(json);
    }

    public void loadGame()
    {
        string saveFile = Application.persistentDataPath + SAVENAME;
        if (File.Exists(saveFile))
        {

            String json = File.ReadAllText(saveFile);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            //LOAD EVERYTHING
            //player pos
            Input.ResetInputAxes();
            player.transform.position = saveData.playerPos;
            //enemy persistent deaths
            Debug.Log("Enemies killed: " + saveData.destroyedEnemyIdList.Count);
            foreach (EnemyDescription enemy in FindObjectsOfType<EnemyDescription>(true))
            {
                
                if (saveData.destroyedEnemyIdList.Contains(enemy.ID)){
                    Debug.Log("boop");
                    enemy.gameObject.SetActive(false);
                } else
                {
                     enemy.gameObject.SetActive(true);
                }
            }
            //all quests possible
            QuestManager.GetInstance().allQuests = saveData.allQuests;
            //annoying global variables in INK
            foreach(Quest q in saveData.allQuests)
            {
                if (q.questVar != "")
                {
                    if (q.returned)
                    {
                        GameObject.Find("SetDialogueGlobals").GetComponent<SetGlobals>().ChangeVariable(q.questVar, "questReturned");
                    }
                    else if (q.isActive)
                    {
                        if (q.goal.isReached())
                        {
                            GameObject.Find("SetDialogueGlobals").GetComponent<SetGlobals>().ChangeVariable(q.questVar, "questDone");
                        }
                        else
                        {
                            GameObject.Find("SetDialogueGlobals").GetComponent<SetGlobals>().ChangeVariable(q.questVar, "inProgress");
                        }
                    } else
                    {
                        GameObject.Find("SetDialogueGlobals").GetComponent<SetGlobals>().ChangeVariable(q.questVar, ""); 
                    }
                }
            }
            //active quests
            QuestManager.GetInstance().activeQuests = saveData.activeQuests;
            //quest thats tracked
            QuestManager.GetInstance().trackedQuest = saveData.trackedQuest;
            GameEvents.instance.UpdateTrackedQuestTrigger();
            GameEvents.instance.UpdateQuestIndicator();
            //runes in inventory
            GlobalRune.setRuneInventory(saveData.runeInv);
            //runes in deck
            GlobalRune.setDeckList(saveData.deckList);
            //state of the game
            GameStateMachine.GetInstance().SetGameState(saveData.gameState);


        }
    }
}
