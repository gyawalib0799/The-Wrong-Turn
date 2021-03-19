using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TaxiController : MonoBehaviour
{
    int curWP;
    float rotSpeed = 1.4f;
    float speed = 15.5f;
    float accuracyWP = 5.0f;

    [SerializeField] Transform[] wayPoints;

    //[SerializeField] GameObject[] paths;

    GameObject currentPath;

    LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        currentPath = GameObject.FindGameObjectWithTag("StartingPath");
        
        wayPoints = currentPath.GetComponentsInChildren<Transform>();

        Debug.Log("waypoints length: " + wayPoints.Length.ToString());

        curWP = 1;

        layerMask = LayerMask.GetMask("Terrain");
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(wayPoints[curWP].transform.position, transform.position) < accuracyWP)
        {
            curWP++;
            if (curWP >= wayPoints.Length)
            {
                curWP = 0;
            }
        }

        Vector3 direction = wayPoints[curWP].transform.position - transform.position;
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        this.transform.Translate(0, 0, Time.deltaTime * speed);

        RaycastHit hitInfo;

        if(Physics.Raycast( transform.position, -transform.up, out hitInfo, Mathf.Infinity,layerMask))
        {
            //transform.up = hitInfo.normal;
            transform.up -= (transform.up - hitInfo.normal) * 0.1f;

        }

      //  this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);



       // this.transform.Translate(0, 0, Time.deltaTime * speed);


    }



}