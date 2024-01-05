using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SaveManager : MonoBehaviour
{

    private GameData gameData;
    public static SaveManager instance;

    private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;


    [SerializeField] private string fileName;

    private void Start()
    {
        //string dataPath = Application.persistentDataPath;
        //Debug.Log("Persistent Data Path: " + dataPath);
        //dataHandler = new FileDataHandler(dataPath, fileName);
        //Debug.Log("handle" + dataHandler);
        //saveManagers = FindAllSaveManager();
        //LoadGame();
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else instance = this;

        string dataPath = Application.persistentDataPath;
        
        dataHandler = new FileDataHandler(dataPath, fileName);
        

        saveManagers = FindAllSaveManager();
        LoadGame();
    }
    public void NewGame()
    {
        gameData = new GameData();

    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("no game data");
            NewGame();
        }
        foreach(ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach(ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManager()
    {
        IEnumerable<ISaveManager> saveManagers=FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }

    [ContextMenu("Delete saved file")]
    public void DeleteSavedData()
    {
        dataHandler=new FileDataHandler(Application.persistentDataPath, fileName);
        dataHandler.Delete();
    }

    public bool HasSaveData()
    {
        try
        {
         
            if (dataHandler.Load() != null)
            {
              
                return true;
            }
         
            return false;
        }
        catch (Exception e)
        {
           
            return false;
        }
    }

}
