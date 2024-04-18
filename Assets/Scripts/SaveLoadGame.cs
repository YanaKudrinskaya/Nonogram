using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class SaveLoadGame : MonoBehaviour
{
    private Dictionary<string, int> _savingGame;
    private void Start()
    {
        LoadGame();
    }
    public List<string> GetPlayerNames()
    {
        List<string> keyList = new List<string>(_savingGame.Keys);
        return keyList;
    }
    public void SaveGame()
    {
        if (_savingGame.ContainsKey(Stats.PlayerName))
        {
            _savingGame[Stats.PlayerName] = Stats.Level;
        }
        else
        {
            _savingGame.Add(Stats.PlayerName, Stats.Level);
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.dat");
        SaveData data = new SaveData();
        data.saveData = _savingGame;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    private void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
            SaveData saveData = (SaveData)bf.Deserialize(file);
            _savingGame = saveData.saveData;
            file.Close();
        }
        else
        {
            Debug.LogError("There is no save data!");
            _savingGame = new Dictionary<string, int>();
        }
    }
    public void LoadGameSaving()
    {
        string playerName = GameObject.FindGameObjectWithTag("PlayerName").GetComponentInChildren<Text>().text;
        print(playerName);
        Stats.SetPlayer(playerName, _savingGame[playerName]);
    }
}

[Serializable]
public class SaveData
{
    public Dictionary<String, int> saveData;
}
