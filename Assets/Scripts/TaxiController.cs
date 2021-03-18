using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiController : MonoBehaviour
{
    [SerializeField] GameObject[] route;
    
    
    public Transform taxi;

    public GameObject[] wayPoints;
    int curWP;
    float rotSpeed = 1.4f;
    float speed = 15.5f;
    float accuracyWP = 5.0f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] waypoint = GetComponentsInChildren<Transform>();

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
                curWP = 0;
            }
        }

        Vector3 direction = wayPoints[curWP].transform.position - transform.position;
        rb.freezeRotation = false;
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            transform.up = hit.normal;
        }

         rb.freezeRotation = true;
          this.transform.Translate(0, 0, Time.deltaTime * speed);
      //  rb.velocity = transform.forward * speed * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision other)
    {
       // if(other.gameObject.tag == "Ground")
         //   Debug.Log("Ground Normal" + other.GetContact()
    }
}

