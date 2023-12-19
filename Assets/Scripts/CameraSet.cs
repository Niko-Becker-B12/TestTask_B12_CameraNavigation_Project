
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraSet", menuName = "Camera Set")]
[System.Serializable]
public class CameraSet : ScriptableObject
{
    public List<CameraInfo> cameraList; 


    public CameraSet()
    {
        cameraList = new List<CameraInfo>();
    }
}
