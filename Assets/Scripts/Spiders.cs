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
    private GameObject jumpDestination;

    [SerializeField]
    private GameObject originalPos;

    [SerializeField]
    private float maxMove = 0.5f;

    [SerializeField]
    private bool hasLanded = false;

    [SerializeField]
    private bool attackPlayer = false;

    private bool hasAttacked = false;

    private Animator animator;

    private bool jumping = false;

    private bool hasJumped = false;

    private bool hasReachedDestination = false;

    [SerializeField]
    private float jumpSpeed = 2;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        

    }

    // Update is called once per frame
    void Update()
    {
        if (attackPlayer)
        {
            if (hasLanded)
            {
                float step = maxMove * Time.deltaTime;
              //  transform.position = Vector3.MoveTowards(transform.position, endPos.transform.position, step);

              //  if ((Vector3.Distance(transform.position, endPos.transform.position) < 0.1f))
              //  {
                  //  HasReachedDestination();
               // }
                if (!hasReachedDestination)
                {
                    //animator.SetBool("ReachedDestination", true);
                    // hasReachedDestination = true;
                    transform.position = Vector3.MoveTowards(transform.position, endPos.transform.position, step);

                      if ((Vector3.Distance(transform.position, endPos.transform.position) < 0.1f))
                      {
                      HasReachedDestination();
                     }

                }
                else
                {
                    animator.SetBool("ReachedDestination", true);
                    hasReachedDestination = true;

                    if (hasAttacked)
                    { 
                    
                      if(hasJumped)
                        {
                            float jumpStep = jumpSpeed * Time.deltaTime;
                            transform.position = Vector3.MoveTowards(transform.position, jumpDestination.transform.position, jumpStep);

                            if(Vector3.Distance(transform.position, jumpDestination.transform.position) < 0.1)
                            {
                                ResetSpider();
                            }

                        }
                    }
                    else
                    {
                        animator.SetBool("hasAttacked", true);
                    }

                }
            
            }
            else
            {
                float step = maxMove * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, endPos.transform.position, step);
            }

            if (Vector3.Distance(transform.position, endPos.transform.position) < 0.1)
            {
                hasLanded = true;
                hasReachedDestination = true;
            }

        }
        else
        {
            if (hasLanded)
            {
                float step = maxMove * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, endPos.transform.position, step);

                if (Vector3.Distance(transform.position, endPos.transform.position) < 0.1)
                {
                    ResetSpider();
                }

            }
            else
            {
                float step = maxMove * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, landingPos.transform.position, step);
            }

            if (Vector3.Distance(transform.position, landingPos.transform.position) < 0.1)
            {
                hasLanded = true;
            }
        }

    }

    public void SpiderJump()
    {
        hasJumped = true;
    }

    public void HasReachedDestination()
    {
        hasReachedDestination = true;
    }

    public void SetHasAttacked()
    {
        hasAttacked = true;
        
       // jumping = true;
    }

    private void ResetSpider()
    {
        hasJumped = false;
        hasReachedDestination = false;
        hasLanded = false;
        hasAttacked = false;
        
        transform.position = originalPos.transform.position;
        gameObject.SetActive(false);
        
    }
}
