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

    [SerializeField]
    TextMeshProUGUI timeText;

    private float turnEndTime;
    

    private int turnsMade = 0;

    private bool playerTurning = false;
    private bool debugOption = false;

    private TurnEnum playersTurnChoice = TurnEnum.STRAIGHT;

    private TurnEnum correctTurn = TurnEnum.STRAIGHT;

    public static Action ProperTurnMade;
    public GameObject deathMonster;
    public GameObject taxi;
    //public static Action WrongTurn;

   
    
    // Start is called before the first frame update
    void Start()
    {
        playerTurnText.text = string.Empty;

        turnsMadeText.text = "Intersections: 0";

        TaxiController.EnteringIntersection += UpdateTurnText;

        timeText.text = string.Empty;
        timeText.gameObject.SetActive(debugOption);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            debugOption = !debugOption;

            timeText.gameObject.SetActive(debugOption);
        }
        
        
        if (playerTurning)
        {
            if (Time.time < turnEndTime)
            {
                //Debug.Log("Time / End Time:  " + Time.time.ToString() + "-------" +  turnEndTime.ToString());
                //check if time

                timeText.text = (turnEndTime - Time.time).ToString();

                if (Input.GetKeyDown(KeyCode.Z) || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
                {
                    playersTurnChoice = TurnEnum.LEFT;
                    playerTurnText.text = string.Empty;
                    playerTurning = false;
                    CheckIfTurnIsCorrect();
                    Debug.Log("Player chose Left");
                }
                else if (Input.GetKeyDown(KeyCode.X) || Input.deviceOrientation == DeviceOrientation.Portrait) 
                {
                    playersTurnChoice = TurnEnum.RIGHT;
                    playerTurnText.text = string.Empty;
                    playerTurning = false;
                    CheckIfTurnIsCorrect();
                    Debug.Log("Player chose Right");
                }

             
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
            turnsMade++;
            turnsMadeText.text = String.Format("Intersections: {0}", turnsMade);
        }
        else
        {
            TaxiController.liveGame = false;

            //GameObject monster = Instantiate(deathMonster, new Vector3(0, 0, 0), Quaternion.identity);
            GameObject monster = Instantiate(deathMonster, taxi.transform.position, Quaternion.identity, taxi.transform);
            monster.transform.rotation = taxi.transform.rotation;
            Vector3 worldToLocal = taxi.transform.InverseTransformVector(0, -2, -2.2f);
            monster.transform.position = monster.transform.position + worldToLocal;
            //Vector3 localForward = 2.2f * taxi.transform.forward;
            //monster.transform.position = new Vector3(taxi.transform.position.x + localForward.x, taxi.transform.position.y - 2.2f + localForward.y, taxi.transform.position.z + localForward.z);
            monster.transform.Rotate(0, 180, 0);
            //playerTurnText.text = "GAME OVER";

            TaxiController.liveGame = false;

            //Time.timeScale = 0;

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

    public void TimerButtonPushed()
    {
        debugOption = !debugOption;

        timeText.gameObject.SetActive(debugOption);
    }
}
