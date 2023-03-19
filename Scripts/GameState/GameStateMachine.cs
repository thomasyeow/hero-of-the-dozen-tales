using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    private static GameStateMachine instance;
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
            //this is the only instance
            instance = this;
        }
    }
    public static GameStateMachine GetInstance()
    {
        return instance;
    }

    public void SetGameState(GameState gs)
    {
        gameState = gs;
        GameEvents.instance.GameStateChangedTrigger();
    }
    public GameState GetGameState()
    {
        return gameState;
    }

    private void Start()
    {
        gameState = GameState.START;
    }
}

public enum GameState
{
    ANY,
    START,
    FIRST_RUNES,
    FIRST_QUEST_ACCEPTED,
    FIRST_QUEST_DONE,
    PLAYER_IN_VILLAGE,
    TALK_TO_CHIEF
}
