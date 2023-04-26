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

    private float attackR = 50f;
    private float timer = 0f;
    private float countDownToIdle = 0f;

    private NavMeshAgent guardNav;

    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    private bool playerSeen = false;
       
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
        
        float _distTo = Vector3.Distance(head.position, target.position);

        if (_distTo <= attackR)
        {
            timer += Time.deltaTime;
            transform.LookAt(target);
        }
        if (timer > 3f)
        {
            raycastDirection = (target.position - head.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(head.position,  raycastDirection, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(head.position, raycastDirection * hit.distance, Color.red);
                if (hit.collider.CompareTag("Player"))
                {
                    playerSeen = true;
                    playerLastLocation = target.position;
                    Debug.DrawRay(head.position, raycastDirection * hit.distance, Color.green);
                }

                else if (!hit.collider.CompareTag("Player") && playerSeen)
                {
                    playerSeen = false;
                }
            }

            if (playerSeen)
            {
                transform.LookAt(target);

                Vector3 _moveTo = target.position;
                guardNav.destination = _moveTo;
            }
            else if (!playerSeen)
            {
                

                if (playerLastLocation != null)
                {
                    transform.LookAt(playerLastLocation);
                    Vector3 _moveTo =  playerLastLocation;
                    guardNav.destination = _moveTo;

                    if (guardNav.destination == null)
                    {
                        countDownToIdle += Time.deltaTime;
                    }
                }
                    
                
                

                if (countDownToIdle >= 5f)
                {
                    countDownToIdle = 0f;
                    timer = 0f;
                }
            }
               
        }
        


        //if (fakeTarget != null)
        //{
        //    transform.LookAt(fakeTarget);

        //    Vector3 _moveTo = Vector3.MoveTowards(transform.position, fakeTarget.position, 10000f);
        //    guardNav.destination = _moveTo;
        //}

        // PatrolArea();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("FakePlayer"))
    //    {
    //        fakeTarget = other.gameObject.GetComponent<Transform>();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("FakePlayer"))
    //    {
    //        fakeTarget = null;
    //        countDownToIdle += Time.deltaTime;
    //    }
    //}
    private void PatrolArea()
    {
        if (playerSeen == false)
        {
            //TODO patrol + return to current patrol point
        }
        
    }
}
