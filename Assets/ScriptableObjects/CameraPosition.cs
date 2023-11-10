using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraPosition : ScriptableObject
{
    public Vector3 position;
    public Quaternion rotation;

    public void SaveToJson()
    {
        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/CameraPosition.json", json);
    }

    public static bool FromJson(out CameraPosition cameraPosition)
    {
        cameraPosition = ScriptableObject.CreateInstance<CameraPosition>();
        string json = System.IO.File.ReadAllText(Application.persistentDataPath + "/CameraPosition.json");
        if (json != null)
        {
            JsonUtility.FromJsonOverwrite(json, cameraPosition);
            return true;
        }
        return false;
        
    }
}
