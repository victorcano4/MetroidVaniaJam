using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightDescrease : MonoBehaviour
{
    // Public reference to the target Light2D component
    public Light2D playerLight;

    // Rate at which the light intensity decreases per second
    public float decreaseRate;

    // Minimum intensity value
    public float minIntensity;

    // Amount by which to increase the light intensity
    public float increaseAmount;

    // Maximum intensity value
    public float maxIntensity;

    //UI light slider
    public Slider slider;

    private LightRechargerMaster lightManager; // Reference to the LightManager

    private void Start()
    {
        // Find the LightManager in the scene
        lightManager = FindObjectOfType<LightRechargerMaster>();
    }



    void Update()
    {
        // Check if the targetLight has been assigned and if its intensity is above the minimum
        if (playerLight != null && playerLight.intensity > minIntensity)
        {
            // Decrease the light intensity over time
            playerLight.intensity -= decreaseRate * Time.deltaTime;

            // Clamp the intensity to the minimum value
            playerLight.intensity = Mathf.Max(playerLight.intensity, minIntensity);

            //Adjust light value in the UI slider
            slider.value = playerLight.intensity/maxIntensity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the tag "Player"
        if (collision.gameObject.CompareTag("LightRecharge"))
        {
            // Check if the targetLight has been assigned
            if (playerLight != null)
            {
                // Increase the light intensity
                playerLight.intensity += increaseAmount;

                // Clamp the intensity to the maximum value
                playerLight.intensity = Mathf.Min(playerLight.intensity, maxIntensity);

                // Notify the LightManager to respawn the light recharge
                lightManager.CollectLight(collision.gameObject.transform.position);

                // Destroy the light recharge
                Destroy(collision.gameObject);
            }
        }
    }

}
