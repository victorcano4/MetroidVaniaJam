using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Sinusoidal_light : MonoBehaviour
{
    // Parameters for the sine wave
    public float amplitude = 1.0f;   // The peak value of the wave
    public float frequency = 1.0f;   // The number of cycles per second
    public float phaseShift = 0.0f;  // The phase shift of the wave
    public float verticalShift = 0.0f; // The vertical shift of the wave

    // Reference to the Light2D component
    public Light2D light2D;

    void Start()
    {
        // Get the Light2D component attached to the GameObject
        light2D = GetComponent<Light2D>();
        if (light2D == null)
        {
            Debug.LogError("No Light2D component found on this GameObject.");
        }
    }

    void Update()
    {
        if (light2D != null)
        {
            // Calculate the time-based sinusoidal value
            float time = Time.time;
            float sinusoidalValue = amplitude * Mathf.Sin((2 * Mathf.PI * frequency * time) + phaseShift) + verticalShift;

            // Update the light intensity
            light2D.intensity = Mathf.Max(0, sinusoidalValue); // Ensure intensity is non-negative
        }
    }
}
