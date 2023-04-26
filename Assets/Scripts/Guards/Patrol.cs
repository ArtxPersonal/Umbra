using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform target;
    public GameObject fakeTarget;


    private float attackR = 30f;
    private float timer = 0f;
    private float countDownToIdle = 0f;

    private NavMeshAgent guardNav;

    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    private int currentPoint;

    private bool playerSeen = false;
    private bool fakePlayerSeen = false;

    
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
        RaycastHit hit;
        float _distTo = Vector3.Distance(transform.position, target.position);
        float _distToFake = Vector3.Distance(transform.position, fakeTarget.transform.position);

        if (_distToFake <= attackR)
        {
            timer += Time.deltaTime;

            if (timer > 3f)
            {
                if (Physics.Raycast(transform.position, fakeTarget.transform.position, out hit))
                {

                    if (hit.collider.CompareTag("FakePlayer"))
                    {
                        fakePlayerSeen = true;
                        Debug.Log("Fake Player Seen");
                    }

                    else if (!hit.collider.CompareTag("FakePlayer"))
                    {
                        fakePlayerSeen = false;
                    }
                }

                if (fakePlayerSeen && !playerSeen)
                {
                    transform.LookAt(fakeTarget.transform);

                    Vector3 _moveTo = Vector3.MoveTowards(transform.position, fakeTarget.transform.position, 30f);
                    guardNav.destination = _moveTo;
                }
                else if (!fakePlayerSeen && !playerSeen)
                {
                    transform.LookAt(fakeTarget.transform);

                    Vector3 _moveTo = Vector3.MoveTowards(transform.position, fakeTarget.transform.position, 30f);
                    guardNav.destination = _moveTo;

                    countDownToIdle += Time.deltaTime;

                    if (countDownToIdle >= 5f)
                    {
                        countDownToIdle = 0f;
                        timer = 0f;
                    }
                }
            }
        }

        if (_distTo <= attackR)
        {
            timer += Time.deltaTime;

            if (timer > 3f)
            {

                if (Physics.Raycast(transform.position, target.position, out hit))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        playerSeen = true;
                    }

                    else if (!hit.collider.CompareTag("Player") && playerSeen)
                    {
                        playerSeen = false;
                    }
                }

                if (playerSeen)
                {
                    transform.LookAt(target);

                    Vector3 _moveTo = Vector3.MoveTowards(transform.position, target.position, 30f);
                    guardNav.destination = _moveTo;
                }
                else if (!playerSeen)
                {
                    transform.LookAt(target);

                    Vector3 _moveTo = Vector3.MoveTowards(transform.position, target.position, 30f);
                    guardNav.destination = _moveTo;

                    countDownToIdle += Time.deltaTime;

                    if (countDownToIdle >= 5f)
                    {
                        countDownToIdle = 0f;
                        timer = 0f;
                    }
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
