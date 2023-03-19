using UnityEngine;

public class simpleCamFollowScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Update()
    {
        transform.position = target.position + offset;
    }
}
