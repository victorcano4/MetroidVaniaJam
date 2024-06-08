using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake_sprite : MonoBehaviour
{
    // Shake parameters
    public float shakeDuration = 0.5f; // Duration of the shake
    public float shakeMagnitude = 0.2f; // Magnitude of the shake

    private Vector3 originalPosition;

    void Start()
    {
        // Store the original position of the object
        originalPosition = transform.localPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I am hit");
        // Start the shake effect when the object is hit
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Restore the object's original position after shaking
        transform.localPosition = originalPosition;
    }
}
