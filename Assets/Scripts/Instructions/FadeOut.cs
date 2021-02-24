using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI left;
    [SerializeField] TextMeshProUGUI right;
    [SerializeField] TextMeshProUGUI instructions;

    [SerializeField] Button readyButton;

    [SerializeField] float buttonActivationTime = 7;
     

    [SerializeField] float fadeTime = 2;

    float elapsedTime = 0;
    float buttonElapsedTime = 0;

    bool fadeStarted = false;
    bool readyButtonVisible = false;

    float alpha = 1;


    // Start is called before the first frame update
    void Start()
    {
        readyButton.gameObject.SetActive(false);
        readyButtonVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
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
            if (elapsedTime < fadeTime)
            {
                alpha = Mathf.Lerp(1,0, elapsedTime / fadeTime);
                left.color = new Color(left.color.r, left.color.g, left.color.b, alpha);
                right.color = new Color(right.color.r, right.color.g, right.color.b, alpha);
                instructions.color = new Color(instructions.color.r, instructions.color.g, instructions.color.b, alpha);
                elapsedTime += Time.deltaTime;
            }
           
        }

    }


    public void ReadyButtonPushed()
    {
        fadeStarted = true;

       readyButton.gameObject.SetActive(false);
        //readyButtonVisible = false;

        
    }

}
