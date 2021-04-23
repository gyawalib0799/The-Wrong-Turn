using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowZombies : MonoBehaviour
{
    [SerializeField] float onScreenTime = 3;

    [SerializeField] float minRespawnTime = 8;
    [SerializeField] float maxRespawnTime = 20;

    [SerializeField] GameObject[] targetPos;

    [SerializeField] float creepSpeed = 1;

    [SerializeField] GameObject startingPos;

    private bool zombieLowering = false;
    private bool zombieRaising = false;

    private float timeUntilMove;

    private int targetIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
       // startingPos = transform.localPosition;

        //calcualte a random spawn time
        timeUntilMove = UnityEngine.Random.Range(minRespawnTime, maxRespawnTime);

        //add that time to the current time to get the future move time for the zombie
        timeUntilMove += Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeUntilMove)
        {
            StartCoroutine(MoveZombie());

            timeUntilMove = Mathf.Infinity;

        }
        if (zombieLowering)
        {
            LowerZombie();
        }
        else if (zombieRaising)
        {
           RaiseZombie();
        }
        
    }

    IEnumerator MoveZombie()
    {
        zombieLowering = true;

        targetIndex = UnityEngine.Random.Range(0, targetPos.Length);

        yield return new WaitForSeconds(onScreenTime);
      
        zombieRaising = true;

    }


    void LowerZombie()
    {
        
        
        if (Vector3.Distance(transform.position, targetPos[targetIndex].transform.position) < 0.01)
        {
            zombieLowering = false;

            
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos[targetIndex].transform.position, creepSpeed * Time.deltaTime);
        }

    }
    void RaiseZombie()
    {

        Debug.LogError("Raising Zombie");

        //Debug.LogError("Distance: " + Vector3.Distance(transform.position, targetPos.transform.position).ToString());

        if (Vector3.Distance(transform.position, startingPos.transform.position) < 0.01)
        {
            zombieRaising = false;
            //reset our timer after the zombie move back up
            //calcualte a random spawn time
            timeUntilMove = UnityEngine.Random.Range(minRespawnTime, maxRespawnTime);

            //add that time to the current time to get the future move time for the zombie
            timeUntilMove += Time.time;


        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPos.transform.position, creepSpeed * Time.deltaTime);
        }


    }

}
