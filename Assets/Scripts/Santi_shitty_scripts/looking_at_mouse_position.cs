using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class looking_at_mouse_position : MonoBehaviour
{
    // Public transform to assign the target GameObject (read-only)
    public Transform targetTransform;

    void Update()
    {
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
    }

}
