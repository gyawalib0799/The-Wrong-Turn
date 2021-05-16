using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPathing : MonoBehaviour
{

    public GameObject[] waypoints;
    public float rotSpeed = 0.2f;
    public float speed = 2.5f;
    public float accuracyWP = 5.0f;
    int currentWP;
    // Start is called before the first frame update
    void Start()
    {
        currentWP = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(waypoints[currentWP].transform.position, this.transform.position) < accuracyWP)
        {
            currentWP++;
            if (currentWP >= waypoints.Length)
            {
                currentWP = 0;
            }
        }

        Vector3 direction = waypoints[currentWP].transform.position - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        this.transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
