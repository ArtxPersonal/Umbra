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
    private float searchRadius = 30;
    private Vector3 randomSearch;
    private bool searchForPlayerCheck = false;
    private float searchTimer = 0;

    [Header("Animation")]
    public Animator animator;
    public RuntimeAnimatorController idle;
    public RuntimeAnimatorController walking;
    public RuntimeAnimatorController running;
       
    private void Awake()
    {
        guardNav = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        animator.runtimeAnimatorController = idle;
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
                    if (hit.distance < 10f && timer < 3f)
                    {
                        timer = 3f;
                    }
                    else if (hit.distance < 20f && timer < 2f)
                    {
                        timer = 2f;
                    }
                    else if (hit.distance < 30f && timer < 1f)
                    {
                        timer = 1f;
                    }

                    if (alert == true && timer < 2.5f)
                    {
                        timer = 2.5f;
                    }

                    Debug.Log(hit.distance);
                    playerVisible = true;
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
            animator.runtimeAnimatorController = running;

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
                animator.runtimeAnimatorController = idle;
                //TODO return to patorl or guard point
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
            if (finalPosition != null)
            {
                transform.LookAt(finalPosition);
                guardNav.destination = finalPosition;
                animator.runtimeAnimatorController = running;
            }
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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerSeen = false;
        }
    }

    public void HitPsyWall()
    {
        alert = true;
    }
}
