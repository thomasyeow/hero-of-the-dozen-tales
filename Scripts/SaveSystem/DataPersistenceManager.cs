using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;

    private List<IDataPersistance> dataPersistanceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.root);
        }
    }



    private void OnEnable()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistanceObjects = FindAllDataPersistenceObjects();
        //LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData(); 
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if(gameData == null)
        {
            NewGame();
        }

        foreach (IDataPersistance dataPersistance in dataPersistanceObjects)
        {
            dataPersistance.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        this.dataPersistanceObjects = FindAllDataPersistenceObjects();

        foreach (IDataPersistance dataPersistance in dataPersistanceObjects)
        {
            dataPersistance.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private List<IDataPersistance> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
