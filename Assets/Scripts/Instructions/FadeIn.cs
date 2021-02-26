using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    
    //when to begin fading the text and for how long the fade should take can be set in the inspector
    [SerializeField] float startTime = 2f;
    [SerializeField] float fadeTime = 3f;

   
    //the alpha value of the text color will fade up from 0 to 1
    float alpha = 0;

    //Reference to the textObject in the scene
    TextMeshProUGUI textMessage;

    bool fadeStarted = false;
    float elapsedTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //grab the reference
        textMessage = GetComponent<TextMeshProUGUI>();
        
        //set the initial color to have alpha of 0;
        textMessage.color = new Color(textMessage.color.r, textMessage.color.b, textMessage.color.g, alpha);
       
        //the coroutine will wait the desired amount of time (startTime) and then start the fade
        StartCoroutine(WaitForFade());

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeStarted)
        {   
            //This we interpolate the alpha value between 0 and 1 over the timeframe specified by fadeTime
            if (elapsedTime < fadeTime)
            {
                alpha = Mathf.Lerp(0, 1, elapsedTime / fadeTime);
                textMessage.color = new Color(textMessage.color.r, textMessage.color.b, textMessage.color.g, alpha);
                Debug.Log(textMessage.color.a.ToString());
                elapsedTime += Time.deltaTime;
            }
        }

    }

    IEnumerator WaitForFade()
    {

        yield return new WaitForSeconds(startTime);
        fadeStarted = true;

    }
}
