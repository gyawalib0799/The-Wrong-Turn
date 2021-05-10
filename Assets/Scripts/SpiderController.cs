using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject[] spiders;


    int previousSpider = -1;

    void Start()
    {
        foreach(GameObject spider in spiders)
        {
            spider.SetActive(false);
        }
        
        StartCoroutine(WaitForNextSpider());
    }

    // Update is called once per frame
    void Update()
    {
        




    }


    IEnumerator WaitForNextSpider()
    {
        float waitTime = 0;
        
        while(true)
        {
             waitTime = UnityEngine.Random.Range(10, 20);
            
            
            
            yield return new WaitForSeconds(waitTime);
            SpawnSpider();


        }
    }

    private void SpawnSpider()
    {
        int spiderIndex = UnityEngine.Random.Range(0, spiders.Length);

        while(spiderIndex == previousSpider)
        {
            spiderIndex = UnityEngine.Random.Range(0, spiders.Length);
        }

        spiders[spiderIndex].SetActive(true);
        previousSpider = spiderIndex;
    }
}
