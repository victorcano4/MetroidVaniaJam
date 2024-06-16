using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRecoilJumpsController : MonoBehaviour
{
    public int MaxRecoilJumps;
    public int CurrentRecoilJumps;
    public GameObject UIRecoilJumpPrefab;
    public void AddRecoilJump()
    {
        Instantiate(UIRecoilJumpPrefab, transform);
    }
    public void UseRecoilJump()
    {
        CurrentRecoilJumps--;
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    }
    public void ResetRecoilJumps()
    {
        for (int i = 0; i < MaxRecoilJumps - CurrentRecoilJumps; i++)
        {
            Instantiate(UIRecoilJumpPrefab, transform);
        }
        CurrentRecoilJumps = MaxRecoilJumps;
    }
}
