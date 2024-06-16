using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BlockPlayerMovement : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;
    public GameObject wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hey!");
            virtualCamera.Follow = null;

            gameObject.GetComponent<Collider2D>().enabled = false;
            wall.SetActive(true);
        }
    }

}
