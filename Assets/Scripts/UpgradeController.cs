using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public enum Upgrade
    {
        Gliding,
        RecoilJump
    }
    public Upgrade UpgradeType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "player_prefab")
        {
            switch(UpgradeType)
            {
                case Upgrade.Gliding:
                    // play transforming animation
                    collision.GetComponent<PlayerGlide>().isGlidingUnlocked = true;
                    break;
                case Upgrade.RecoilJump:
                    //play transforming animation
                    collision.GetComponent<PlayerShooting>().isRecoilJumpUnlocked = true;
                    break;
            }
        }
    }
}
