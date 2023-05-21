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

    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    private bool playerSeen = false;
    private bool playerNoticed = false;
    private bool playerLost = true;
    private bool startTimer = false;
       
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

        //float _distTo = Vector3.Distance(head.position, target.position);


        if (playerSeen || playerNoticed)
        {
            raycastDirection = (target.position - head.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(head.position, raycastDirection, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Player"))
                {
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
                    if (lostTimer >= 3f)
                    {
                        playerLost = true;
                    }
                }
            }
        }


        if (playerNoticed)
        {
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
        else if (!playerNoticed)
        {
            countDownToIdle += Time.deltaTime;

            if (countDownToIdle >= 15f)
            {
                countDownToIdle = 0f;
                timer = 0f;
            }

            // PatrolArea();
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
}
