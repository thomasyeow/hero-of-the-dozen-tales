using UnityEngine;

public class SetQuestLocation : MonoBehaviour
{
    public string questTitle;
    private void OnTriggerEnter(Collider other)
    {
        foreach (Quest q in QuestManager.GetInstance().activeQuests)
        {
            if (q.title == questTitle)
            {
                q.goal.playerInLocation = true;
                break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (Quest q in QuestManager.GetInstance().activeQuests)
        {
            if (q.title == questTitle)
            {
                q.goal.playerInLocation = false;
                break;
            }
        }
    }
}
