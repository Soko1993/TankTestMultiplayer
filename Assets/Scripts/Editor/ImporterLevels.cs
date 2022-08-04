using TanksEngine.LevelCreator;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ImporterLevels : Editor
{
    [MenuItem("TankEngine/Import levels from folder")]
    public static void ImportFromFiles()
    {
        int n = 0;
        string[] allFoundFiles = Directory.GetFiles(Application.dataPath + $@"\Levels\", "*.txt", SearchOption.AllDirectories);
        foreach (string file in allFoundFiles)
        {
            Debug.Log(file);
            LoadFromFile(file,n);
            n++;
        }
        
    }
    
    private static void LoadFromFile(string name,int n)
    {
        //string json = File.ReadAllText(Application.dataPath + $@"\Levels\{name}.txt");
        string json = File.ReadAllText(name);
        LevelData levelData = Newtonsoft.Json.JsonConvert.DeserializeObject<LevelData>(json);
        LevelDataSO levelDataSO = new();
        levelDataSO.levelData = levelData;
        // AssetDatabase.CreateAsset(levelDataSO, $"Assets/Data files/Levels/Imported/{levelDataSO.levelData.LevelName}.asset");
        AssetDatabase.CreateAsset(levelDataSO, $"Assets/Resources/Levels/{levelDataSO.levelData.LevelName}.asset");
        
    }
    [MenuItem("TankEngine/Check...")]
    private static void Check()
    {
        //var a = Resources.Load<Object>("Levels/level-x");
        //LevelDataSO s = Resources.Load<LevelDataSO>("Levels/level-x");
        //var b = Resources.Load<UnityEngine.ScriptableObject>("Levels/levelfasf-x");
        //var c = Resources.Load<LevelDataSO>("Levels/level-x");

        //string json = File.ReadAllText(Application.dataPath + "Levels2/level-x");

        var q = Resources.Load<TextAsset>("Levels2/level-x");
        LevelData levelData = Newtonsoft.Json.JsonConvert.DeserializeObject<LevelData>(q.text);
    }
}
