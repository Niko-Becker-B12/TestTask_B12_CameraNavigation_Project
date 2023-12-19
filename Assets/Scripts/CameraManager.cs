
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// class controlls all cameras in the scene excpet the player camera 
public class CameraManager : Singleton<CameraManager>
{
    public GameObject cameraNavigationUI;
    public GameObject cameraButtonPrefab;
    public GameObject cameraPrefab;
    public GameObject cameraContainer;

    public CameraSet cameraSet;
    private List<GameObject> cameraList = new();

    public void AddCamera()
    {
        GameObject newCameraObject = Instantiate(cameraPrefab, cameraContainer.transform);
        cameraList.Add(newCameraObject);
        newCameraObject.name = "cam_position_" + (cameraList.Count).ToString();

        GameObject newCameraButton = Instantiate(cameraButtonPrefab, cameraNavigationUI.transform);
        newCameraButton.name = "cam_position_" + (cameraList.Count).ToString() + "_btn";
        newCameraButton.GetComponentInChildren<Text>().text = "position " + (cameraList.Count).ToString();
        newCameraButton.GetComponent<CamButton>().linkedCamera = newCameraObject.GetComponent<Camera>();
    }

    public void AddCamera(CameraInfo camInfo)
    {
        GameObject newCameraObject = Instantiate(cameraPrefab, cameraContainer.transform);
        cameraList.Add(newCameraObject);
        newCameraObject.name = camInfo.cameraName;
        Camera camera = newCameraObject.GetComponent<Camera>();
        camera.fieldOfView = camInfo.fieldOfView;
        camera.transform.position = camInfo.position;
        camera.transform.rotation = camInfo.rotation;


        GameObject newCameraButton = Instantiate(cameraButtonPrefab, cameraNavigationUI.transform);
        newCameraButton.name = "cam_position_" + (cameraList.Count).ToString() + "_btn";
        newCameraButton.GetComponentInChildren<Text>().text = "position " + (cameraList.Count).ToString();
        newCameraButton.GetComponent<CamButton>().linkedCamera = newCameraObject.GetComponent<Camera>();
    }

    public void ClearCameraList()
    {
        cameraList.Clear();
        DestroyAllChildren(cameraContainer);
        DestroyAllChildren(cameraNavigationUI);
    }

    public void SaveCameraInfos()
    {
        if(cameraSet == null)
        {
            cameraSet = ScriptableObject.CreateInstance<CameraSet>();
        }
        cameraSet.cameraList.Clear();
        foreach(GameObject cameraObject in cameraList)
        {
            cameraSet.cameraList.Add(ExtractCameraInfo(cameraObject.GetComponent<Camera>()));
        }
    }

    public void SaveCameraInfosAsNewScriptableObject(string setName)
    {
        SaveCameraInfos();
        CameraSet newCameraSet = ScriptableObject.CreateInstance<CameraSet>();
        newCameraSet.cameraList = cameraSet.cameraList;

        string path = "Assets/Data/" + setName + ".asset";
        UnityEditor.AssetDatabase.CreateAsset(newCameraSet, path);

        Debug.Log("Camera-Info saved in : " + path);
    }

    public void LoadCameraSet(CameraSet camSet)
    {
        if (camSet == null)
        {
            Debug.Log("No Scriptable Object Camera Set to load camera information");
            return;
        }

        ClearCameraList();
        foreach(CameraInfo camInfo in camSet.cameraList)
        {
            AddCamera(camInfo);
        }
    }


    private void DestroyAllChildren(GameObject parent)
    {
        int childCount = parent.transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject child = parent.transform.GetChild(i).gameObject;
            DestroyAllChildren(child);
            DestroyImmediate(child);
        }
    }


    public CameraInfo ExtractCameraInfo(Camera cameraToExtract)
    {
        CameraInfo extractedInfo = new CameraInfo();

        // Informationen aus der Kamera extrahieren
        extractedInfo.cameraName = cameraToExtract.name;
        extractedInfo.fieldOfView = cameraToExtract.fieldOfView;
        extractedInfo.position = cameraToExtract.transform.position;
        extractedInfo.rotation = cameraToExtract.transform.rotation;

        return extractedInfo;
    }

    public void SaveCameraSetAsJSON(string cameraSetName)
    {
        string path = "Assets/Data/" + "JSON/" + cameraSetName + ".json";
        CameraSetSerializer.SaveCameraSet(cameraSet,path);
    }

    public void LoadCameraSetFromJSON(string cameraSetName)
    {
        string path = "Assets/Data/" + "JSON/" + cameraSetName + ".json";
        LoadCameraSet(CameraSetSerializer.LoadCameraSet(path));
    }
}




