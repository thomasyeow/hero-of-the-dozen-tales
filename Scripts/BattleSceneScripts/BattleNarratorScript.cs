using UnityEngine;
using UnityEngine.UI;

public class BattleNarratorScript : MonoBehaviour
{
    public static string narrator;
    private Text _narrator;
    // Start is called before the first frame update
    void Start()
    {
        narrator = "Battle Start!";
        _narrator = GameObject.Find("Announcement Text").GetComponent<Text>();
        _narrator.text = "Battle Start!";
    }

    private void Update()
    {
        _narrator.text = narrator;
    }

    public static void setNarrator(string message)
    {
        narrator = message;
    }
}
