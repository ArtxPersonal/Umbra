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

    [Header("SearchForPlayer")]
    private float searchRadius = 50;
    private Vector3 randomSearch;
    private bool searchForPlayerCheck = false;
    private float searchTimer = 0;
       
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

        if(searchForPlayerCheck == true)
        {
            SearchForPlayer();
        } 
    }

    private void CanSeePlayer()
    {
        if (playerSeen || playerNoticed)
        {
            raycastDirection = (target.position - head.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(head.position, raycastDirection, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //add distance check for timer countdown speed

                    playerVisible = true;

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
                        playerLastLocation = new Vector3(target.position.x, 0, target.position.z);
                        transform.LookAt(playerLastLocation);
                        Debug.DrawRay(head.position, raycastDirection * hit.distance, Color.green);
                    }
                }

                else if (!hit.collider.CompareTag("Player"))
                {

                    Debug.DrawRay(head.position, raycastDirection * hit.distance, Color.red);
                    lostTimer += Time.deltaTime;

                    if (lostTimer < 0.5f)
                    {
                        transform.LookAt(target);
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

            if (!guardNav.hasPath)
            {
                SearchForPlayer();
            }
           
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
            SearchForPlayer();
            if (countDownToIdle >= 10f)
            {
                countDownToIdle = 0f;
                timer = 0f;
                CheckAlert();
            }
        }
    }

    private void SearchForPlayer()
    {
        if (!guardNav.hasPath)
        {
            randomSearch = Random.insideUnitSphere * searchRadius;
            randomSearch += target.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomSearch, out hit, searchRadius, 1);
            Vector3 finalPosition = hit.position;
            transform.LookAt(finalPosition);
            guardNav.destination = finalPosition;
        }
    }

    private void CheckAlert()
    {
        if (/*!fakePlayerNoticed && fakePlayerLost &&*/ !playerNoticed && playerLost)
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

        //if(other.CompareTag("FakePlayer"))
        //{
        //    fakePlayerSeen = true;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerSeen = false;
        }

        //if (other.CompareTag("FakePlayer"))
        //{
        //    fakePlayerSeen = false;
        //}
    }
}
