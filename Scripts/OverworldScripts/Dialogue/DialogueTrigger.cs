using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    [SerializeField] private GameObject interactableCue;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private bool isOneTimeUse = false;
    [SerializeField] private GameState nextGameState;

    private bool playerInRange;
    void Awake()
    {
        playerInRange = false;
        interactableCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialoguePlaying)
        {
            interactableCue.SetActive(true);
            if (Input.GetKeyDown("e"))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                if (nextGameState != GameState.ANY)
                {
                    GameStateMachine.GetInstance().SetGameState(nextGameState);
                }
                if (isOneTimeUse)
                {
                    interactableCue.SetActive(false);
                    Destroy(this);
                }
            }
        }
        else
        {
            interactableCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerInRange = false;
        }
    }
}
