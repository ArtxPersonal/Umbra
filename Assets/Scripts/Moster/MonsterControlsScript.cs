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
    [SerializeField] private int movementSpeed = 10;
    private Vector2 monsterMovement;

    [Header("PsyWall")]
    bool canPsyWall = true;
    public GameObject PsyWallPrefab;
    GameObject PsyWallCopy;
    public GameObject poofParticleWall;
    GameObject poofParticleWallCopy;
    bool stopPsyWallCoroutineBool = false;

    [Header("FakeMan")]
    public GameObject manPrefab;
    GameObject manCopy;
    bool canFakeMan = true;
    public GameObject poofParicleMan;
    GameObject poofParticleManCopy;
    private NavMeshAgent fakeMan;

    // Start is called before the first frame update
    void Awake()
    {
        monsterControls = new MonsterControls();
        monsterBody = GetComponent<Rigidbody>();
        if (monsterBody == null)
        {
            Debug.LogError("MonsterRigidBody = null");
        }
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
        monsterControls.LandControls.PsyWall.performed += PsyWallCast;
        monsterControls.LandControls.Teleport.performed += Teleport;
        //monsterControls.LandControls.FakeMan.performed += FakeManCast;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    //private void FakeManCast(InputAction.CallbackContext context)
    //{
    //    SpawnManAtMousPosFakeMan();
    //}
    private void PsyWallCast(InputAction.CallbackContext context)
    {
        SpawnAtMousePosPsyWall();
    }

    private void Movement()
    {
        monsterMovement = monsterControls.LandControls.Move.ReadValue<Vector2>();
        monsterVelocity = new Vector3(monsterMovement.x * movementSpeed, monsterBody.velocity.y, monsterMovement.y * movementSpeed);
        monsterBody.velocity = transform.TransformDirection(monsterVelocity);

    }

    //private void SpawnManAtMousPosFakeMan()
    //{
    //    if (Mouse.current.rightButton.wasPressedThisFrame && canFakeMan)
    //    {
    //        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            canFakeMan = false;
    //            manCopy = Instantiate(manPrefab, hit.point, transform.rotation) as GameObject;
    //            fakeMan = manCopy.GetComponent<NavMeshAgent>();
    //            fakeMan.destination = fakeMan.transform.position + fakeMan.transform.forward * 50;
    //            StartCoroutine(FakeManActive());
    //        }
    //    }
    //}
    //IEnumerator FakeManActive()
    //{
    //    yield return new WaitForSeconds(10);
    //    poofParticleManCopy = Instantiate(poofParicleMan, manCopy.transform.position, manCopy.transform.rotation);
    //    Destroy(manCopy);
    //    yield return new WaitForSeconds(1);
    //    Destroy(poofParticleManCopy);
    //    canFakeMan = true;
    //}

    //IEnumerator StopFakeMan()
    //{
    //    poofParticleManCopy = Instantiate(poofParicleMan, manCopy.transform.position, manCopy.transform.rotation);
    //    Destroy(manCopy);
    //    yield return new WaitForSeconds(1);
    //    Destroy(poofParticleManCopy);
    //    canFakeMan = true;
    //}

    private void SpawnAtMousePosPsyWall()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame && canPsyWall)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                canPsyWall = false;
                PsyWallCopy = Instantiate(PsyWallPrefab, hit.point, Quaternion.identity) as GameObject;
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
        if(PsyWallCopy == null)
        {
            poofParticleWallCopy = Instantiate(poofParticleWall, PsyWallCopy.transform.position, PsyWallCopy.transform.rotation);
            yield return new WaitForSeconds(1);
            Destroy(poofParticleWallCopy);
            canPsyWall = true;
            yield break;
        }
        poofParticleWallCopy = Instantiate(poofParticleWall, PsyWallCopy.transform.position, PsyWallCopy.transform.rotation);
        Destroy(PsyWallCopy);
        yield return new WaitForSeconds(1);
        Destroy(poofParticleWallCopy);
        canPsyWall = true;
        yield break;
    }

    IEnumerator StopPsyWall()
    {
        poofParticleWallCopy = Instantiate(poofParticleWall, PsyWallCopy.transform.position, PsyWallCopy.transform.rotation);
        Destroy(PsyWallCopy);
        yield return new WaitForSeconds(1);
        Destroy(poofParticleWallCopy);
        canPsyWall = true;
        yield break;
    }

    public void StopPsyWallFunction()
    { 
        stopPsyWallCoroutineBool = true;
        StartCoroutine(StopPsyWall());
        StopCoroutine(PsyWallActive());
    }

    public void Teleport(InputAction.CallbackContext context)
    {
        monsterBody.AddForce(monsterVelocity * 10, ForceMode.Impulse);
    }


    //ToDo: couple seconds checking where the guards are
    //+ Teleport
    
}
