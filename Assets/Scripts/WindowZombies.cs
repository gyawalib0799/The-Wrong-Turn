using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowZombies : MonoBehaviour
{
    [SerializeField] float onScreenTime = 3;

    [SerializeField] float minRespawnTime = 8;
    [SerializeField] float maxRespawnTime = 20;

    //array of locations the zombie will appear in
    [SerializeField] GameObject[] targetPos;

    //how fast the zombie moves up and down 
    [SerializeField] float creepSpeed = 1;


    [SerializeField] GameObject startingPos;    //for the zombie

    private bool zombieLowering = false;
    private bool zombieRaising = false;

    private float timeUntilMove;

    //There is an array of locations where the zombie will come down to, this is the index into the array
    private int targetIndex = 0;

    private int previousTargetIndex = 0;

    private AudioSource audio;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
       

        //calcualte a random spawn time
        timeUntilMove = UnityEngine.Random.Range(minRespawnTime, maxRespawnTime);

        //add that time to the current time to get the future move time for the zombie
        timeUntilMove += Time.time;

        audio = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();

        animator.enabled = false;

        UIManager.PlayerDeath += TurnOffZombie;
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

        //we want the zombie to appear differently every time
        while (targetIndex == previousTargetIndex)
        {
            targetIndex = UnityEngine.Random.Range(0, targetPos.Length);
        }
    
        
        if (targetIndex == 0)
        {   //this will play the scary sound when the zombie comes down with head and body in middle of view
            animator.enabled = true;
            audio.Play();
        }

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
            animator.enabled = false;

            previousTargetIndex = targetIndex;


        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPos.transform.position, creepSpeed * Time.deltaTime);
        }


    }

    void TurnOffZombie()
    {
        gameObject.SetActive(false);
    }

}
