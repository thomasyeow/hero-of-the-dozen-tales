using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    public GameObject PauseView;
    //[SerializeField] private Slider slider;

    private void Awake()
    {
        PauseView.SetActive(false);
    }

    private void Update()
    {
        
            if (Input.GetKeyUp(KeyCode.Escape) && !PauseView.activeSelf)
            {
                TurnOnPauseView();
                Time.timeScale = 0f;
            }
            else if(Input.GetKeyUp(KeyCode.Escape) && PauseView.activeSelf)
            {
                TurnOffPauseView();
                Time.timeScale = 1f;
            }
        
    }


    private void TurnOnPauseView()
    {
        //slider.value = SoundManager.SoundVolume;
        PauseView.SetActive(true);
    }

    private void TurnOffPauseView()
    {
        PauseView.SetActive(false);
    }

    public void SaveGame()
    {
        DataPersistenceManager.instance.SaveGame();
        Debug.Log("UI Saved Game");
    }

    public void LoadGame()
    {
        DataPersistenceManager.instance.LoadGame();
        Debug.Log("UI Load Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeVolume()
    {
        //SoundManager.Instance.SetVolume(slider.value);
    }
}
