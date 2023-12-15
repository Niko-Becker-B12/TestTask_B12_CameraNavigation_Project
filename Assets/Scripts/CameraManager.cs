using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float transitionDuration;

    Sequence activeCameraTransition;

    public void MoveCameraToWaypoint(Transform waypoint)
    {
        if (activeCameraTransition != null && activeCameraTransition.IsPlaying())
        {
            activeCameraTransition.Kill();
        }
        
        Camera mainCamera = Camera.main;

        activeCameraTransition = DOTween.Sequence()
            .Join(mainCamera.transform.DOMove(waypoint.position, transitionDuration))
            .Join(mainCamera.transform.DORotate(waypoint.rotation.eulerAngles, transitionDuration));
    }
}
