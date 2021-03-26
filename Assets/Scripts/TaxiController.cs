using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TaxiController : MonoBehaviour
{
    int curWP;
    int previousWP;

    float rotSpeed = 1.4f;
    float speed = 15.5f;
    float accuracyWP = 5.0f;

    bool lastWaypoint = false;

    

    [SerializeField] Transform[] wayPoints;

    [SerializeField] float colliderDisableTime = 10;

    //[SerializeField] GameObject[] paths;

    GameObject currentPath;

    LayerMask layerMask;

    TurnEnum nextTurn;

    Intersection currentIntersection;

    GameObject intersection;

    Rigidbody rb;

    //This Action delegate will be raised when the car is entering the intersection
    public static Action<TurnEnum> EnteringIntersection;
    
    // Start is called before the first frame update
    void Start()
    {
        currentPath = GameObject.FindGameObjectWithTag("StartingPath");
        
        wayPoints = currentPath.GetComponentsInChildren<Transform>();

        Debug.Log("waypoints length: " + wayPoints.Length.ToString());

        curWP = 1;
        

        layerMask = LayerMask.GetMask("Terrain");

        rb = GetComponent<Rigidbody>();

        
      
    }

    // Update is called once per frame
    void Update()
    {


        if (Vector3.Distance(wayPoints[curWP].transform.position, transform.position) < accuracyWP)
        {
            curWP++;
            if (curWP >= wayPoints.Length)
            {
                curWP = 1;
                lastWaypoint = true;
            }
        }

        if (!lastWaypoint)
        {
            Vector3 direction = wayPoints[curWP].transform.position - transform.position;


            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
            
        }
        
        this.transform.Translate(0, 0, Time.deltaTime * speed);


        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, -transform.up, out hitInfo, Mathf.Infinity, layerMask))
        {
            //transform.up = hitInfo.normal;
            transform.up -= (transform.up - hitInfo.normal) * 0.1f;

        }





    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Intersection"))
        {
            Debug.Log("------------Entering Intersection-------Ready for Turn");

            currentIntersection = other.gameObject.GetComponent<Intersection>();

            StartCoroutine(DisableColliders(other));

            PickTurnDirection();

        }
    }


    void PickTurnDirection()
    {/*
        if (currentIntersection.GetTurnCount() == 1)
        {
            currentPath = currentIntersection.GetPath(TurnEnum.STRAIGHT);
            wayPoints = currentPath.GetComponentsInChildren<Transform>();

            curWP = 1;

            Debug.Log("Next Path: " + currentPath.gameObject.name);

            return;
        }
        */
        int nextTurnInt;
        
        nextTurnInt = Random.Range(0, currentIntersection.GetTurnCount());
        int turnCap = currentIntersection.GetTurnCount();

        int leftTurn = 0;
        int rightTurn = 0;
        int straightTurn = 0;

        if (turnCap > 1)
        {
            for (int i = 0; i < turnCap; i++)
            {
                int val = (int) currentIntersection.turns[i].turn;
                switch (val)
                {
                    case 0:
                        leftTurn = i;
                        break;
                    case 1:
                        rightTurn = i;
                        break;
                    case 2:
                        straightTurn = i;
                        break;
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                nextTurnInt = leftTurn;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                nextTurnInt = rightTurn;
            }
            else
            {
                nextTurnInt = straightTurn;
            }

        }

        Debug.Log("Next turn: " + nextTurnInt.ToString());

        if (EnteringIntersection != null)
        {
            EnteringIntersection(nextTurn);
        }

       
        
        currentPath = currentIntersection.GetPath(nextTurnInt);

        wayPoints = currentPath.GetComponentsInChildren<Transform>();

        curWP = 1;

        lastWaypoint = false;

        Debug.Log("Next Path: " + currentPath.gameObject.name);
    }


    //need to shut down colliders for x seconds and then re-enable after entering one.
    IEnumerator DisableColliders(Collider other)
    {
        //Transform intersectionTop = other.gameObject.GetComponentInParent<Transform>();

        Transform intersectionTop = other.gameObject.transform.parent;

        Collider[] children = intersectionTop.GetComponentsInChildren<Collider>();

        Debug.Log("Colliders Detected: " + children.Length.ToString());

        foreach (Collider child in children)
            child.gameObject.SetActive(false);

        yield return new WaitForSeconds(colliderDisableTime);


        foreach (Collider child in children)
            child.gameObject.SetActive(true) ;

    }

}