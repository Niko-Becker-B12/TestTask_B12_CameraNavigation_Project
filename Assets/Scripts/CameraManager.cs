using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Pathfinder pathfinder;

    public float transitionDuration = 2.0f;
    public Ease transitionEase = Ease.OutQuad;

    Sequence activeCameraTransition;

    public void MoveCameraToWaypoint(Transform waypoint)
    {
        MoveCameraToWaypoint(waypoint.position, waypoint.rotation.eulerAngles);
    }

    public void MoveCameraToWaypoint(CameraDataSheet dataSheet)
    {
        MoveCameraToWaypoint(dataSheet.position, dataSheet.rotation.eulerAngles);
    }

    public void MoveCameraToWaypoint(Vector3 position, Vector3 rotation)
    {
        if (activeCameraTransition != null && activeCameraTransition.IsActive())
        {
            activeCameraTransition.Kill();
        }

        Transform mainCamera = Camera.main.transform;

        /* Arcing while looking outward and away from the arc's pivot is very disorienting.
         * Passing the sign of the rotation along ensures the path is chosen to make the camera look inward. */
        bool arcToTheRight = Mathf.DeltaAngle(mainCamera.eulerAngles.y, rotation.y) > 0;

        if (pathfinder.TryGetObstacleFreeArcPath(mainCamera.transform.position, position, arcToTheRight, out List<Vector3> waypoints))
        {
            activeCameraTransition = DOTween.Sequence()
                .Join(mainCamera.DOPath(waypoints.ToArray(), transitionDuration).SetEase(transitionEase))
                .Join(mainCamera.DORotate(rotation, transitionDuration).SetEase(transitionEase));
        }
        else
        {
            // Fall back to linear transition in case no obstacle avoidance is possible.
            activeCameraTransition = DOTween.Sequence()
                .Join(mainCamera.DOMove(position, transitionDuration).SetEase(transitionEase))
                .Join(mainCamera.DORotate(rotation, transitionDuration).SetEase(transitionEase));
        }
    }
}
