using UnityEngine;
/*
    This class holds all enemy prefabs and
    all enemy-related functions and info necessary for combat
*/
public class EnemyCollection : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public GameObject vikingPrefab;
    public GameObject colossusPrefab;
    public GameObject banditPrefab;

    //get enemy prefab based on type
    public GameObject getEnemyPrefab(NewBattleSystem.KindOfEnemy enemyType)
    {
        return enemyType switch
        {
            NewBattleSystem.KindOfEnemy.SKELETON => skeletonPrefab,
            NewBattleSystem.KindOfEnemy.VIKING => vikingPrefab,
            NewBattleSystem.KindOfEnemy.RUNECOLOSSUS => colossusPrefab,
            NewBattleSystem.KindOfEnemy.BANDIT => banditPrefab,
            //return skeleton prefab by default
            _ => skeletonPrefab,
        };
    }
}
