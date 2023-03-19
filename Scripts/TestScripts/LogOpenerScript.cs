using UnityEngine;

public class LogOpenerScript : MonoBehaviour
{
    public GameObject LogPanel;
    private bool openedLog = false;

    public void openLogPanel()
    {
        if (LogPanel != null)
        {
            if (openedLog)
            {
                openedLog = false;
                LogPanel.SetActive(false);
            }
            else
            {
                openedLog = true;
                LogPanel.SetActive(true);
            }
        }
    }

}
