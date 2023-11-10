using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    public List<Transform> cameraPositions;
    public float transitionTime;
    public string collisionLayerName;

    private Transform mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    public void OnCameraPositionChosen(int index)
    {
        if (index < 0 || index > cameraPositions.Count - 1) return;
        if (mainCamera == null) return;

        if (DOTween.IsTweening(mainCamera))
        {
            DOTween.Kill(mainCamera);
        }

        Vector3 targetPosition = cameraPositions[index].position;
        List<Vector3> obstacleAvoidingArc = new List<Vector3>();
        bool hitObstacle = false;
        if (CheckForObstacles(cameraPositions[index].position, out RaycastHit hit))
        {
            Vector3 obstaclePosition = hit.collider.transform.position;
            Bounds bounds = hit.collider.bounds;

            Vector3 point1 = (hit.point - obstaclePosition).normalized * (bounds.extents.x + bounds.extents.y + bounds.extents.z + 1f);
            Vector3 point3 = Vector3.Reflect(point1, (targetPosition - mainCamera.position).normalized);
            Vector3 point2 = point1 + point3;
            obstacleAvoidingArc.AddRange(new Vector3[] { point1, point2, point3 });

            Vector3 scaleNullifyY = new Vector3(1f, 0f, 1f);
            Vector3 heightAdjustment = Vector3.up * (targetPosition.y + mainCamera.position.y) / 2f;
            for (int i = 0; i < obstacleAvoidingArc.Count; i++)
            {
                obstacleAvoidingArc[i] = Vector3.Scale((obstacleAvoidingArc[i] + obstaclePosition), scaleNullifyY) + heightAdjustment;
            }

            hitObstacle = true;
        }

        if (hitObstacle)
        {
            List<Vector3> path = new List<Vector3>();
            path.Add(mainCamera.position);
            path.AddRange(obstacleAvoidingArc);
            path.Add(targetPosition);
            mainCamera.DOPath(path.ToArray(), transitionTime, PathType.CatmullRom).SetEase(Ease.InOutQuad);
        }
        else
        {
            mainCamera.DOMove(cameraPositions[index].position, transitionTime).SetEase(Ease.InOutQuad);
        }

        mainCamera.DORotateQuaternion(cameraPositions[index].rotation, transitionTime).SetEase(Ease.InOutQuad);
    }

    private bool CheckForObstacles(Vector3 targetPosition, out RaycastHit hit)
    {
        Vector3 diff = targetPosition - mainCamera.position;
        Vector3 direction = diff.normalized;
        float distance = diff.magnitude;
        int layer = 1 << LayerMask.NameToLayer(collisionLayerName);

        return Physics.Raycast(mainCamera.position, direction, out hit, distance, layer);
    }

}
