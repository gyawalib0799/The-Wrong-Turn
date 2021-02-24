using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] float startTime = 2f;
    [SerializeField] float fadeTime = 3f;

   
    
    float alpha = 0;

    TextMeshProUGUI textMessage;

    bool fadeStarted = false;
    float elapsedTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        textMessage = GetComponent<TextMeshProUGUI>();
        if(textMessage == null)
        {
            textMessage = GetComponentInChildren<TextMeshProUGUI>();
        }
        textMessage.color = new Color(textMessage.color.r, textMessage.color.b, textMessage.color.g, alpha);
       

        StartCoroutine(WaitForFade());

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeStarted)
        {
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
