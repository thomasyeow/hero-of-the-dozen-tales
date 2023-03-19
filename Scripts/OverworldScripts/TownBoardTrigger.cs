using UnityEngine;

public class TownBoardTrigger : MonoBehaviour
{

    private bool playerInRange;
    public bool questBoardOpened;
    [SerializeField] private GameObject interactableCue;
    public OpenQuestBoardUI openQuestBoard;
    void Awake()
    {
        playerInRange = false;
    }
    void Update()
    {
        if (playerInRange)
        {
            interactableCue.SetActive(true);
            if (Input.GetKeyDown("e"))
            {
                openQuestBoard.Open();
                questBoardOpened = true;
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
