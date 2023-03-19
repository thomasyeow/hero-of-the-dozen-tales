using System.Collections;
using TMPro;
using UnityEngine;

public class LootPopUpManager : MonoBehaviour
{
    public int duration;
    public GameObject popUpPrefab;
    public GameObject popUpParent;

    private bool canPopUp;

    public static LootPopUpManager instance;
    private void Awake()
    {
        instance = this;
        canPopUp = true;
    }

    public void PopUpLoot(string name, float amount)
    {
        GameObject popUp = Instantiate(popUpPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        popUp.transform.SetParent(popUpParent.transform);
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = name + " x " + amount;
        StartCoroutine(PopUpEffects(popUp));
    }

    public IEnumerator PopUpEffects(GameObject popUp)
    {
        popUp.transform.localScale = new Vector3(1, 0, 1);
        do
        {
            yield return new WaitForSeconds(0.01f);
        } while (!canPopUp);

        float tempY = 0;
        while (popUp.transform.localScale.y < 1)
        {
            canPopUp = false;
            if (popUp.transform.localScale.y != 1)
            {
                tempY += 0.1f;
                popUp.transform.localScale = new Vector3(1, tempY, 1);
                yield return new WaitForSeconds(0.01f);
            }
        }
        canPopUp = true;
        Destroy(popUp, duration);
    }
}
