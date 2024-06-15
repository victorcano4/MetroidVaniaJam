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
    public Sprite spriteS_pressed;
    public Sprite spriteS;

    // Names of the child objects to change the sprites of
    public string childObjectA;
    public string childObjectD;
    public string childObjectS;

    // Private references to the SpriteRenderer of the children
    private Image childSpriteRendererA;
    private Image childSpriteRendererD;
    private Image childSpriteRendererS;


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

        // Find the child object D by name and get its SpriteRenderer component
        Transform childTransformS = transform.Find(childObjectS);
        if (childTransformS != null)
        {
            childSpriteRendererS = childTransformS.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("Child object S not found!");
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

        else if (Input.GetKeyUp(KeyCode.A) && childSpriteRendererA != null)
        {
            // Change the sprite of the child object A to spriteA
            childSpriteRendererA.sprite = spriteA;
        }

        // Check if the 'D' key is pressed and the childSpriteRendererY is assigned
        if (Input.GetKeyDown(KeyCode.D) && childSpriteRendererD != null)
        {
            // Change the sprite of the child object D to spriteD
            childSpriteRendererD.sprite = spriteD_pressed;
        }

        // Check if the 'D' key is pressed and the childSpriteRendererY is assigned
        else if (Input.GetKeyUp(KeyCode.D) && childSpriteRendererD != null)
        {
            // Change the sprite of the child object D to spriteD
            childSpriteRendererD.sprite = spriteD;
        }

        // Check if the 'S' key is pressed and the childSpriteRendererX is assigned
        if (Input.GetKeyDown(KeyCode.S) && childSpriteRendererS != null)
        {
            // Change the sprite of the child object S to spriteS
            childSpriteRendererS.sprite = spriteS_pressed;
        }

        else if (Input.GetKeyUp(KeyCode.S) && childSpriteRendererS != null)
        {
            // Change the sprite of the child object S to spriteS
            childSpriteRendererS.sprite = spriteS;
        }



    }
}
