using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TaxiController : MonoBehaviour
{
    int curWP;
    int previousWP;
    public static bool liveGame;

   [SerializeField] float rotSpeed = 1.4f;
   [SerializeField]  float speed = 18f;
    [SerializeField] float accuracyWP = 7.0f;

    bool lastWaypoint = false;

    bool guidanceEnabled = true;

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
        liveGame = true;
        currentPath = GameObject.FindGameObjectWithTag("StartingPath");
        
        wayPoints = currentPath.GetComponentsInChildren<Transform>();

        Debug.Log("waypoints length: " + wayPoints.Length.ToString());

        curWP = 1;
        

        layerMask = LayerMask.GetMask("Terrain");

        rb = GetComponent<Rigidbody>();

       // UIManager.WrongTurn += ProcessGameOver;
        UIManager.ProperTurnMade += TurnComplete;
        UIManager.LevelUp += LevelUp;


        speed = GameManager.instance.GetNextLevelSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!liveGame)
        {
            this.transform.Translate(0, 0, 0);
            StartCoroutine(LeadToDeath());
            guidanceEnabled = false;
            Vector3 direction = wayPoints[curWP].transform.position - transform.position;


            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.fixedDeltaTime);

        }

        if (guidanceEnabled)
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

               // rb.freezeRotation = false;
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.fixedDeltaTime);
               // rb.freezeRotation = true;
            }
        }
        this.transform.Translate(0, 0, Time.deltaTime * speed);


        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, -transform.up, out hitInfo, Mathf.Infinity, layerMask))
        {
            //transform.up = hitInfo.normal;
            //rb.freezeRotation = false;
          //  transform.up -= (transform.up - hitInfo.normal) * 0.1f;
           // rb.freezeRotation = true;

        }





    }

    IEnumerator LeadToDeath()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        yield return new WaitForSeconds(1);
        Vector3 direction = this.transform.right;
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        this.enabled = false;

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Intersection"))
        {
            Debug.Log("------------Entering Intersection-------Ready for Turn");

            currentIntersection = other.gameObject.GetComponent<Intersection>();

            StartCoroutine(DisableColliders(other));

            PickTurnDirection();

            guidanceEnabled = false;

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
        Debug.Log("Next turn: " + nextTurnInt.ToString());

        /*
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
      */
       // Debug.Log("Next turn: " + nextTurnInt.ToString());
        
        nextTurn = currentIntersection.GetTurn(nextTurnInt);

        if (EnteringIntersection != null)
        {
            //
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

    //adjust speed only it should receive a speed;
    void LevelUp(float nextSpeed)
    {
        //Level rot speed time
        //  1    1.6  18  2.5
        //  2    1.8, 20  2.4
        //  3    2.0, 22  2.2
        //  4    2.2, 25  2.0
        //  5    2.4, 27  1.8
        //  6    2.6, 29  1.6
        // 7    2.8, 31  1.4
        // 8    3.0, 33  1.2
        // 9    3.2  35  1.1
        // 10   3.4  37  1.0

        //GameManager.instance.AddLevel(); this should be done in ui Manager

        speed = nextSpeed;

    }

    void TurnComplete()
    {
        guidanceEnabled = true;
        Debug.Log("Guidance Enabled");
    }

}