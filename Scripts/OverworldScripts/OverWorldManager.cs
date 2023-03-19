using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverWorldManager : MonoBehaviour
{
    public enum PlayerState { BUSY, COLLECTING, FIGHTING, PLAYING, LOOTING }

    public static OverWorldManager instance;

    public PlayerState playerState;

    public List<int> destroyedEnemyIDList = new List<int>();
    public List<GameObject> listOfEnemies = new List<GameObject>();
    public List<GameObject> listOfCards = new List<GameObject>();
    public DialogueVariables dialogueVariables;
    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    private void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.root);
            dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        }
    }

    public void loadBattleScene(List<GameObject> enemies)
    {
        listOfEnemies = copyListOfGo(enemies);

        NewBattleSystem.enemyGoToEnum(enemies);

        //deletes enemies that are added to battlescene, useful if we were to use additivescene
        foreach (GameObject go in listOfEnemies)
            destroyedEnemyIDList.Add(go.GetComponent<EnemyDescription>().ID);
            
            //Destroy(go);
        //set hero coordinates
        SaveDataScript.heroCoords = GameObject.FindGameObjectWithTag("Player").transform.position;

        //use additive here, if we use it
        SceneManager.LoadScene("BattleScene");
    }

    public List<GameObject> copyListOfGo(List<GameObject> goList)
    {
        List<GameObject> list = new List<GameObject>();

        foreach (GameObject go in goList)
        {
            list.Add(go);
        }
        return list;
    }
}