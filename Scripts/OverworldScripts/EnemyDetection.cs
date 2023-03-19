using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    //list of enemy detection colliders
    List<Collider> hitColliders;
    //list of enemies in collider range
    [SerializeField] private List<GameObject> enemiesGO = new List<GameObject>();
    //radius of collider sphere
    public float sphereRadius;
    //refers to enemy layer mask
    int layer_mask;

    void Start()
    {
        // nazwy warstw na ktorych leza rzeczy do wykrycia
        layer_mask = LayerMask.GetMask("Enemy");
    }

    /*private void Update()
    {
        //array of all colliders overlapping with sphere of radius "sphereRadius" centered on player
        hitColliders = Physics.OverlapSphere(transform.position, sphereRadius, layer_mask, QueryTriggerInteraction.Ignore).ToList();

        for (int i = 0; i < hitColliders.Count; i++)
        {
            Collider hitCollider = hitColliders[i];
            //Debug.Log($"{hitCollider.gameObject.name} in : {Vector3.Distance(hitCollider.transform.position, transform.position)}.");
            //Debug.Log("Collider added!");

            if (Vector3.Distance(hitCollider.transform.position, transform.position) < 2.75f)
            {

                //get gameobjects from colliders
                enemiesGO = swapToGameObject(hitColliders);
                //Debug.Log("collided with: " + enemiesGO[0].name);
                //remDuplicates(enemiesGO);
                enemiesGO = enemiesGO.Distinct().ToList();
                //if gameObjects are further than 8f from player, remove them from enemiesGO
                checkEnemiesInRange(enemiesGO);

                //make list of enemies that are outside of the range (opposite of enemiesGO), then save it to SaveDataScript
                List<GameObject> enemiesOut = new List<GameObject>();
                enemiesOut = swapToGameObject(hitColliders);
                enemiesOut = enemiesOut.Distinct().ToList();
                checkEnemiesOutOfRange(enemiesOut);
                SaveDataScript.storeSaveData(enemiesOut);
                //Debug.Log("saving enemies: " + enemiesOut.Count);

                OverWorldManager.instance.loadBattleScene(enemiesGO);

                break;
            }
            //enemiesGO = swapToGameObject(hitColliders);
            //enemiesGO = enemiesGO.Distinct().ToList();
        }
    }*/

    //on player collision with rigidbodies
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //if collision with "Enemy" tagged gameObject
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
            //array of all colliders overlapping with sphere of radius "sphereRadius" centered on player
            hitColliders = Physics.OverlapSphere(transform.position, sphereRadius, layer_mask, QueryTriggerInteraction.Ignore).ToList();
            foreach (Collider hitCollider in hitColliders)
            {
                //gameObjects of all nearby enemies
                enemiesGO = swapToGameObject(hitColliders);
                //remove possible duplicate gameobjects resulting from multiple colliders from enemiesGO
                enemiesGO = enemiesGO.Distinct().ToList();
                
                //TODO make list of enemies that are outside of the range (opposite of enemiesGO), then save it to SaveDataScript
                /*List<GameObject> enemiesOut = new List<GameObject>();
                enemiesOut = swapToGameObject(hitColliders);
                enemiesOut = enemiesOut.Distinct().ToList();
                checkEnemiesOutOfRange(enemiesOut);
                SaveDataScript.storeSaveData(enemiesOut);*/
                //load battle scene

            }
            Debug.Log("enemiesGO size: " + enemiesGO.Count);
            OverWorldManager.instance.loadBattleScene(enemiesGO);
            
        }
    }

    //returns list of gameObjects from colliders
    private List<GameObject> swapToGameObject(List<Collider> list)
    {
        List<GameObject> newList = new List<GameObject>();
        foreach (Collider col in list)
        {
            newList.Add(col.gameObject);
            //Debug.Log($"{col.gameObject.name}");
        }
        return newList;
    }


    //if collisions are further than 8f from player, remove them from list
    //Probably obsolete, because sphereRadius can do this already
    /*private void checkEnemiesInRange(List<GameObject> list)
    {
        List<GameObject> templist = new List<GameObject>();
        for (int i = 0; i < list.Count; i++)
        {
            GameObject col = list[i];

            if (Vector3.Distance(col.transform.position, transform.position) > 8f)
            {
                templist.Add(col);
            }
        }
        foreach (GameObject go in templist)
            list.Remove(go);
    }*/


    //if object is 8f or less in distance, remove them from the list

    private void checkEnemiesOutOfRange(List<GameObject> list)
    {
        List<GameObject> templist = new List<GameObject>();
        for (int i = 0; i < list.Count; i++)
        {
            GameObject col = list[i];

            if (Vector3.Distance(col.transform.position, transform.position) <= 8f)
            {
                templist.Add(col);
            }
        }
        foreach (GameObject go in templist)
            list.Remove(go);

    }
}