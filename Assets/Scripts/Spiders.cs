using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiders : MonoBehaviour
{

    [SerializeField]
    private GameObject landingPos;

    [SerializeField]
    private GameObject endPos;

    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private float maxMove = 0.5f;

    private bool hasLanded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLanded)
        {
            float step = maxMove * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPos.transform.position, step);
        }
        else
        {
            float step = maxMove * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, landingPos.transform.position, step);
        }

        if(Vector3.Distance(transform.position,landingPos.transform.position)<0.1)
        {
            hasLanded = true;
        }

    }
}
