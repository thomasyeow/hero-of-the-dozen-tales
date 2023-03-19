using UnityEngine;

public class OpenMap : MonoBehaviour
{
    public GameObject map;

    private void Start()
    {
        map.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m") && !map.activeSelf)
        {
            map.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown("m") && map.activeSelf)
        {
            map.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
