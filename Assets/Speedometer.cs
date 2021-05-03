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
        speed = GameManager.instance.GetNextLevelSpeed();
    }

    private void Update(){
        //speed = target.velocity.magnitude * 3.6f;

        //speed = 3;
        float currentRotation = player.transform.rotation.x;

        //Debug.LogError("Current x Rot: " + currentRotation.ToString());
       // if (currentRotation > 0)
       //     speed -= 0.25f;

        if (speedLabel != null)
            speedLabel.text = ((int)(speed)) + "km/hr";
        if (arrow != null & speed<20)
            arrow.localEulerAngles = 
                //new Vector3(0,0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed/maxSpeed));
                new Vector3(0,0, -7);
        if (arrow != null & speed>20 & speed<30)
            arrow.localEulerAngles = 
                //new Vector3(0,0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed/maxSpeed));
                new Vector3(0,0, -16);
        if (arrow != null & speed>30 & speed<40)
            arrow.localEulerAngles = 
                //new Vector3(0,0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed/maxSpeed));
                new Vector3(0,0, -21);
    }

}
