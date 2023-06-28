using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsyWallCol : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GuardHead"))
        {
            player.GetComponent<MonsterControlsScript>().StopPsyWallFunction();
            other.gameObject.GetComponentInParent<Patrol>().HitPsyWall();
        }
    }
}
