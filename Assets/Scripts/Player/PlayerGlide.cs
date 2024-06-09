using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGlide : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            myRigidbody.drag = 10f;
        }
        else
        {
            myRigidbody.drag = 1f;
        }
    }
}
