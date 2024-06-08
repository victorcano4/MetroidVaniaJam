using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PressAD : MonoBehaviour
{
    // Public references to the new sprites
    public Sprite spriteA_pressed;
    public Sprite spriteD_pressed;
    public Sprite spriteA;
    public Sprite spriteD;

    // Names of the child objects to change the sprites of
    public string childObjectA;
    public string childObjectD;

    // Private references to the SpriteRenderer of the children
    private Image childSpriteRendererA;
    private Image childSpriteRendererD;


    void Start()
    {
        // Find the child object A by name and get its SpriteRenderer component
        Transform childTransformA = transform.Find(childObjectA);
        if (childTransformA != null)
        {
            childSpriteRendererA = childTransformA.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("Child object A not found!");
        }

        // Find the child object D by name and get its SpriteRenderer component
        Transform childTransformD = transform.Find(childObjectD);
        if (childTransformD != null)
        {
            childSpriteRendererD = childTransformD.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("Child object D not found!");
        }
    }

    void Update()
    {
        // Check if the 'A' key is pressed and the childSpriteRendererX is assigned
        if (Input.GetKeyDown(KeyCode.A) && childSpriteRendererA != null)
        {
            // Change the sprite of the child object A to spriteA
            childSpriteRendererA.sprite = spriteA_pressed;
        }

        // Check if the 'D' key is pressed and the childSpriteRendererY is assigned
        if (Input.GetKeyDown(KeyCode.D) && childSpriteRendererD != null)
        {
            // Change the sprite of the child object D to spriteD
            childSpriteRendererD.sprite = spriteD_pressed;
        }

        if (Input.GetKeyUp(KeyCode.A) && childSpriteRendererA != null)
        {
            // Change the sprite of the child object A to spriteA
            childSpriteRendererA.sprite = spriteA;
        }

        // Check if the 'D' key is pressed and the childSpriteRendererY is assigned
        if (Input.GetKeyUp(KeyCode.D) && childSpriteRendererD != null)
        {
            // Change the sprite of the child object D to spriteD
            childSpriteRendererD.sprite = spriteD;
        }
    }
}
