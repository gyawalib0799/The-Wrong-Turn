using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class JumpScare : MonoBehaviour
{
    [SerializeField] float viewTime;

    float elapsedTime = 0;
    bool zombieVisible = false;
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        FadeOut.onJumpScare += Scare;
    }

    // Update is called once per frame
    void Update()
    {
        if(zombieVisible)
        {
            if(elapsedTime < viewTime)
            {
                elapsedTime += Time.deltaTime;
            }
            else
            {
                zombieVisible = false;
                gameObject.SetActive(false);
                LoadGame();
            }
        }
    }

    private void LoadGame()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;

        sceneNumber += 1;

        SceneManager.LoadScene(sceneNumber);

        
    }

    void Scare()
    {
        gameObject.SetActive(true);
        zombieVisible = true;
    }

}
