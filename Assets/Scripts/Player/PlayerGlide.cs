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

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isGlidingUnlocked && Input.GetKey(KeyCode.Space))
        {
            isGliding = true;
            myRigidbody.drag = 10f;
            player_animator.SetBool("isGliding", true);
        }
        else
        {
            isGliding = false;
            myRigidbody.drag = 1f;
        }

    }
}
