using UnityEngine;

public class StoryTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private bool isOneTimeUse = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !DialogueManager.GetInstance().dialoguePlaying)
        {
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            if (isOneTimeUse)
            {
                Destroy(this);
            }
        }
    }
}
