using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using DG.Tweening;

public class Move : MonoBehaviour
{
    public Transform cameraTransform; 
    public CameraPositions cameraPositions; 

    public void MoveToPosition1()
    {
        cameraTransform.DOMove(cameraPositions.position1, 1f); // Move over 1 second
    }

    // Similarly for other positions
    public void MoveToPosition2()
    {
        cameraTransform.DOMove(cameraPositions.position2, 1f);
    }

    public void MoveToPosition3()
    {
        cameraTransform.DOMove(cameraPositions.position3, 1f);
    }
}

