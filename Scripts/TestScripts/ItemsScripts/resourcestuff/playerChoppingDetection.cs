using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class playerChoppingDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject tree;
    private GameObject hero;

    List<Collider> hitColliders;
    public float range;
    int layer_mask;

    private void Start()
    {
       // tree = this.gameObject;
       // hero = GameObject.FindGameObjectWithTag("Player");

        layer_mask = LayerMask.GetMask("tree");

    }


    void FixedUpdate()
    {
        //if(tree != null && hero != null)
        //    if (Vector3.Distance(tree.transform.position, hero.transform.position) < 5)
        //        resourceManager.Instance.isTouchingTree = true;
        //    else
        //        resourceManager.Instance.isTouchingTree = false;

        Vector3 direction = Vector3.back;
    //    direction = Quaternion.Euler(0, -45, 0) * direction;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * range));

        if(Physics.Raycast(theRay, out RaycastHit hit, range))
        {
            if(hit.collider.tag == "tree")
            {
                Debug.Log("hit a treeeeee");
            }
            else if (hit.collider.tag == "rock")
            {
                Debug.Log("hit a roccccc");
            }
        }

    }

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    //if collision with "tree" tagged gameObject
    //    if (hit.collider.gameObject.CompareTag("tree"))
    //    {

    //        hitColliders = Physics.OverlapSphere(transform.position, sphereRadius, layer_mask, QueryTriggerInteraction.Ignore).ToList();
    //        foreach (Collider hitCollider in hitColliders)
    //        {
    //            //gameObjects of all nearby enemies
    //            //   enemiesGO = swapToGameObject(hitColliders);
    //            //remove possible duplicate gameobjects resulting from multiple colliders from enemiesGO
    //            //  enemiesGO = enemiesGO.Distinct().ToList();

    //            //TODO make list of enemies that are outside of the range (opposite of enemiesGO), then save it to SaveDataScript
    //            /*List<GameObject> enemiesOut = new List<GameObject>();
    //            enemiesOut = swapToGameObject(hitColliders);
    //            enemiesOut = enemiesOut.Distinct().ToList();
    //            checkEnemiesOutOfRange(enemiesOut);
    //            SaveDataScript.storeSaveData(enemiesOut);*/

    //            //load battle scene
    //            Debug.Log("hit a tree");


    //        }
    //    }
    //}
}
