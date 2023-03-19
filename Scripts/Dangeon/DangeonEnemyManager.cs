using UnityEngine;

public class DangeonEnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    private void OnEnable()
    {
        FillSlots(enemyPrefab);
    }

    void FillSlots(GameObject prefab)
    {
        foreach (Transform t in transform)
        {
            if (Random.Range(0f, 1f) > 0.5f)
            {
                var go = Instantiate(prefab, new Vector3(t.position.x, t.position.y, t.position.z), t.rotation);
                go.transform.SetParent(t);

            }
        }

    }
}
