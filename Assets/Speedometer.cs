using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Speedometer : MonoBehaviour
{
    public Rigidbody target;
    public float maxSpeed = 22f;

    //1.4, 20
    //1.4, 22
    //1.6, 25
    //1.8, 27
    //2.2, 29
    //2.4, 31

    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI")]
    public Text speedLabel;
    public RectTransform arrow;
    private float speed = 15.5f;
    
    public void Update(){
        speed = target.velocity.magnitude * 3.6f;

        if (speedLabel != null)
            speedLabel.text = ((int)(speed*22)) + "km/hr";
        if (arrow != null)
            arrow.localEulerAngles = 
                new Vector3(0,0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed/maxSpeed));
    }

}
