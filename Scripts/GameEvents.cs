using System;
using System.Collections;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Do not destroy this script's gameObject between scenes
            DontDestroyOnLoad(gameObject);
            //this is the only instance of GlobalRuneInventory
            instance = this;
        }
    }
    public event Action onUpdateTrackedQuestTrigger;
    public void UpdateTrackedQuestTrigger()
    {
        if (onUpdateTrackedQuestTrigger != null)
        {
            onUpdateTrackedQuestTrigger();
        }
    }
    public event Action onGameStateChanged;
    public void GameStateChangedTrigger()
    {
        if (onGameStateChanged != null)
        {
            onGameStateChanged();
        }
    }

    public event Action onUpdateQuestIndicator;

    public void UpdateQuestIndicator()
    {
        if (onUpdateQuestIndicator != null)
        {
            onUpdateQuestIndicator();
        }
    }
}
