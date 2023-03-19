using TMPro;
using UnityEngine;

public class QuestAcceptUI : MonoBehaviour
{
    public Quest quest;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI task;
    public TextMeshProUGUI reward;
    public GameObject UIWindow;

    public void AcceptQuest()
    {
        if (quest.questVar != "")
        {
            GameObject.Find("SetDialogueGlobals").GetComponent<SetGlobals>().ChangeVariable(quest.questVar, "inProgress");
            DialogueManager.GetInstance().questPopped = false;
        }
        QuestManager.GetInstance().getQuest(quest.title);
        UIWindow.SetActive(false);
        GameEvents.instance.UpdateTrackedQuestTrigger();
    }
    public void CancelQuest()
    {
        if (quest.questVar != "")
        {
            Debug.Log("cancel");
            GameObject.Find("SetDialogueGlobals").GetComponent<SetGlobals>().ChangeVariable(quest.questVar, "questCanceled");
            DialogueManager.GetInstance().questPopped = false;
        }
        UIWindow.SetActive(false);
    }
    public void getQuest(string s)
    {
        //Debug.Log("getQuestUI " + s);
        foreach (Quest q in QuestManager.GetInstance().allQuests)
        {
            if (q.title == s)
            {
                quest = q;
                break;
            }
        }
        setQuestDescription();
    }
    public void setQuestDescription()
    {
        UIWindow.SetActive(true);
        if (quest.questVar != "")
        {
            DialogueManager.GetInstance().questPopped = true;
        }
        title.text = quest.title;
        description.text = quest.description;
        task.text = quest.task;
        reward.text = "Reward: " + quest.goldReward.ToString() + " gold.";

    }
}
