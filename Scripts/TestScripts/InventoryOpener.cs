using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOpener : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject runePanel;

    public GameObject skillsPanel;
    public GameObject openSkillsPanelButton;
    public GameObject closeSkillsPanellButton;
    [SerializeField] private GameObject craftingView;
    private static bool openedIP;
    private bool openedRP = false;
    public GameObject skillProgressPanel;
    public Text skillProgressPanelHeader;
    public Text skillProgressPanelDesc;


    public GameObject EQpanel;

    private void Start()
    {
        //     inventoryPanel.SetActive(false);
        //    runePanel.SetActive(false);
        //     skillsPanel.SetActive(false);
        openedIP = false;
        craftingView.SetActive(false);  
    }

    public void openInventoryPanel()
    {
        if (inventoryPanel != null)
        {
            openedRP = false;
            runePanel.SetActive(false);

            EQpanel.SetActive(false);

            if (inventoryPanel != null)
                inventoryPanel.SetActive(false);
            if (openedIP == true)
            {
                //     Time.timeScale = 1;
                openedIP = false;
                inventoryPanel.SetActive(false);
                if (openedRP)
                    runePanel.SetActive(false);
            }
            else if (openedIP == false)
            {
                //    Time.timeScale = 0;
                openedIP = true;
                inventoryPanel.SetActive(true);
                if (openedRP)
                    runePanel.SetActive(true);
            }
        }
    }

    public void openRunesPanel()
    {
        if (runePanel != null)
        {
            if (openedRP)
            {
                openedRP = false;
                runePanel.SetActive(false);
                openedIP = false;
                inventoryPanel.SetActive(false);
            }
            else
            {
                openedRP = true;
                runePanel.SetActive(true);
                openedIP = false;
                inventoryPanel.SetActive(false);
            }
        }
    }

    public void OpenSkillsPanel()
    {
        skillsPanel.SetActive(true);
        openSkillsPanelButton.SetActive(false);
        //    Time.timeScale = 0;
    }
    public void CloseSkillsPanel()
    {
        openSkillsPanelButton.SetActive(true);
        skillsPanel.SetActive(false);
        //    Time.timeScale = 1;

    }


    public void LoadSkillTree(Button btn)
    {
        string name = btn.name.Split("Image")[0];
        name = name.ToUpper();

        SkillSet chosenEnum = (SkillSet)Enum.Parse(typeof(SkillSet), name, true);



        skillProgressPanelHeader.text = $"Skills of: {name}";

        string levels = "";

        int levelOfskill = SkillSystemManager.Instance.getLvl(chosenEnum);

        for (int i = 0; i <= levelOfskill; i++)
        {
            SkillSystemManager.Instance.unlockSkill(chosenEnum, i);
        }

        System.Collections.Generic.List<bool> list = SkillSystemManager.Instance.getSkillProgress(chosenEnum);
        for (int i = 1; i < list.Count; i++)
        {
            //bool isActivated = list[i];
            levels += $"Skill {i}: {list[i]}\n";
        }

        skillProgressPanelDesc.text = levels;

        skillsPanel.SetActive(false);
        skillProgressPanel.SetActive(true);
    }

    public void GoBackToSkillsPanel()
    {
        skillProgressPanel.SetActive(false);
        skillsPanel.SetActive(true);
    }

    public void openEQPanel()
    {
        //   Time.timeScale = 1;
        if (EQpanel != null)
        {
            openedIP = false;
            inventoryPanel.SetActive(false);
            EQpanel.SetActive(true);
        }
    }

    public void closeEQPanel()
    {
        if (EQpanel != null)
        {
            EQpanel.SetActive(false);
        }
    }

    public void ToggleCraftView()
    {
        craftingView.SetActive(!craftingView.activeSelf);
    }
}
