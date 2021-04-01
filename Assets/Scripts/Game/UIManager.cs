using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI playerTurnText;

    [SerializeField]
    TextMeshProUGUI turnsMadeText;

    [SerializeField]
    private float maxTimeForTurn = 1.5f;

    private float turnEndTime;
    

    private int turnsMade = 0;

    private bool playerTurning = false;

    private TurnEnum playersTurnChoice = TurnEnum.STRAIGHT;

    private TurnEnum correctTurn = TurnEnum.STRAIGHT;

    public static Action ProperTurnMade;
    public static Action WrongTurn;


    
    // Start is called before the first frame update
    void Start()
    {
        playerTurnText.text = string.Empty;

        turnsMadeText.text = "Turns: 0";

        TaxiController.EnteringIntersection += UpdateTurnText;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurning)
        {
            if (Time.time < turnEndTime)
            {
              //  Debug.Log("Time / End Time:  " + Time.time.ToString() + "-------" +  turnEndTime.ToString());
                //check if time
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    playersTurnChoice = TurnEnum.LEFT;
                    playerTurnText.text = string.Empty;
                    playerTurning = false;
                    CheckIfTurnIsCorrect();
                    Debug.Log("Player chose Left");
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    playersTurnChoice = TurnEnum.RIGHT;
                    playerTurnText.text = string.Empty;
                    playerTurning = false;
                    CheckIfTurnIsCorrect();
                    Debug.Log("Player chose Right");
                }

               // turnEndTime += Time.deltaTime;
               // else
               // {
               //     playersTurnChoice = TurnEnum.STRAIGHT;
               //     playerTurning = false;
               //     CheckIfTurnIsCorrect();
                //}
            }
            else
            {
                playerTurning = false;
                CheckIfTurnIsCorrect();
                Debug.Log("Player going straight");
            }
        }

    }
    
    private void CheckIfTurnIsCorrect()
    {
        Debug.LogError("Next Turn: " + correctTurn.ToString() + " playerTurn: " + playersTurnChoice.ToString());
        
        if(correctTurn == playersTurnChoice)
        {
            if (ProperTurnMade != null)
                ProperTurnMade();
        }
        else
        {
            if (WrongTurn != null)
                WrongTurn();
        }

       // correctTurn = null;

    }
    private void UpdateTurnText(TurnEnum nextTurn)
    {
        playersTurnChoice = TurnEnum.STRAIGHT; //set a default choice
        correctTurn = nextTurn;
        
        if (nextTurn == TurnEnum.LEFT)
        {
            playerTurnText.text = "RIGHT !!!";
        }
        else if (nextTurn == TurnEnum.RIGHT)
        {
            playerTurnText.text = "LEFT !!!";
        }
        else
        {
            playerTurnText.text = string.Empty;
        }

        playerTurning = true;

        turnEndTime = Time.time + maxTimeForTurn;
      //  Debug.LogError("-Turn End Time-" + turnEndTime.ToString());
    }
}
