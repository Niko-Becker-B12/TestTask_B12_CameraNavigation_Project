
using UnityEngine;
using UnityEngine.UI;


// class links a new cameraButton to a specific camera that is instantiated by the CameraManager class 
[RequireComponent(typeof(Button))]
public class CamButton : MonoBehaviour
{
    public Camera linkedCamera; 

    void Start()
    {
        Button cameraButton = GetComponent<Button>();
        cameraButton.onClick.AddListener(MoveCameraOnClick);
    }

    void MoveCameraOnClick()
    {
        Player.Instance.MoveToNextCamera(linkedCamera);
    }
}
