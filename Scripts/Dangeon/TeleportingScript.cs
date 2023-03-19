using System.Collections;
using UnityEngine;
using static DangeonEntrance;

public class TeleportingScript : MonoBehaviour
{
    public GameObject overworldEntrance;
    [SerializeField] private GameObject dangeon;
    [SerializeField] private GameObject dangeonEntrance;

    public GameObject DangeonEntrance => dangeonEntrance;

    private void OnEnable()
    {
        SetDangeonEntrance += SetEntrance;
    }

    public void SetEntrance(GameObject go)
    {
        overworldEntrance = go;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.gameObject.tag == "Player" && other.gameObject != null)
        {
            StartCoroutine(Tp(other));
        }
    }

    private IEnumerator Tp(Collider other)
    {
        yield return new WaitForSeconds(0.2f);
        overworldEntrance = GameObject.FindGameObjectWithTag("ExitD");
        if (overworldEntrance != null)
        {
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.gameObject.GetComponent<CharacterController>().transform.position = overworldEntrance.transform.Find("Exit") == null ? overworldEntrance.transform.position : overworldEntrance.transform.Find("Exit").transform.transform.position;
            other.gameObject.GetComponent<CharacterController>().enabled = true;
        }

        Destroy(dangeon);
    }
}
