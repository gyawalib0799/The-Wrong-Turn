using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class JumpScare : MonoBehaviour
{
    //allows setting how long the zombie appears on screen for in the inspector
    [SerializeField] float viewTime;

    //keeps track of the amount of time the zombie is on screen
    float elapsedTime = 0;
    
    //can we see the zombie?
    bool zombieVisible = false;
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        //upon script start we will hide the zombie and subscribe to the jumpscare action declared in the fadeout script
        gameObject.SetActive(false);

        FadeOut.onJumpScare += Scare;
    }

    // Update is called once per frame
    void Update()
    {
        if(zombieVisible)
        {
            //if zombie is visible we will make sure it only stays visible for specified time
            if(elapsedTime < viewTime)
            {
                elapsedTime += Time.deltaTime;
            }
            else
            {
                //once the time has passed we will make the zombie invisible again and load the next scene.
                zombieVisible = false;
                gameObject.SetActive(false);
                LoadGame();
            }
        }
    }

    //loads the next scene in the build index
    private void LoadGame()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;

        sceneNumber += 1;

        SceneManager.LoadScene(sceneNumber);
  
    }

    void Scare()
    {
        //starts the jump scare, the sound is set to play on awake in the inspector and will start automatically
        gameObject.SetActive(true);
        zombieVisible = true;
    }

}
