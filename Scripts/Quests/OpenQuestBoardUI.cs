using UnityEngine;

public class OpenQuestBoardUI : MonoBehaviour
{
    public GameObject questBoardUI;
    public void Open()
    {
        if (!questBoardUI.activeSelf)
        {
            questBoardUI.SetActive(true);
        } else
        {
            Close();
        }
    }
    public void Close()
    {
        questBoardUI.SetActive(false);
    }
}
