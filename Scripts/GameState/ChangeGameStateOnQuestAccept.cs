using UnityEngine;

public class ChangeGameStateOnQuestAccept : MonoBehaviour
{
    [SerializeField] private string questTitle;
    [SerializeField] private GameState activeOnGameState;
    [SerializeField] private GameState nextGameState;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activeOnGameState == GameStateMachine.GetInstance().GetGameState())
        {

            foreach (Quest q in QuestManager.GetInstance().activeQuests)
            {
                if (q.title == questTitle)
                {
                    GameStateMachine.GetInstance().SetGameState(nextGameState);
                    break;
                }
            }
        }
    }
}
