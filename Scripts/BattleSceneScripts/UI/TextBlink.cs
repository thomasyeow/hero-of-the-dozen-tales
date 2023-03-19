using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    public Text text;

    public void Start()
    {
        text.enabled = false;
    }
    public void Blink(string msg, int time)
    {
        StartCoroutine(ShowMessageCoroutine(msg, time));
    }
    IEnumerator ShowMessageCoroutine(string msg, int time)
    {
        text.enabled = true;
        text.text = msg;
        yield return new WaitForSecondsRealtime(time);
        text.enabled = false;
    }
}
