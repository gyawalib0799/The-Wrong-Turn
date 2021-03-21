using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
 
    
    TurnEnum nextTurn;

    TurnEnum turnChoice;

    bool turnMade = false;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //need a timer in here 
        if (!turnMade)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                turnChoice = TurnEnum.RIGHT;
                turnMade = true;
            }


            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                turnChoice = TurnEnum.LEFT;
                turnMade = true;
            }
        }
            
     
    }

    void ProcessTurn(TurnEnum turn)
    {
        nextTurn = turn;


    }

}
