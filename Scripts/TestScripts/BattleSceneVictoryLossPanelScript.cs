using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneVictoryLossPanelScript : MonoBehaviour
{
    //victory screen panel
    public GameObject victoryPanel;
    public static int VictoryOrLoss;        //0 = nothing, 1 = win, 2 = loss

    //losing screen panel
    public GameObject lossPanel;
    void Start()
    {
        VictoryOrLoss = 0;
    }


    void Update()
    {
        victoryScreen();
        lossScreen();
    }

    private void victoryScreen()
    {
        if (VictoryOrLoss == 1)
            if (victoryPanel != null && victoryPanel.activeSelf != true)
                victoryPanel.SetActive(true);
    }
    private void lossScreen()
    {
        if (VictoryOrLoss == 2)
            if (lossPanel != null && lossPanel.activeSelf != true)
                lossPanel.SetActive(true);
    }


    public void VictoryExitButton()
    {
        SceneManager.LoadScene("3D Hero test");
        //load enemies
        SaveDataScript.LoadEnemies();
    }
    public void LossExitButton()
    {
        SceneManager.LoadScene("StartingScreen");
    }
}
