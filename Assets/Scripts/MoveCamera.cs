using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveCamera : MonoBehaviour
{   
    // Define an enum for sidestep directions
    public enum SidestepDirection
    {
        Right,
        Left,
        Forward,
        Backward
    }

    // Initialize the movable object and the camera positions
    public Transform cameraTransform; 
    public CameraPositions cameraPositions; 

    // Public variables for speed control
    public float initialMoveDuration = 1f; // Duration for initial movement towards the target
    public float postManeuverMoveDuration = 1f; // Duration for movement after the maneuver
    
    // Public Variable to select the direction in the Editor
    public SidestepDirection sidestepDirection; 

    // Public variables for maneuver control
    public float sidestepDistance = 1f;
    public float sidestepDurationMultiplier = 0.5f;
    public float toTargetDurationMultiplier = 1f;
    
    // Store the current Tween
    private Tween currentTween; 
    private Vector3 targetPosition; // Added to keep track of the target position

    // Public methods to move the camera to the target positions through OnClick events using the MoveToPosition method
    public void MoveToPosition1()
    {   
        Debug.Log("MoveToPosition1() called");
        MoveToPosition(cameraPositions.position1);
    }

    public void MoveToPosition2()
    {
        MoveToPosition(cameraPositions.position2);
    }

    public void MoveToPosition3()
    {
        MoveToPosition(cameraPositions.position3);
    }

    private void MoveToPosition(Vector3 newPosition)
    {   
        //quick fix
        // Check if moving from position 2 to position 1
        if (cameraTransform.position == cameraPositions.position2 && newPosition == cameraPositions.position1)
        {
            sidestepDirection = SidestepDirection.Left; // Use left sidestep
        }
        else
        {
            sidestepDirection = SidestepDirection.Right; // Default or other logic
        }

        Debug.Log("MoveToPosition called. Target Position: " + newPosition);

        targetPosition = newPosition; // Update the target position

        if (currentTween != null && currentTween.IsActive()) 
        {
            currentTween.Kill();
            Debug.Log("Existing tween killed.");
        }

        currentTween = cameraTransform.DOMove(newPosition, initialMoveDuration);
    }
    
    void OnTriggerEnter(Collider other)
    {    
        Debug.Log("Obstacle encountered. Performing avoidance maneuver.");

        if (other.gameObject.layer == LayerMask.NameToLayer("RayCast Obstacle"))
        {
            if (currentTween != null && currentTween.IsActive())
            {
                currentTween.Kill();
            }

            // Determine the sidestep vector based on the selected direction
            Vector3 stepDirection = Vector3.zero;
            switch (sidestepDirection)
            {
                case SidestepDirection.Left:
                    stepDirection = Vector3.left;
                    break;
                case SidestepDirection.Right:
                    stepDirection = Vector3.right;
                    break;
                case SidestepDirection.Forward:
                    stepDirection = Vector3.forward;
                    break;
                case SidestepDirection.Backward:
                    stepDirection = Vector3.back;
                    break;
            }

            // Calculate the actual durations using the multipliers
            float actualSidestepDuration = initialMoveDuration * sidestepDurationMultiplier;
            //float actualToTargetDuration = postManeuverMoveDuration * toTargetDurationMultiplier;

            // Perform the sidestep
            Vector3 sidestepPosition = transform.position + stepDirection * sidestepDistance;
            currentTween = transform.DOMove(sidestepPosition, actualSidestepDuration).OnComplete(() =>
            {
                // Calculate the duration for the movement back to the target using the multiplier
                float actualToTargetDuration = postManeuverMoveDuration * toTargetDurationMultiplier;

                // After sidestep, move back to the target
                currentTween = transform.DOMove(targetPosition, actualToTargetDuration);
            });
        }
    }
}
