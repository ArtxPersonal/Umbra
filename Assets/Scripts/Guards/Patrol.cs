using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform target;
    private Vector3 playerLastLocation;
    public Transform head;
    private Vector3 raycastDirection;

    private float attackR = 30f;
    private float timer = 0f;
    private float countDownToIdle = 0f;
    private float lostTimer = 0;

    private NavMeshAgent guardNav;
    private bool alert = false;

    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    private bool playerSeen = false;
    private bool playerNoticed = false;
    private bool playerLost = true;
    private bool startTimer = false;
    private bool playerVisible = false;
    private bool playerVisibleFirst = false;

    [Header("FakePlayer")]
    private bool fakePlayerSeen = false;
    private bool fakePlayerNoticed = false;
    private bool fakePlayerLost = true;
    public Transform fakeTarget;
    private Vector3 fakePlayerLastLocation;
    private float fakePlayerTimer = 0;
    private float fakeplayerLostTimer = 0;
    private float fakePlayerCountdownToIdle = 0;
    private bool fakePlayerVisible = false;
    private bool fakePlayerVisibleFirst = false;
       
    private void Awake()
    {
        guardNav = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        CanSeeFakePlayer();   
    }

    private void CanSeePlayer()
    {
        if ((playerSeen || playerNoticed) && !fakePlayerVisibleFirst)
        {
            raycastDirection = (target.position - head.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(head.position, raycastDirection, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //add distance check for timer countdown speed

                    playerVisible = true;
                    if (playerNoticed && playerVisible && !fakePlayerVisible)
                    {
                        playerVisibleFirst = true;
                    }
                    else
                    {
                        playerVisibleFirst = false;
                    }

                    if (alert == true)
                    {
                        timer = 3f;
                    }
                    transform.LookAt(target);
                    if (timer < 3)
                    {
                        timer += Time.deltaTime;
                    }
                    else if (timer >= 3)
                    {
                        lostTimer = 0f;
                        playerNoticed = true;
                        playerLost = false;
                        playerLastLocation = target.position;
                        transform.LookAt(playerLastLocation);
                        Debug.DrawRay(head.position, raycastDirection * hit.distance, Color.green);
                    }
                }

                else if (!hit.collider.CompareTag("Player"))
                {
                    
                    Debug.DrawRay(head.position, raycastDirection * hit.distance, Color.red);
                    lostTimer += Time.deltaTime;

                    if (lostTimer < 1.8f)
                    {
                        playerLastLocation = target.position;
                    }

                    if (lostTimer >= 3f)
                    {
                        playerLost = true;
                    }
                }
            }
        }


        if (playerNoticed)
        {
            alert = true;
            transform.LookAt(playerLastLocation);

            Vector3 _moveTo = playerLastLocation;
            guardNav.destination = _moveTo;
            if (playerLost)
            {
                countDownToIdle += Time.deltaTime;
                if (countDownToIdle >= 3f)
                {
                    playerNoticed = false;
                    countDownToIdle = 0f;
                }
            }
        }
        else if (!playerNoticed && timer >= 3)
        {
            countDownToIdle += Time.deltaTime;

            if (countDownToIdle >= 10f)
            {
                countDownToIdle = 0f;
                timer = 0f;
                CheckAlert();
            }

            // PatrolArea();
        }
    }

    private void CanSeeFakePlayer()
    {
        if ((fakePlayerSeen || fakePlayerNoticed) && !playerVisibleFirst)
        {
            raycastDirection = (fakeTarget.position - head.position).normalized;

            RaycastHit hit;
            if (Physics.Raycast(head.position, raycastDirection, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("FakePlayer"))
                {
                    //add distance check for timer countdown speed

                    fakePlayerVisible = true;
                    if (fakePlayerNoticed && fakePlayerVisible && !playerVisible)
                    {
                        fakePlayerVisibleFirst = true;
                    }
                    else
                    {
                        fakePlayerVisibleFirst = false;
                    }

                    if (alert == true)
                    {
                        fakePlayerTimer = 3f;
                    }
                    transform.LookAt(fakeTarget);
                    if (fakePlayerTimer < 3)
                    {
                        fakePlayerTimer += Time.deltaTime;
                    }
                    else if (fakePlayerTimer >= 3)
                    {
                        fakeplayerLostTimer = 0;
                        fakePlayerNoticed = true;
                        fakePlayerLost = false;
                        fakePlayerLastLocation = fakeTarget.position;
                        transform.LookAt(fakePlayerLastLocation);

                        Debug.DrawRay(head.position, raycastDirection * hit.distance, Color.green);
                    }
                }

                else if (!hit.collider.CompareTag("FakePlayer"))
                {
                    fakePlayerVisible = false;

                    Debug.DrawRay(head.position, raycastDirection * hit.distance, Color.red);
                    fakeplayerLostTimer += Time.deltaTime;

                    if (fakeplayerLostTimer < 1.8f)
                    {
                        fakePlayerLastLocation = fakeTarget.position;
                    }

                    if (fakePlayerTimer >= 3f)
                    {
                        fakePlayerLost = true;
                    }
                }
            }
        }

        if (fakePlayerNoticed)
        {
            alert = true;
            transform.LookAt(fakePlayerLastLocation);

            Vector3 _fakeMoveTo = fakePlayerLastLocation;
            guardNav.destination = _fakeMoveTo;

            if (!fakeTarget.gameObject.activeInHierarchy)
            {
                fakePlayerNoticed = false;
                fakePlayerVisible = false;
                fakePlayerCountdownToIdle = 0f;
            }

            if (fakePlayerLost)
            {
                fakePlayerCountdownToIdle += Time.deltaTime;

                if (fakePlayerCountdownToIdle >= 3f)
                {
                    fakePlayerNoticed = false;
                    fakePlayerCountdownToIdle = 0f;
                }
            }
        }
        else if (!fakePlayerNoticed && fakePlayerTimer >= 3)
        {
            fakePlayerCountdownToIdle += Time.deltaTime;

            if (!fakeTarget.gameObject.activeInHierarchy)
            {
                fakePlayerCountdownToIdle = 0f;
                fakePlayerTimer = 0f;
                fakePlayerVisible = false;
                CheckAlert();
            }
            if (fakePlayerCountdownToIdle >= 10f)
            {
                fakePlayerCountdownToIdle = 0f;
                fakePlayerTimer = 0f;
                CheckAlert();
            }

            // PatrolArea();
        }
    }
    private void CheckAlert()
    {
        if (!fakePlayerNoticed && fakePlayerLost && !playerNoticed && playerLost)
        {
            alert = false;
        }
    }
    private void PatrolArea()
    {
        if (playerSeen == false)
        {
            //TODO patrol + return to current patrol point
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerSeen = true;
        }

        if(other.CompareTag("FakePlayer"))
        {
            fakePlayerSeen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerSeen = false;
        }

        if (other.CompareTag("FakePlayer"))
        {
            fakePlayerSeen = false;
        }
    }
}
