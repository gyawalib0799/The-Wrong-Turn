using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    [SerializeField] Camera jeepCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        jeepCamera.gameObject.SetActive(false);

        //Camera.main = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
