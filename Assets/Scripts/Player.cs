using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;


[RequireComponent(typeof(Camera))]
public class Player : Singleton<Player>
{
    NavMeshAgent aiAgent;
    private Camera mainCamera;

    [Range(0.0f, 4.0f)]
    public float transitionDuration = 2f;

    public InterpolationType interpolationType = InterpolationType.Linear;

    public void MoveToNextCamera(Camera targetCamera)
    {
        PathType pathType;
        if(interpolationType == InterpolationType.Linear)
        {
            pathType = PathType.Linear;
        } else
        {
            pathType = PathType.CatmullRom;
        }

        Vector3[] wayPath = PathFinder.CalcColliderAvoidingPath(transform.position, targetCamera.transform.position);

        // adjust duration for animation depending on path length to avoid high speed at long paths 
        float adjustedDuration = PathFinder.AdjustedDuration(PathFinder.CalcPathLength(wayPath), transitionDuration);

        //Move through calculated waypoints with catmull-interpolation 
        transform.DOPath(wayPath, adjustedDuration, pathType, PathMode.Full3D, 5, new Color(1, 0, 0));

        // animate starting rotation to rotation of targetCamera

        transform.DORotateQuaternion(targetCamera.transform.rotation, adjustedDuration);

        // animate starting field of view to field of view of targetCamera
        GetComponent<Camera>().DOFieldOfView(targetCamera.fieldOfView, adjustedDuration);

    }
}

public enum InterpolationType { Linear, Catmull};


