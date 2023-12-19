using UnityEngine;
using System.IO;
using Newtonsoft.Json;


// class serializes camera sets as JSON-Files and deserializes from JSON-Files
public class CameraSetSerializer
{
    public static void SaveCameraSet(CameraSet cameraSet, string filePath)
    {
        string json = JsonUtility.ToJson(cameraSet);
        File.WriteAllText(filePath, json);
    }

    public static CameraSet LoadCameraSet(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<CameraSet>(json);
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
            return null;
        }
    }
}
