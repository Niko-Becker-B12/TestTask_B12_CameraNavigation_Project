using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONManager : MonoBehaviour
{
    public string folderPath = "JSON";

    string GetFilePathForObject(Object o)
    {
        return Path.Combine(folderPath, o.GetType().Name, o.name + ".json");
    }

    public void SerializeToJSON(Object objectToSerialize)
    {
        string filePath = GetFilePathForObject(objectToSerialize);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        File.WriteAllText(filePath, JsonUtility.ToJson(objectToSerialize));
    }

    public void DeserializeFromJSON(Object objectToOverwrite)
    {
        string filePath = GetFilePathForObject(objectToOverwrite);

        if (!File.Exists(filePath))
        {
            Debug.LogError($"File {filePath} does not exist! No JSON data will be loaded.");
            return;
        }

        string json = File.ReadAllText(filePath);
        JsonUtility.FromJsonOverwrite(json, objectToOverwrite);
    }
}
