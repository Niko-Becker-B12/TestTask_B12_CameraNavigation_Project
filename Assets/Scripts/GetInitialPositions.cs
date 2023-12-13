using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInitialPositions : MonoBehaviour
{
    // Define the slots for camera positions
    public GameObject cameraPosition1;
    public GameObject cameraPosition2;
    public GameObject cameraPosition3;

    // Define the slot for the CameraPositions scriptable object
    public CameraPositions cameraPositions;
    
    void Start()
    {
        // Initialize the CameraPositions scriptable object with the initial positions
        cameraPositions.position1 = cameraPosition1.transform.position;
        cameraPositions.position2 = cameraPosition2.transform.position;
        cameraPositions.position3 = cameraPosition3.transform.position;
    }
}
