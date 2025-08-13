using System.IO;
using Services.GameDataService;
using UnityEngine;
using Zenject;

public class GameDataService : IInitializable
{
    private readonly string _savePath = Path.Combine(Application.persistentDataPath, "game_data.json");

    public GameData Data { get; private set; }

    public void Initialize()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            Data = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Data = new GameData();
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(_savePath, json);
    }
}
