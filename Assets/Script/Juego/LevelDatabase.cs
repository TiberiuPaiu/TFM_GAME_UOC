using UnityEngine;
using System.IO;

[System.Serializable]
public class LevelDatabase
{
    public LevelDataJson[] levels;

    public static LevelDatabase LoadFromJson()
    {
        string path = Application.dataPath + "/ConfigsJSON/levels.json";

        string json = File.ReadAllText(path);

        return JsonUtility.FromJson<LevelDatabase>(json);
    }
}