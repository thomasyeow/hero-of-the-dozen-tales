using System.Collections;
using UnityEngine;

public class DisappearAfter1Second : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Countdown1());
    }
    //Delete this game object after 1 second
    private IEnumerator Countdown1()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
