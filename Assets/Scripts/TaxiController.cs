using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TaxiController : MonoBehaviour
{
    int curWP;
    float rotSpeed = 2;//1.4f;
    float speed = 15.5f;
    float accuracyWP = 5.0f;


    Vector3 currentWaypoint;

    bool guidanceEnabled = true;

    LayerMask layerMask;


    [SerializeField] Transform testWaypoint;

    void Start()
    {
        layerMask = LayerMask.GetMask("Waypoints");
        FindNextWaypoint();
    }

    // Update is called once per frame



    void Update()
    {
        /*
        if (Vector3.Distance(currentWaypoint, transform.position) < accuracyWP)
        {
            FindNextWaypoint();
        }
        */

        if (guidanceEnabled)
        {
            Vector3 direction = currentWaypoint - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        }
        this.transform.Translate(0, 0, Time.deltaTime * speed);


     //   this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);



       // this.transform.Translate(0, 0, Time.deltaTime * speed);

    }

    void FindNextWaypoint()
    {

        RaycastHit hitInfo = new RaycastHit();

        Debug.DrawRay(transform.position, transform.forward * 30,Color.red,5);


       // (Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, float.PositiveInfinity, layerMask);

        if (Physics.Raycast(transform.position,transform.forward * 10, out hitInfo, Mathf.Infinity,layerMask))
        {
            currentWaypoint = hitInfo.collider.gameObject.transform.position;

            Debug.Log("Next Waypoint: " + hitInfo.collider.gameObject.name);

        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Waypoint"))
        {
            guidanceEnabled = false;
        }

        else 
        {/*
            if(other.gameObject.tag.Equals("Turn"))
            {
               // Vector3 turnDirection = testWaypoint.transform.position - transform.position;
               // this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(turnDirection), rotSpeed * Time.deltaTime);
                transform.rotation = Quaternion.AngleAxis(-45, Vector3.up);
                guidanceEnabled = false;
            }*/
        }
    }
/*
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Turn"))
        {
            Vector3 turnDirection = testWaypoint.transform.position - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(turnDirection), rotSpeed * Time.deltaTime);
        }
    }
*/

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Waypoint")) ;
        {
            FindNextWaypoint();
            guidanceEnabled = true;
            Debug.Log("Exitting Collision");
        }
    }









    /* [SerializeField] GameObject[] route;

    int currentPath = 0;

    public Transform taxi;

   // public GameObject[] wayPoints;
    int curWP = 0;
    float rotSpeed = 1.4f;
    float speed = 15.5f;
    float accuracyWP = 5.0f;

    Rigidbody rb;

    Transform[] paths;
    Transform[] waypoints;
    Transform[] upcomingWaypoints;

    bool approachingTurn = false;

    public static Action ApproachingTurn;


    // Start is called before the first frame update
    void Start()
    {

        waypoints = route[currentPath].GetComponentsInChildren<Transform>();    //start at path 0

        curWP = 1;

        Debug.Log("Waypoint Length: " + waypoints.Length.ToString());
        Debug.Log("Waypoint 1 location" + waypoints[1].transform.position.ToString());

       // rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame



    void Update()
    {
        if(waypoints[curWP].tag == "Turn" && !approachingTurn)
        {
            approachingTurn = true;

         /*   if(ApproachingTurn != null)
            {
                ApproachingTurn();
            }*/

    /*       Debug.Log("------------TURN APPROACHING----------");

           GenerateNextTurnData();
       }

       else if (Vector3.Distance(waypoints[curWP].transform.position, transform.position) < accuracyWP)
       {
           if (waypoints[curWP].tag == "Turn")
           {
               waypoints = upcomingWaypoints;
               curWP = 1;
               approachingTurn = false;
           }
           else
           {
               curWP++;
               if (curWP >= waypoints.Length)
               {
                   curWP = 0;
               }
           }
       }

       Vector3 direction = waypoints[curWP].transform.position - transform.position;

       this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
       this.transform.Translate(0, 0, Time.deltaTime * speed);

       Debug.Log("Waypoint 1 location" + waypoints[curWP].gameObject.transform.position.ToString());
   }


   void GenerateNextTurnData()
   {
       GameObject nextPath;

       int randomTurn = Random.Range(0, 2);



       if (randomTurn == 0)
       {
           Debug.LogError("TurnLeft");

           nextPath = waypoints[curWP].GetComponent<Turn>().left;


       }
       else
       {
           Debug.LogError("TurnRight");

           nextPath = waypoints[curWP].GetComponent<Turn>().right;
       }

       upcomingWaypoints = nextPath.GetComponentsInChildren<Transform>();


   }

   //test code below not in use

   // RaycastHit hit;
   // if (Physics.Raycast(transform.position, -transform.up, out hit))
   //  {
   //     transform.up = hit.normal;
   //  }

   //    rb.freezeRotation = true;
   //    this.transform.Translate(0, 0, Time.deltaTime * speed);
   //  rb.velocity = transform.forward * speed * Time.deltaTime;


   */
}

