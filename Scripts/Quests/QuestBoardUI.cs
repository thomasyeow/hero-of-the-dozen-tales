using TMPro;
using UnityEngine;

public class QuestBoardUI : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject questAcceptUI;
    [SerializeField] private Quest[] quests;

    private void Awake()
    {
        foreach (GameObject go in buttons)
        {
            go.SetActive(false);
        }
        UpdateUI();
    }
    public void OpenQuestWindow(int index)
    {
        questAcceptUI.GetComponent<QuestAcceptUI>().getQuest(quests[index].title);
        UpdateUI();
    }
    public void UpdateUI()
    {
        for (int i = 0; i < quests.Length && i < buttons.Length; i++)
        {
            if (quests[i] != null && quests[i].title != "")
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = quests[i].title;
                buttons[i].SetActive(true);
            }
            else
            {
                buttons[i].SetActive(false);
            }
            foreach (Quest q in QuestManager.GetInstance().activeQuests)
            {
                if (q.title == quests[i].title)
                {
                    buttons[i].SetActive(false);
                }
            }
        }
    }
}
