using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 vec;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject blackScreen;
    // Start is called before the first frame update
    void Start()
    {
        tp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void tp()// 346.7093 2.766226  176.7136
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = vec;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
