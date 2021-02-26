using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    
    //References to the UI elements
    [SerializeField] TextMeshProUGUI left;
    [SerializeField] TextMeshProUGUI right;
    [SerializeField] TextMeshProUGUI instructions;

    [SerializeField] Button readyButton;

    
    //How long to wait until the button is visible/active on screen - basically after both instructions are displayed.
    [SerializeField] float buttonActivationTime = 7;
     
    //how long the fade out will take
    [SerializeField] float fadeTime = 2;
    //how long until the jump scare is started, -1 means it will be random between 1 and 3 seconds.
    [SerializeField] float jumpScareTime = -1;

    //timers for the fade out and jumpscare
    float elapsedTime = 0;
    float buttonElapsedTime = 0;
    float jumpScareElapsedTime = 0;
   

    //flags for knowing what phase we are in
    bool fadeStarted = false;
    bool readyButtonVisible = false;
    bool jumpScareCountDownStarted = false;
    
    //will be the alpha value of the text color.  It will fade from 1 (visible) to 0 (not visible)
    float alpha = 1;

    //This action will notify the zombie when to appear
    public static Action onJumpScare;


    // Start is called before the first frame update
    void Start()
    {
        //turn off the button at start
        readyButton.gameObject.SetActive(false);
        readyButtonVisible = false;

        //if jump scare time is set to -1 in inspector then we will generate a random start time between 1 and 3 seconds.
        //otherwise just use the specified time.
        if (jumpScareTime == -1)
            jumpScareTime = UnityEngine.Random.Range(1, 3);

    }

    // Update is called once per frame
    void Update()
    {
        //start the timing of when to display the button
        buttonElapsedTime += Time.deltaTime;
        
        if(buttonElapsedTime > buttonActivationTime)
        {
            if (!readyButtonVisible)
            {
                readyButton.gameObject.SetActive(true);
                readyButtonVisible = true;
            }
        }
        
        
        if (fadeStarted)
        {
            //start fading the color of the text over the course of the amount of time specified in inspector by fade time
            //once the fade is finished (when elapsed time is greater than fade time we will start the jumpscarecountdown
            if (elapsedTime < fadeTime)
            {
                alpha = Mathf.Lerp(1,0, elapsedTime / fadeTime);
                left.color = new Color(left.color.r, left.color.g, left.color.b, alpha);
                right.color = new Color(right.color.r, right.color.g, right.color.b, alpha);
                instructions.color = new Color(instructions.color.r, instructions.color.g, instructions.color.b, alpha);
                elapsedTime += Time.deltaTime;
            }
            else
            {
               
                jumpScareCountDownStarted = true;
             
            }
            //Debug.Log("Alpha: " + alpha.ToString());
           
        }

        if(jumpScareCountDownStarted)
        {
            if(jumpScareElapsedTime < jumpScareTime)
            {
                jumpScareElapsedTime += Time.deltaTime;
                Debug.Log("Elapsed Time: " + elapsedTime.ToString());
            }
            else
            {
                //once the jump scare timer is complete (when jumpscareElapsedTime is greater than jumpScareTime)
                //we initiate the jumpscare action which is listened for by the zombie jumpscare script
                if(onJumpScare != null)
                {
                    onJumpScare();
                }
            }
        }
       


    }

    
    //the following prcedure will activate when the ready button is pushed
    //It will start the text fade and disable the button
    public void ReadyButtonPushed()
    {
        fadeStarted = true;

       readyButton.gameObject.SetActive(false);
       

        
    }

}
