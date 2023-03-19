using UnityEngine;

public class AddARandomRune : MonoBehaviour
{
    [SerializeField]
    private RunesInventory runesAmtSO;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //random rune from 0 to 9
            System.Random random = new System.Random();
            int type = random.Next(10);
            GlobalRune.addRune((GlobalRune.Type)type, 1);

            //Old code
            //Debug.Log("Added rune number: " + num);
            //runesAmtSO.runesAmt[num] += 1;
            //UnityEditor.EditorUtility.SetDirty(runesAmtSO);

            Destroy(gameObject);
        }
    }
}
