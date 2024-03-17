using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;
    private GameData gameData;
    private List<IGameData> gameDataList;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameDataList = FindAllGameDataObjects();
    }

    public void NewGame()
    {
        gameData = new GameData(AchievementManager.instance.achievements);
    }

    public void LoadGame()
    {
        if(gameData == null)
        {
            Debug.Log("No data to load found");
            NewGame();
        } else
        {
            foreach(IGameData gameDataObj in  gameDataList)
            {
                gameDataObj.LoadData(gameData);
            }

            Debug.Log("LoadedGame");
            Debug.Log(gameData.achievements);
        }
    }

    public void SaveGame() 
    {
        foreach (IGameData gameDataObj in gameDataList)
        {
            gameDataObj.SaveData(ref gameData);
        }

        Debug.Log("Saved data");
        Debug.Log(gameData.achievements);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IGameData> FindAllGameDataObjects()
    {
        IEnumerable<IGameData> gameDataObjList = FindObjectsOfType<MonoBehaviour>().OfType<IGameData>();
        return new List<IGameData>(gameDataObjList);
    }
}
