using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOrientation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private SpriteRenderer gunSprite;
    private bool facingRight; 

    private void Update()
    {
        FlipBasedOnMousePosition();
    }

    private void FlipBasedOnMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x > transform.position.x)
        {
            playerSprite.flipX = false;
            gunSprite.flipY = false;
            facingRight = false;
        }
        else if (mousePosition.x < transform.position.x)
        {
            playerSprite.flipX = true;
            gunSprite.flipY = true;
            facingRight = true;
        }
    }

    public bool PlayerFacingRight()
    { 
        return facingRight;
    }
}
