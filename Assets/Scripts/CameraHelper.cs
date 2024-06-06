using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// The only purpose of this script is setting the camera target to follow.
// This has to be done because the target is the player sprite and the reference gets lost when the prefab is reimported.
public class CameraHelper : MonoBehaviour
{
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.Find("PlayerSprite").transform;
    }
}
