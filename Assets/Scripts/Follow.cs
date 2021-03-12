using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform taxi;

    public GameObject[] wayPoints;
    int curWP;
    float rotSpeed = 0.6f;
    float speed = 10.5f;
    float accuracyWP = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
