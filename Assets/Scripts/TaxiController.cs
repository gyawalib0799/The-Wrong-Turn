using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiController : MonoBehaviour
{
    [SerializeField] GameObject[] route;

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
        if(waypoints[curWP].tag == "Turn")
        {

            Debug.Log("------------TURN APPROACHING----------");
        }
        
        else if (Vector3.Distance(waypoints[curWP].transform.position, transform.position) < accuracyWP)
        {
            curWP++;
            if (curWP >= waypoints.Length)
            {
                curWP = 0;
            }
        }

        Vector3 direction = waypoints[curWP].transform.position - transform.position;
        
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        this.transform.Translate(0, 0, Time.deltaTime * speed);

        Debug.Log("Waypoint 1 location" + waypoints[curWP].gameObject.transform.position.ToString());
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



}

