using UnityEngine;
using UnityEngine.AI;


// static class for ai pathfinding calculation
public static class PathFinder
{

    internal static Vector3[] CalcColliderAvoidingPath(Vector3 startPos, Vector3 endPos)
    {
        // using navMesh ai for pathfinding on x,z and linear interpolation for y 

   
        NavMeshPath aiPath = new NavMeshPath();
        NavMesh.CalculatePath(startPos, endPos, NavMesh.AllAreas, aiPath);

        Vector3[] wayPath = new Vector3[aiPath.corners.Length];

        if ((aiPath.corners.Length - 1) <= 0)    // dont devide thorugh zero. This case occurs when the startPos and endPos are too similar on y-axis
            return null;


        for (int i = 0; i < aiPath.corners.Length; i++)
        {

            float t = (float)i / (aiPath.corners.Length - 1); 
            float interpolatedY = Mathf.Lerp(startPos.y, endPos.y, t); 

            wayPath[i] = new Vector3(aiPath.corners[i].x, interpolatedY, aiPath.corners[i].z);
        }
        return wayPath;
    }

    public static float CalcPathLength(Vector3[] wayPath)
    {
        float pathLength = 0f;

        if (wayPath == null || wayPath.Length < 2)
        {
            Debug.LogWarning("Path is invalid for lenth calculation");
            return pathLength;
        }

        for (int i = 0; i < wayPath.Length - 1; i++)
        {
            pathLength += Vector3.Distance(wayPath[i], wayPath[i + 1]);
        }

        return pathLength;
    }


    public static float AdjustedDuration(float pathLength, float duration)
    {
        float speed = pathLength / duration;
        float adjustedDuration = pathLength / speed;
        return adjustedDuration;
    }
}
