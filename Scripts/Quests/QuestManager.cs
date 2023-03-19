using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    [SerializeField] public List<Quest> allQuests;
    [SerializeField] public List<Quest> activeQuests;
    [SerializeField] public Quest trackedQuest;
    [SerializeField] public bool resetActiveQuests = false;
    //public Quest trackedQuest;

    private static QuestManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Do not destroy this script's gameObject between scenes
            DontDestroyOnLoad(gameObject);
            //this is the only instance
            instance = this;
        }
    }

    public static QuestManager GetInstance()
    {
        return instance;
    }

    public void getQuest(string s)
    {
        foreach (Quest q in allQuests)
        {
            if (q.title == s)
            {

                activeQuests.Add(q);
                activeQuests.Last().isActive = true;
                break;
            }
        }
        GameEvents.instance.UpdateQuestIndicator();
    }
    public void completeQuest(string s)
    {
        foreach (Quest q in activeQuests)
        {
            if (q.title == s)
            {
                if (q.goal.isReached())
                {
                    GameObject player = GameObject.Find("hero");
                    player.GetComponent<Character_Controller>().AddMoney(q.goldReward);
                    foreach (GlobalRune.Type type in q.runeRewards)
                    {
                        GlobalRune.addRune(type, 1);
                    }
                    q.goal.currentAmount = 0;
                    activeQuests.Remove(q);
                    Quest tempQ = q;
                    tempQ.isActive = false;
                    if (!q.autoComplete)
                    {
                        q.returned = true;
                    }
                }
                break;
            }
        }
        GameEvents.instance.UpdateQuestIndicator();
        GameEvents.instance.UpdateTrackedQuestTrigger();

    }
    private void OnApplicationQuit()
    {
        if (resetActiveQuests)
        {
            activeQuests = new List<Quest>();
            trackedQuest = null;
        }
    }
}
