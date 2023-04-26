using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsyWallCol : MonoBehaviour
{
    public GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //GameManager.Instance.psyWallHit = true;
        player.GetComponent<MonsterControlsScript>().StopPsyWallFunction();
    }
}
