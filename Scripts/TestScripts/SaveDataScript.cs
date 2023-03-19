using System.Collections.Generic;
using UnityEngine;

public class SaveDataScript : MonoBehaviour
{
    public static SaveDataScript instance { get; private set; }

    public static List<objData> ObjectPositions = new List<objData>();
    public static Vector3 heroCoords = new Vector3();
    static float timer = 0;

    public GameObject enemyPrefab;
    private static bool shouldLoad = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //   Debug.Log("created frenemy");
            Instantiate(enemyPrefab, new Vector3(heroCoords.x + 10, heroCoords.y, heroCoords.z), transform.rotation);
            // en.GetComponent<>
        }
        if (timer > 0)
            timer -= Time.deltaTime;
        if (shouldLoad == true && timer <= 0)
        {

            foreach (objData objdata in ObjectPositions)
            {
                //    Debug.Log("putting at : x" + objdata.xyzCoords[0] + ", y" + objdata.xyzCoords[1] + ", z" + objdata.xyzCoords[2]);

                Instantiate(enemyPrefab, new Vector3(objdata.xyzCoords[0], objdata.xyzCoords[1], objdata.xyzCoords[2]), transform.rotation);
                //     Instantiate(enemyPrefab, new Vector3(heroCoords.x + 10, heroCoords.y, heroCoords.z), transform.rotation);
            }
            shouldLoad = false;
        }
    }

    public static void storeSaveData(List<GameObject> lObjects)
    {

        ObjectPositions.Clear();
        foreach (GameObject go in lObjects)
        {
            float[] coords = { go.transform.position.x, go.transform.position.y, go.transform.position.z };
            //Debug.Log("saving coords: x" + coords[0] + ", y" + coords[1] + ", z" + coords[2]);

            objData objdata = new objData(go, coords);
            ObjectPositions.Add(objdata);
        }
    }

    public static List<objData> GetSaveData()
    {
        return ObjectPositions;
    }

    public static void LoadEnemies()
    {
        Character_Controller.teleportPlayer(heroCoords);
        shouldLoad = true;
        timer = 0.1f;
    }
}
public class objData
{
    public GameObject gameObject { get; set; }
    public float[] xyzCoords { get; set; }

    public objData(GameObject gameObject, float[] xyzCoords)
    {
        this.gameObject = gameObject;
        this.xyzCoords = xyzCoords;
    }

}
