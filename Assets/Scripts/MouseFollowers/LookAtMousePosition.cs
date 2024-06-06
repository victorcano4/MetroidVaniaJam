using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMousePosition : MonoBehaviour
{
    // public transform to assign the target GameObject (read-only)
    // public Transform targetTransform;

    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure z position is 0 for 2D

        // Calculate the direction from the player to the mouse
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle between the player's forward direction and the direction to the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the flashlight to face the mouse direction
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        /* ---Old code if needed---
        // Check if the targetTransform has been assigned
        if (targetTransform == null)
            return;

        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = targetTransform.position.z; // Ensure the z position matches the target's z position

        // Calculate the direction from the object to the mouse position
        Vector3 direction = mousePosition - targetTransform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to this object (not the target object)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        */
    }
}
