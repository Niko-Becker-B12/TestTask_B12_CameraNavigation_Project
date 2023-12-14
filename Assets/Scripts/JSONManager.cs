using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONManager : MonoBehaviour
{
    public CameraPositions cameraPositions;

    public void SaveToJSON()
    {
        string json = JsonUtility.ToJson(cameraPositions);
        File.WriteAllText(Application.persistentDataPath + "/CameraPositions.json", json);
        Debug.Log("Saved to " + Application.persistentDataPath + "/CameraPositions.json");
    }

    public void LoadFromJSON()
    {
        string filePath = Application.persistentDataPath + "/CameraPositions.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(json, cameraPositions);
            Debug.Log("Loaded from " + filePath);
        }
        else
        {
            Debug.LogError("File not found");
        }
    }
}
