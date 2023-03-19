using UnityEngine;

public class balanceFx : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("b4 position: " + transform.position);
        transform.position = new Vector3(0, 0);
        Debug.Log("new position: " + transform.position);
    }

}
