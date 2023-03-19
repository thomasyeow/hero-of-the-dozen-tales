using System.Collections.Generic;
using UnityEngine;

public class SetActiveOnGameStateChange : MonoBehaviour
{
    [SerializeField] private List<GameState> activeOnGameStates;
    private void Start()
    {
        GameEvents.instance.onGameStateChanged += UpdateOnChange;
        UpdateOnChange();
    }
    private void UpdateOnChange()
    {
        foreach (GameState gs in activeOnGameStates)
        {
            if (gs == GameState.ANY || gs == GameStateMachine.GetInstance().GetGameState())
            {
                gameObject.SetActive(true);
                break;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnDestroy()
    {
        GameEvents.instance.onGameStateChanged -= UpdateOnChange;
    }
}

