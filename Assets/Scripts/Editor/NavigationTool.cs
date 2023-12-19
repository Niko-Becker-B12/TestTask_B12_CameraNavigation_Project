
using UnityEditor;
using UnityEngine;


// class creates a Editor Window for editing camera Sets
public class NavigationTool : EditorWindow
{
    private string cameraSetName = "";
    private string cameraSetNameJSON = "";
    private CameraSet selectedCameraSet;

    [MenuItem("Window/Navigation Tool")]
    public static void ShowWindow()
    {
        GetWindow(typeof(NavigationTool), false, "Navigation Tool");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Navigation Tool", EditorStyles.boldLabel);
        cameraSetName = EditorGUILayout.TextField("Camera Set Name", cameraSetName);
        selectedCameraSet = EditorGUILayout.ObjectField("Camera Set", selectedCameraSet, typeof(CameraSet), false) as CameraSet;

        if (GUILayout.Button("Add Camera"))
        {
            CameraManager.Instance.AddCamera();
        }

        if (GUILayout.Button("Save Camera Set"))
        {
            CameraManager.Instance.SaveCameraInfos();
        }

        if (GUILayout.Button("Load Camera Set"))
        {
            if (selectedCameraSet != null)
            {
                CameraManager.Instance.LoadCameraSet(selectedCameraSet);
            }
            else
            {
                Debug.LogWarning("You have to choose a valid camera set");
            }
        }

        if (GUILayout.Button("Save as New Camera Set"))
        {
            CameraManager.Instance.SaveCameraInfosAsNewScriptableObject(cameraSetName);
        }

        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Clear Camera Set"))
        {
            CameraManager.Instance.ClearCameraList();
        }

        GUI.backgroundColor = Color.white;
        JSONUtility();
    }

    void JSONUtility()
    {
        GUILayout.Space(20);
        GUILayout.Label("JSON Data");
        cameraSetNameJSON = EditorGUILayout.TextField("JSON Filename", cameraSetNameJSON);
        if (GUILayout.Button("Save current camera set as JSON"))
        {
            CameraManager.Instance.SaveCameraSetAsJSON(cameraSetNameJSON);
        }


        if (GUILayout.Button("Load camera set from JSON"))
        {
            CameraManager.Instance.LoadCameraSetFromJSON(cameraSetNameJSON);
        }
    }
}
