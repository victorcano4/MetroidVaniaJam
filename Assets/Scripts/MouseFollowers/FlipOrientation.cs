using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOrientation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private SpriteRenderer gunSprite;
    private Vector2 savedPosition;

    private void Start()
    {
        savedPosition = playerSprite.transform.position;
    }
    private void Update()
    {
        FlipBasedOnMousePosition();
    }

    void FlipBasedOnMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x > transform.position.x)
        {
            playerSprite.flipX = false;
            gunSprite.flipY = false;
            playerSprite.transform.position = new Vector2(savedPosition.x, transform.position.y);
        }
        else if (mousePosition.x < transform.position.x)
        {
            playerSprite.flipX = true;
            gunSprite.flipY = true;
            playerSprite.transform.position = new Vector2(savedPosition.x + 0.60f, transform.position.y);

        }
    }
}
