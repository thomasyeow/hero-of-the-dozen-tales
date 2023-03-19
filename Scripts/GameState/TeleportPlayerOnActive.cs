using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeleportPlayerOnActive : MonoBehaviour
{
    [SerializeField] private Vector3 vec;
    [SerializeField] private GameState activeGameState;
    [SerializeField] private GameState nextGameState;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject blackScreen;

    private void OnEnable()
    {
        try
        {
            if (activeGameState == GameStateMachine.GetInstance().GetGameState())
            {
                GameEffects.instance.PopUpBlackScreen();
                //StartCoroutine(PopUpBlackScreen());
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = vec;
                player.GetComponent<CharacterController>().enabled = true;
                if (nextGameState != GameState.ANY)
                {
                    GameStateMachine.GetInstance().SetGameState(nextGameState);
                }
            }
        }
        catch{ }
    }
    public IEnumerator PopUpBlackScreen()
    {
        Color color = blackScreen.GetComponent<Image>().color;
        color.a = 1f;
        blackScreen.GetComponent<Image>().color = color;
        blackScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        for (float i = 1; i > 0; i -= 0.05f)
        {
            yield return new WaitForSecondsRealtime(0.05f);
            Debug.Log("processing");
            color.a = i;
            blackScreen.GetComponent<Image>().color = color;
        }
        blackScreen.SetActive(false);
        Debug.Log("done");
    }

    private void OnApplicationQuit()
    {
        blackScreen.SetActive(false);
        StopAllCoroutines();
    }
}
