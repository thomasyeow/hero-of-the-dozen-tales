using UnityEngine;

public class EnemyDescription : MonoBehaviour
{
    [SerializeField] private NewBattleSystem.KindOfEnemy kind;
    public int ID = 0;

    private void Start()
    {
        //destroy gameobject at start if its ID is in OverWorldManager list of defeated enemies
        if (OverWorldManager.instance.destroyedEnemyIDList.Contains(this.ID))
        {
            gameObject.SetActive(false);
        }
    }
    public NewBattleSystem.KindOfEnemy GetKindOfEnemy()
    {
        return kind;
    }
}
