using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float transitionDuration;

    Sequence activeCameraTransition;

    public void MoveCameraToWaypoint(Vector3 position, Vector3 rotation)
    {
        if (activeCameraTransition != null && activeCameraTransition.IsPlaying())
        {
            activeCameraTransition.Kill();
        }

        Camera mainCamera = Camera.main;

        activeCameraTransition = DOTween.Sequence()
            .Join(mainCamera.transform.DOMove(position, transitionDuration))
            .Join(mainCamera.transform.DORotate(rotation, transitionDuration));
    }

    public void MoveCameraToWaypoint(Transform waypoint)
    {
        MoveCameraToWaypoint(waypoint.position, waypoint.rotation.eulerAngles);
    }

    public void MoveCameraToWaypoint(CameraDataSheet dataSheet)
    {
        MoveCameraToWaypoint(dataSheet.position, dataSheet.rotation.eulerAngles);
    }
}
