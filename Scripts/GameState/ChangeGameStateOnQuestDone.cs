using UnityEngine;

public class ChangeGameStateOnQuestDone : MonoBehaviour
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

            foreach (Quest q in QuestManager.GetInstance().allQuests)
            {
                if (q.title == questTitle && q.returned)
                {
                    GameStateMachine.GetInstance().SetGameState(nextGameState);
                    break;
                }
            }
        }
    }
}
