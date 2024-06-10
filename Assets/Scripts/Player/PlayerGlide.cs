using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGlide : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public bool isGliding = false;
    public Animator player_animator;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isGliding = true;
            myRigidbody.drag = 10f;
        }
        else
        {
            isGliding = false;
            myRigidbody.drag = 1f;
        }


        //Animation
        if (isGliding)
        {
            player_animator.SetBool("isGliding", true);
        }
    }
}
