using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONManager : MonoBehaviour
{
    public void WriteMainCameraPosition()
    {
        Transform mainCamera = Camera.main.transform;
        CameraPosition cameraPosition = ScriptableObject.CreateInstance<CameraPosition>();
        cameraPosition.position = mainCamera.position;
        cameraPosition.rotation = mainCamera.rotation;
        cameraPosition.SaveToJson();
    }

    public void ReadMainCameraPosition()
    {
        if (CameraPosition.FromJson(out CameraPosition cameraPosition))
        {
            Transform mainCamera = Camera.main.transform;
            mainCamera.position = cameraPosition.position;
            mainCamera.rotation = cameraPosition.rotation;
        }
    }
}
