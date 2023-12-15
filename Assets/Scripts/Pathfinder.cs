using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [Tooltip("When checking for obstacles, this determines the amount of physics checks to do for each path.")]
    [Range(0, 10)]
    public int baseArcResolution = 4;
    [Tooltip("After checking for obstacles, each segment is further divided into this many segments.")]
    [Range(1, 10)]
    public int arcSubsegments = 3;
    public float avoidanceRadius = 0.3f;
    public float[] possibleArcStrengthValues = new float[] { 0.4f, 0.6f, 0.8f };

    public bool TryGetObstacleFreeArcPath(Vector3 startPosition, Vector3 endPosition, bool arcRight, out List<Vector3> waypoints)
    {
        waypoints = new List<Vector3>();

        Vector3 movementOverPath = endPosition - startPosition;
        Ray ray = new Ray(startPosition, movementOverPath);
        if (!Physics.SphereCast(ray, avoidanceRadius, movementOverPath.magnitude))
        {
            waypoints.Add(startPosition);
            waypoints.Add(endPosition);
            return true;
        }

        // Check multiple arcs if the direct path is obstructed.

        float distance = Vector3.Distance(startPosition, endPosition);
        Vector3 rightAngle = Vector3.Cross(endPosition - startPosition, Vector3.up).normalized;
        Vector3 arcDirection = rightAngle * (arcRight ? 1 : -1);

        foreach (var arcStength in possibleArcStrengthValues)
        {
            waypoints.Clear();

            DOCurve.CubicBezier.GetSegmentPointCloud(
                waypoints,
                startPosition,
                startPosition + arcStength * distance * arcDirection,
                endPosition,
                endPosition + arcStength * distance * arcDirection,
                baseArcResolution * arcSubsegments + 1);

            if (!TestIfPathIsFreeOfObstacles(waypoints, arcSubsegments))
            {
                continue;
            }
            return true;
        }

        return false;
    }

    private bool TestIfPathIsFreeOfObstacles(List<Vector3> waypoints, int stepSize)
    {
        for (int i = stepSize; i < waypoints.Count; i += stepSize)
        {
            Vector3 currentWaypoint = waypoints[i];
            Vector3 lastWaypoint = waypoints[i - stepSize];
            Vector3 movementOverStep = currentWaypoint - lastWaypoint;

            Debug.DrawLine(lastWaypoint, currentWaypoint, Color.red, 1.0f);

            Ray ray = new Ray(lastWaypoint, movementOverStep);
            if (Physics.SphereCast(ray, avoidanceRadius, movementOverStep.magnitude))
            {
                return false;
            }
        }
        return true;
    }
}
