using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI playerTurnText;

    [SerializeField]
    TextMeshProUGUI turnsMadeText;

    //[SerializeField]
    private float maxTimeForTurn = 2.5f;

    [SerializeField]
    TextMeshProUGUI timeText;

    [SerializeField]
    private AudioSource music;
    
    
    private float turnEndTime;



    private int turnsMade = 0;

    private bool playerTurning = false;
    private bool debugOption = false;

    private TurnEnum playersTurnChoice = TurnEnum.STRAIGHT;

    private TurnEnum correctTurn = TurnEnum.STRAIGHT;

    public static Action ProperTurnMade;
    public static Action PlayerDeath;
    public static Action<float> LevelUp;

    public GameObject deathMonster;
    public GameObject deathMonster2;
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

        maxTimeForTurn = GameManager.instance.GetNextLevelTime();

       
        music.volume = GameManager.instance.GetVolume();

        

    }

    // Update is called once per frame
    void Update()
    {
       ///////Debugging purposes only///////////////////////////
        if (Input.GetKeyDown(KeyCode.D))
        {
            debugOption = !debugOption;//////////////////////////////

            timeText.gameObject.SetActive(debugOption);
        }

        if(Input.GetKeyDown(KeyCode.L))///////////////////////////////////
        {
            Debug.LogError("Level UP!");
            GameManager.instance.IncrementCurrentLevel();
            maxTimeForTurn = GameManager.instance.GetNextLevelTime();//////////////////////////////

            if (LevelUp != null)
            {
                LevelUp(GameManager.instance.GetNextLevelSpeed());////////////////////////h
            }
        }
        ////////////////////////////////////////////////////////////////////

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

        if (correctTurn == playersTurnChoice)
        {
            if (ProperTurnMade != null)
                ProperTurnMade();
            turnsMade++;
            turnsMadeText.text = String.Format("Intersections: {0}", turnsMade);
            CheckForLevelIncrease();
        }
        else
        {
            TaxiController.liveGame = false;


            StartCoroutine(CreateMonster());
            TaxiController.liveGame = false;

            //This action will tell the window zombie not to reappear during the death sequence
            if (PlayerDeath != null)
                PlayerDeath();

        }

        // correctTurn = null;

    }

    private void CheckForLevelIncrease()
    {
        if (turnsMade % GameManager.instance.GetIntersectionsBeforeIncrease() == 0)
        {
            //level up
            Debug.LogError("Level UP!");
            GameManager.instance.IncrementCurrentLevel();
            maxTimeForTurn = GameManager.instance.GetNextLevelTime();

            if (LevelUp != null)
            {
                LevelUp(GameManager.instance.GetNextLevelSpeed());
            }
        }
    }

        

    IEnumerator CreateMonster()
    {

        yield return new WaitForSeconds(3);
        System.Random generator = new System.Random();
        int val = generator.Next(0, 50);
        if (val > 24)
        {
            GameObject monster = Instantiate(deathMonster, taxi.transform.position, Quaternion.identity, taxi.transform);
            monster.transform.rotation = taxi.transform.rotation;
            //Vector3 worldToLocal = taxi.transform.InverseTransformVector(0, -2, -2.2f);
            Vector3 worldToLocal = taxi.transform.TransformDirection(new Vector3(0.3f, -2, 2.2f));
            monster.transform.position = monster.transform.position + worldToLocal;

            monster.transform.Rotate(0, 180, 0);
        }
        else
        {
            GameObject monster = Instantiate(deathMonster2, taxi.transform.position, Quaternion.identity, taxi.transform);
            monster.transform.rotation = taxi.transform.rotation;
            // Vector3 worldToLocal = taxi.transform.InverseTransformVector(-0.7f, -1.55f, -3.67f);
            Vector3 worldToLocal = taxi.transform.TransformDirection(new Vector3(0.47f, -1.55f, 3.67f));
            monster.transform.position = monster.transform.position + worldToLocal;

            monster.transform.Rotate(0, 180, 0);
        }
        yield return new WaitForSeconds(2f);


        music.Stop();
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        SceneManager.LoadScene(0);
        GameManager.instance.GetAudio().Play();
    }


    void UpdateTurnText(TurnEnum nextTurn)
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

