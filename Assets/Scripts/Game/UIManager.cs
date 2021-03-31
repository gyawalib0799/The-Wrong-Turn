using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI playerTurnText;

    private bool playerTurning = false;

    private TurnEnum playersTurnChoice = TurnEnum.STRAIGHT;

    // Start is called before the first frame update
    void Start()
    {
        playerTurnText.text = string.Empty;

        TaxiController.EnteringIntersection += UpdateTurnText;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurning)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                playersTurnChoice = TurnEnum.LEFT;
                playerTurnText.text = string.Empty;
                playerTurning = false;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                playersTurnChoice = TurnEnum.RIGHT;
                playerTurnText.text = string.Empty;
                playerTurning = false;
            }
            else
            {
                playersTurnChoice = TurnEnum.STRAIGHT;
            }

        }

    }
    private void UpdateTurnText(TurnEnum correctTurn)
    {
        if (correctTurn == TurnEnum.LEFT)
        {
            playerTurnText.text = "RIGHT !!!";
        }
        else if (correctTurn == TurnEnum.RIGHT)
        {
            playerTurnText.text = "LEFT !!!";
        }
        else
        {
            playerTurnText.text = string.Empty;
        }

        playerTurning = true;

    }
}
