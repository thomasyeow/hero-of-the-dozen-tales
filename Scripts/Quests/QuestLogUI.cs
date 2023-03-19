using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestLogUI : MonoBehaviour
{
    public GameObject questLogWindow;
    public List<GameObject> activeQuestsButtons;
    public GameObject descriptionWindow;
    public GameObject trackedQuestWindow;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI task;
    public TextMeshProUGUI reward;
    public TextMeshProUGUI trackedQuestTitle;
    public TextMeshProUGUI trackedQuestProgress;
    public GameObject completeButton;
    public GameObject trackButton;
    private int questIndex = 0;
    private Quest trackedQuest;

    private void Start()
    {
        UpdateTrackedQuest();
        //trackedQuest = QuestManager.GetInstance().trackedQuest;
        /*if (QuestManager.GetInstance().activeQuests.Contains(trackedQuest))
        {
            questIndex = QuestManager.GetInstance().activeQuests.IndexOf(trackedQuest);
            trackQuest();
        }
        else
        {
            defaultQuestTracking();
        }*/
        GameEvents.instance.onUpdateTrackedQuestTrigger += UpdateTrackedQuest;
    }

    public void OpenQuestLog()
    {
        if (!questLogWindow.activeSelf)
        {
            questLogWindow.SetActive(true);
            descriptionWindow.SetActive(false);
            for (int i = 0; i < activeQuestsButtons.Count; i++)
            {
                activeQuestsButtons[i].SetActive(false);
            }
            for (int i = 0; i < QuestManager.GetInstance().activeQuests.Count && i < activeQuestsButtons.Count; i++)
            {
                activeQuestsButtons[i].SetActive(true);
                activeQuestsButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = QuestManager.GetInstance().activeQuests[i].title;
            }
        } else
        {
            hideQuestLog();
        }
    }
    public void checkQuestDescription(int index)
    {
        descriptionWindow.SetActive(true);
        title.text = QuestManager.GetInstance().activeQuests[index].title;
        description.text = QuestManager.GetInstance().activeQuests[index].description;

        if (QuestManager.GetInstance().activeQuests[index].goal.goalType == GoalType.Kill)
        {
            task.text = "Progress: " + QuestManager.GetInstance().activeQuests[index].goal.currentAmount + "/" + QuestManager.GetInstance().activeQuests[index].goal.requiredAmount + " Killed.";
        }

        if (QuestManager.GetInstance().activeQuests[index].goal.goalType == GoalType.Gathering)
        {
            task.text = "Progress: " + QuestManager.GetInstance().activeQuests[index].goal.currentAmount + "/" + QuestManager.GetInstance().activeQuests[index].goal.requiredAmount + " Gathered.";
        }
        //if goal is reached, show text "Completed"
        if (QuestManager.GetInstance().activeQuests[index].goal.isReached())
        {
            task.text = "Completed";
            task.color = Color.green;
        }
        else
        {
            task.color = Color.black;
        }
        reward.text = "Reward: " + QuestManager.GetInstance().activeQuests[index].goldReward.ToString() + " gold.";
        if (QuestManager.GetInstance().activeQuests[index].runeRewards.Count > 0)
        {
            reward.text += "\nRunes: ";
            foreach (GlobalRune.Type t in QuestManager.GetInstance().activeQuests[index].runeRewards)
            {
                reward.text += t + ", ";
            }
        }

        if (QuestManager.GetInstance().activeQuests[index].autoComplete && QuestManager.GetInstance().activeQuests[index].goal.isReached())
        {
            completeButton.SetActive(true);
            trackButton.SetActive(false);
        }
        else
        {
            completeButton.SetActive(false);
            trackButton.SetActive(true);
        }
        questIndex = index;
    }
    public void completeQuest()
    {
        QuestManager.GetInstance().completeQuest(QuestManager.GetInstance().activeQuests[questIndex].title);
        OpenQuestLog();
    }
    public void hideQuestLog()
    {
        questLogWindow.SetActive(false);
    }

    public void trackQuest()
    {
        trackedQuestWindow.SetActive(true);
        trackedQuest = QuestManager.GetInstance().activeQuests[questIndex];
        QuestManager.GetInstance().trackedQuest = trackedQuest;
        trackedQuestTitle.text = QuestManager.GetInstance().activeQuests[questIndex].title;
        trackedQuestProgress.text = QuestManager.GetInstance().activeQuests[questIndex].title;
        if (QuestManager.GetInstance().activeQuests[questIndex].goal.goalType == GoalType.Kill)
        {
            trackedQuestProgress.text = "Progress: " + QuestManager.GetInstance().activeQuests[questIndex].goal.currentAmount + "/" + QuestManager.GetInstance().activeQuests[questIndex].goal.requiredAmount + " Killed.";
        }

        if (QuestManager.GetInstance().activeQuests[questIndex].goal.goalType == GoalType.Gathering)
        {
            trackedQuestProgress.text = "Progress: " + QuestManager.GetInstance().activeQuests[questIndex].goal.currentAmount + "/" + QuestManager.GetInstance().activeQuests[questIndex].goal.requiredAmount + " Gathered.";
        }

        //if goal is reached, show text "Completed"
        if (QuestManager.GetInstance().activeQuests[questIndex].goal.isReached())
        {
            trackedQuestProgress.text = "Completed";
            trackedQuestProgress.color = Color.green;
            if (trackedQuest.turnInLocation.radius > 0)
            {
                QuestArrowDirection.GetInstance().SetQuestLocation(trackedQuest.turnInLocation);
            }
            else
            {
                QuestArrowDirection.GetInstance().deactivateArea();
            }
        }
        else
        {
            if (trackedQuest.turnInLocation.radius > 0)
            {
                QuestArrowDirection.GetInstance().SetQuestLocation(trackedQuest.goalLocation);
            }
            else
            {
                QuestArrowDirection.GetInstance().deactivateArea();
            }
            trackedQuestProgress.color = Color.black;
        }
    }
    public void defaultQuestTracking()
    {
        if (QuestManager.GetInstance().activeQuests.Count > 0)
        {
            trackedQuestWindow.SetActive(true);
            trackedQuest = QuestManager.GetInstance().activeQuests[0];
            QuestManager.GetInstance().trackedQuest = trackedQuest;
            trackedQuestTitle.text = QuestManager.GetInstance().activeQuests[0].title;
            trackedQuestProgress.text = QuestManager.GetInstance().activeQuests[0].title;
            if (QuestManager.GetInstance().activeQuests[0].goal.goalType == GoalType.Kill)
            {
                trackedQuestProgress.text = "Progress: " + QuestManager.GetInstance().activeQuests[0].goal.currentAmount + "/" + QuestManager.GetInstance().activeQuests[0].goal.requiredAmount + " Killed.";
            }

            if (QuestManager.GetInstance().activeQuests[0].goal.goalType == GoalType.Gathering)
            {
                trackedQuestProgress.text = "Progress: " + QuestManager.GetInstance().activeQuests[0].goal.currentAmount + "/" + QuestManager.GetInstance().activeQuests[0].goal.requiredAmount + " Gathered.";
            }
            //if goal is reached, show text "Completed"
            if (QuestManager.GetInstance().activeQuests[0].goal.isReached())
            {
                trackedQuestProgress.text = "Completed";
                trackedQuestProgress.color = Color.green;
                QuestArrowDirection.GetInstance().SetQuestLocation(trackedQuest.turnInLocation);
            }
            else
            {
                trackedQuestProgress.color = Color.black;
                QuestArrowDirection.GetInstance().SetQuestLocation(trackedQuest.goalLocation);
            }
        }
        else
        {
            trackedQuestWindow.SetActive(false);
            QuestArrowDirection.GetInstance().deactivateArea();
        }
    }
    public void UpdateTrackedQuest()
    {
        if (QuestManager.GetInstance().trackedQuest != null)
        {
            trackedQuest = QuestManager.GetInstance().trackedQuest;
            if (QuestManager.GetInstance().activeQuests.Contains(trackedQuest))
            {
                questIndex = QuestManager.GetInstance().activeQuests.IndexOf(trackedQuest);
                trackQuest();
            }
            else
            {
                questIndex = QuestManager.GetInstance().activeQuests.IndexOf(trackedQuest);
                defaultQuestTracking();
            }
        }
        else
        {
            defaultQuestTracking();
        }
    }
    private void OnDestroy()
    {
        GameEvents.instance.onUpdateTrackedQuestTrigger -= UpdateTrackedQuest;
    }
}
