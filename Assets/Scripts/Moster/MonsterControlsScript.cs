using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class MonsterControlsScript : MonoBehaviour
{
    private MonsterControls monsterControls;
    private Vector3 monsterVelocity;
    public Camera cam;

    [Header("MonsterMovement")]
    public Rigidbody monsterBody;
    [SerializeField] private int movementSpeed = 8;
    private Vector2 monsterMovement;

    [Header("PsyWall")]
    private bool canPsyWall = true;
    public GameObject PsyWallPrefab;
    GameObject PsyWallCopy;
    public GameObject poofParticleWall;
    GameObject poofParticleWallCopy;
    public bool stopPsyWallCoroutineBool = false;

    [Header("SpeedBoost")]
    private bool speedBoost = true;

    // Start is called before the first frame update
    void Awake()
    {
        monsterControls = new MonsterControls();
        monsterBody = GetComponent<Rigidbody>();
        if (monsterBody == null)
        {
            Debug.LogError("MonsterRigidBody = null");
        }
        //fakeMan = manPrefab.GetComponent<NavMeshAgent>();

    }

    private void OnEnable()
    {
        monsterControls.Enable();
    }
    private void OnDisable()
    {
        monsterControls.Disable();
    }

    private void Start()
    {
        monsterControls.LandControls.PsyWall.performed += SpawnAtMousePosPsyWall;
        monsterControls.LandControls.Teleport.performed += SpeedBoost;
        //monsterControls.LandControls.FakeMan.performed += SpawnManAtMousPosFakeMan;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    private void Movement()
    {
        monsterMovement = monsterControls.LandControls.Move.ReadValue<Vector2>();
        monsterVelocity = new Vector3(monsterMovement.x * movementSpeed, monsterBody.velocity.y, monsterMovement.y * movementSpeed);
        monsterBody.velocity = transform.TransformDirection(monsterVelocity);

    }

    private void SpawnAtMousePosPsyWall(InputAction.CallbackContext context)
    {
        if(Mouse.current.leftButton.wasPressedThisFrame && canPsyWall)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                canPsyWall = false;
                //PsyWallCopy = Instantiate(PsyWallPrefab, hit.point, Quaternion.identity) as GameObject;
                PsyWallPrefab.transform.position = hit.point;
                PsyWallPrefab.SetActive(true);
                StartCoroutine(PsyWallActive());
            }
        }
    }
    IEnumerator PsyWallActive()
    {
        yield return new WaitForSeconds(5);
        if(stopPsyWallCoroutineBool == true)
        {
            stopPsyWallCoroutineBool = false;
            yield break;
        }
        //if(PsyWallCopy == null)
        //{
        //    poofParticleWallCopy = Instantiate(poofParticleWall, PsyWallCopy.transform.position, PsyWallCopy.transform.rotation);
        //    yield return new WaitForSeconds(1);
        //    Destroy(poofParticleWallCopy);
        //    canPsyWall = true;
        //    yield break;
        //}

        //poofParticleWallCopy = Instantiate(poofParticleWall, PsyWallCopy.transform.position, PsyWallCopy.transform.rotation);
        //Destroy(PsyWallCopy);
        PsyWallPrefab.SetActive(false);
        poofParticleWall.transform.position = PsyWallPrefab.transform.position;
        poofParticleWall.SetActive(true);
        yield return new WaitForSeconds(1);
        poofParticleWall.SetActive(false);
        //Destroy(poofParticleWallCopy);
        yield return new WaitForSeconds(10);
        canPsyWall = true;
        yield break;
    }

    IEnumerator StopPsyWall()
    {
        //poofParticleWallCopy = Instantiate(poofParticleWall, PsyWallCopy.transform.position, PsyWallCopy.transform.rotation);
        poofParticleWall.transform.position = PsyWallPrefab.transform.position;
        PsyWallPrefab.SetActive(false);
        poofParticleWall.SetActive(true);
        yield return new WaitForSeconds(1);
        poofParticleWall.SetActive(false);
        yield return new WaitForSeconds(10);
        canPsyWall = true;
        yield break;
    }

    public void StopPsyWallFunction()
    {
        stopPsyWallCoroutineBool = true;
        StartCoroutine(StopPsyWall());
        StopCoroutine(PsyWallActive());
    }

    public void SpeedBoost(InputAction.CallbackContext context)
    {
        if (speedBoost)
        {
            if(monsterVelocity == Vector3.zero)
            {
                movementSpeed = 12;
                speedBoost = false;
            }
            else
            {
                movementSpeed = 12;
                speedBoost = false;
            }

            StartCoroutine(ReloadSpeedBoost());
        }
       
    }

    private IEnumerator ReloadSpeedBoost()
    {
        yield return new WaitForSeconds(2);
        movementSpeed = 8;
        yield return new WaitForSeconds(13);
        speedBoost = true;
        yield break;
    }


    //ToDo: couple seconds checking where the guards are
    //+ Teleport
    
}
