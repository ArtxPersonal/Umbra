using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManagerInstance;

    public static GameManager Instance { get { return gameManagerInstance; } }

    private GameManagerControls gameManagerControls;
    public GameObject mosterCam;
    public GameObject guardCam;

    public bool psyWallHit = false;

    private void Awake()
    {
        if (gameManagerInstance != null && gameManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gameManagerInstance = this;
        }

        gameManagerControls = new GameManagerControls();
    }
  
   
 
    void Start()
    {
        gameManagerControls.GameManager.SwitchCam.performed += SwitchCam;
    }

    private void OnEnable()
    {
        gameManagerControls.Enable();
    }
    private void OnDisable()
    {
        gameManagerControls.Disable();
    }

    private void SwitchCam(InputAction.CallbackContext context)
    {
        SwitchCams();
    }
    private void SwitchCams()
    {
        if(mosterCam.activeSelf == true)
        {
            mosterCam.SetActive(false);
            guardCam.SetActive(true);
        }
        else
        {
            guardCam.SetActive(false);
            mosterCam.SetActive(true);
        }
    }
}
