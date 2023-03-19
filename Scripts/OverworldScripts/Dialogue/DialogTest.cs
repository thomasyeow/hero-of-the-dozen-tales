using UnityEngine;

public class DialogTest : MonoBehaviour
{
    [SerializeField] private GameObject ShopPanel;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void HideShop()
    {
        gameObject.SetActive(false);
    }

}
