using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEffects : MonoBehaviour
{
    public static GameEffects instance;

    public GameObject blackScreen;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            //this is the only instance of GameEffects
            instance = this;
        }
    }
    public void PopUpBlackScreen()
    {
        StartCoroutine(FadeEffect(blackScreen));
    }

    private IEnumerator FadeEffect(GameObject go)
    {
        Color color = go.GetComponent<Image>().color;
        color.a = 1f;
        go.GetComponent<Image>().color = color;
        go.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        for (float i = 1; i > 0; i -= 0.05f)
        {
            yield return new WaitForSecondsRealtime(0.05f);
            Debug.Log("processing");
            color.a = i;
            go.GetComponent<Image>().color = color;
        }
        go.SetActive(false);
        Debug.Log("done");
    }
}
