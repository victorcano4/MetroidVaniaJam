using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGlide : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public bool isGliding = false;
    public Animator player_animator;
    public bool isGlidingUnlocked = false;
    public float rb_gliding_drag = 10;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isGlidingUnlocked && Input.GetKey(KeyCode.LeftShift))
        {
            isGliding = true;
            myRigidbody.drag = rb_gliding_drag;
            player_animator.SetBool("isGliding", true);
        }
        else
        {
            isGliding = false;
            myRigidbody.drag = 1f;
            player_animator.SetBool("isGliding", false);
        }

    }
}
