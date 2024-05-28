using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiming_icon_movement : MonoBehaviour
{
    // Speed at which the object will follow the mouse
    public float speed = 5f;

    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure z position is 0 for 2D

        // Move the object towards the mouse position
        transform.position = Vector3.Lerp(transform.position, mousePosition, speed * Time.deltaTime);
    }

}

