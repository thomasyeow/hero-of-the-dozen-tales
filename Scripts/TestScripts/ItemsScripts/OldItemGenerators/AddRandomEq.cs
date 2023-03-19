using UnityEngine;

public class AddRandomEq : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //        Equipment eq2 = ItemGenerator.GenerateEquipment();
            //      eq2 = ScriptableObject.CreateInstance<Equipment>();
            //      string path = "Assets/SO Values/EquipmentSO/eq2";
            //      AssetDatabase.CreateAsset(eq2, path);
            //     Debug.Log("Created:       " + eq2.name);

            Destroy(gameObject);
        }
    }
}
