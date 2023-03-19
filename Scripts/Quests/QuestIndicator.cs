using UnityEngine;

public class QuestIndicator : MonoBehaviour
{
    [SerializeField] private string questTitle;
    [SerializeField] private ActiveQuests activeQuestsSO;
    [SerializeField] private GameObject questAvailable;
    [SerializeField] private GameObject questInProgress;
    [SerializeField] private GameObject questDone;
    [SerializeField] private GameObject minimapQuestAvailable;
    [SerializeField] private GameObject minimapQuestInProgress;
    [SerializeField] private GameObject minimapQuestDone;
    private void Start()
    {
        UpdateQuestIndicator();
        GameEvents.instance.onUpdateQuestIndicator += UpdateQuestIndicator;
    }

    public void UpdateQuestIndicator()
    {
        //if quest was returned, dont show any indicator
        foreach (Quest q in QuestManager.GetInstance().allQuests)
        {
            if (q.title.Equals(questTitle))
            {
                if (q.returned)
                {
                    questAvailable.SetActive(false);
                    questDone.SetActive(false);
                    questInProgress.SetActive(false);

                    minimapQuestAvailable.SetActive(false);
                    minimapQuestDone.SetActive(false);
                    minimapQuestInProgress.SetActive(false);
                    return;
                }
                else if (q.isActive)
                {
                    if (q.goal.isReached())//if quest is active and quest goal is reached, show quest done indicator
                    {
                        questDone.SetActive(true);

                        questAvailable.SetActive(false);
                        questInProgress.SetActive(false);

                        minimapQuestDone.SetActive(true);

                        minimapQuestAvailable.SetActive(false);
                        minimapQuestInProgress.SetActive(false);
                        return;
                    }
                    else
                    {
                        questInProgress.SetActive(true);

                        questDone.SetActive(false);
                        questAvailable.SetActive(false);

                        minimapQuestInProgress.SetActive(true);

                        minimapQuestDone.SetActive(false);
                        minimapQuestAvailable.SetActive(false);
                        return;
                    }
                }
                else // if quest isnt returned and isnt active, show quest available indicator
                {
                    questAvailable.SetActive(true);

                    questDone.SetActive(false);
                    questInProgress.SetActive(false);

                    minimapQuestAvailable.SetActive(true);

                    minimapQuestDone.SetActive(false);
                    minimapQuestInProgress.SetActive(false);
                    return;
                }
            }
        }
    }
    private void OnDestroy()
    {
        GameEvents.instance.onUpdateQuestIndicator -= UpdateQuestIndicator;
    }
}
