using UnityEngine;
using UnityEngine.UI;

public class StoryPopUp : MonoBehaviour
{

    public GameObject storyWindow;
    public Text text;
    public string message;
    private bool used = false;
    // Start is called before the first frame update

    private void Awake()
    {
        storyWindow.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            PopUp();
        }
    }
    private void Update()
    {
        if (Time.timeScale == 0 && Input.GetMouseButtonDown(0))
        {
            UnpauseGame();
        }
    }
    void PopUp()
    {
        Time.timeScale = 0;
        storyWindow.SetActive(true);
        text.text = message;
        used = true;
    }

    public void UnpauseGame()//unpause game after LMB click
    {
        if (used)
        {
            Time.timeScale = 1;
            storyWindow.SetActive(false);
            Destroy(gameObject);
        }
    }
}
