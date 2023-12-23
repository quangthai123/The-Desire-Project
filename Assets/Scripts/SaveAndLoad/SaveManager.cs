using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{

    private GameData gameData;
    public static SaveManager instance;

    private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;

    [SerializeField] private string fileName;

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath,fileName);
        saveManagers=FindAllSaveManager();
        LoadGame();
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else instance = this;
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
        Debug.Log("save game");
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
        if (dataHandler.Load() != null)
        {
            return true;
        }

        return false;
    }
}
