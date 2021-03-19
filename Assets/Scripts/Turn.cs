using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
 
    
    
    
    
    /*  enum TurnDirection { NONE, LEFT, RIGHT };

    [SerializeField] public GameObject left { get; }
    [SerializeField] public GameObject right { get; }

    bool turnPending = false;

    TurnDirection nextTurn = TurnDirection.NONE;

    // Start is called before the first frame update
    void Start()
    {
       // TaxiController.ApproachingTurn += GenerateNextTurn;
    }

    // Update is called once per frame
    void Update()
    {
        if(turnPending)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(nextTurn == TurnDirection.RIGHT)
                {
                    Debug.Log("Correct");
                    turnPending = false;
                }
                else 
                {
                    Debug.Log("GAME OVER");
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(nextTurn == TurnDirection.LEFT)
                {
                    Debug.Log("Correct");
                    turnPending = false;
                }
                else
                {
                    Debug.Log("GAME OVER");
                }
            }


        }
    }

    void GenerateNextTurn()
    {
        int randomTurn = Random.Range(0, 2);

        if(randomTurn == 0)
        {
            Debug.LogError("TurnLeft");
           // nextTurn = TurnDirection.left;
        }
        else
        {
            Debug.LogError("TurnRight");
           // nextTurn = TurnDirection.right;
        }

        turnPending = true;
    }*/
}
