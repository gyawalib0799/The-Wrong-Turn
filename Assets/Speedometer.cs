using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Speedometer : MonoBehaviour
{
    public Rigidbody target;
    public float maxSpeed = 22f;


    //1.4  18 t = 2.5
    //1.4, 20 t=2.4
    //1.4, 22 t=2.2
    //1.6, 25 t=2.0
    //1.8, 27 t=1.8
    //2.2, 29 t=1.6
    //2.4, 31 t=1.4

    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI")]
    public Text speedLabel;
    public RectTransform arrow;
    private float speed = 15.5f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update(){
        //speed = target.velocity.magnitude * 3.6f;

        speed = 3;
        float currentRotation = player.transform.rotation.x;

        //Debug.LogError("Current x Rot: " + currentRotation.ToString());
       // if (currentRotation > 0)
       //     speed -= 0.25f;

        if (speedLabel != null)
            speedLabel.text = ((int)(speed * 22)) + "km/hr";
        if (arrow != null)
            arrow.localEulerAngles = 
                new Vector3(0,0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed/maxSpeed));
    }

}
