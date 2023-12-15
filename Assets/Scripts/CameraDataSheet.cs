using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data Sheets/Camera")]
public class CameraDataSheet : ScriptableObject
{
    public Vector3 position;
    public Quaternion rotation;

    public void CopyDataFrom(Camera camera)
    {
        position = camera.transform.position;
        rotation = camera.transform.rotation;
    }

    public void ApplyDataTo(Camera camera)
    {
        camera.transform.SetPositionAndRotation(position, rotation);
    }
}
