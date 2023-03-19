using UnityEngine;

public class ResetGlobalMoney : MonoBehaviour
{
    public static ResetGlobalMoney instance;
    public Money money;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            //Do not destroy this script's gameObject between scenes
            DontDestroyOnLoad(gameObject);
            //this is the only instance of GlobalRuneInventory
            instance = this;
        }
    }
    private void OnApplicationQuit()
    {
        money.money = 0;
    }
}
