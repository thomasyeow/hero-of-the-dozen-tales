using UnityEngine;

public class ShowRuneIfInInventory : MonoBehaviour
{
    public int runeNum;

    [SerializeField]
    private RunesInventory runesAmtSO;
    private Vector3 nomovepos;

    void Start()
    {
        nomovepos = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        Vector3 newmovepos = new Vector3(0, 0, 0);
        gameObject.transform.localScale = newmovepos;
    }

    void Update()
    {
        CheckRuneAmt();
    }

    private void CheckRuneAmt()
    {
        if (runesAmtSO.runesAmt[runeNum] > 0)
        {
            gameObject.transform.localScale = nomovepos;
        }
    }
}
