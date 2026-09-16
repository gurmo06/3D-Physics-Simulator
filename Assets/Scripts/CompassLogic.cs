using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassLogic : MonoBehaviour
{
    public Transform target; // Target object or direction to point the compass towards
    public Transform playerCamera; // Camera object which compass points from

    private RectTransform compassRectTransform;

    void Start()
    {
        // Ensure that the references are set in the Unity Editor
        if (target == null)
        {
            Debug.LogError("Target Transform reference is not set!");
            return;
        }

        if (playerCamera == null)
        {
            Debug.LogError("Player Cmaera reference is not set!");
            return;
        }

        // Get the RectTransform component of the compass image
        compassRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Calculate the direction from the player's forward vector to the target
        Vector3 direction = target.position - playerCamera.position;
        Vector3 playerForward = playerCamera.forward;

        // Project the target direction onto the player's local x-z plane
        Vector3 projectedDirection = Vector3.ProjectOnPlane(direction, playerCamera.up).normalized;

        // Calculate the angle between the forward direction of the player and the projected target direction
        float angle = Vector3.SignedAngle(playerForward, projectedDirection, playerCamera.up);

        // Apply rotation to the compass image based on the calculated angle
        compassRectTransform.rotation = Quaternion.Euler(0f, 0f, -angle);
    }
}
