using System.Collections;
using UnityEngine;

public class DangeonEntrance : MonoBehaviour
{
    public delegate void DangeonEntranceEvent(GameObject gameObject);
    public static DangeonEntranceEvent SetDangeonEntrance;

    [SerializeField] private GameObject dangeonPlace;
    [SerializeField] private GameObject dangeonPrefab;


    private void OnEnable()
    {
        dangeonPlace = GameObject.FindGameObjectWithTag("DangeonPlace");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (dangeonPlace.transform.childCount == 0)
            {
                StartCoroutine(Tp(other));
            }
            else
            {
                foreach (Transform t in dangeonPlace.transform)
                {
                    Destroy(t.gameObject);
                }
                StartCoroutine(Tp(other));  
            }
        }
    }

    private IEnumerator Tp(Collider other)
    {
        yield return new WaitForSeconds(0.2f);
        var go = Instantiate(dangeonPrefab, dangeonPlace.transform.position, Quaternion.identity);
        go.transform.parent = dangeonPlace.transform;
        SetDangeonEntrance.Invoke(gameObject);
        if (go.transform.Find("Entrance") != null)
        {
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.gameObject.GetComponent<CharacterController>().transform.position = go.transform.Find("Entrance").transform.position;
            other.gameObject.GetComponent<CharacterController>().enabled = true;
        }else
        {
            SetDangeonEntrance.Invoke(GameObject.FindGameObjectWithTag("ExitD"));
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.gameObject.GetComponent<CharacterController>().transform.position = go.transform.Find("Entrance").transform.position;
            other.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
