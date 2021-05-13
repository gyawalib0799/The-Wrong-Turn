using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Speedometer : MonoBehaviour
{
    public Rigidbody target;
    public float maxSpeed = 0;


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
    private float speed = 0;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        speed = 0;
    }

    private void Update()
    {
        // 3.6f to convert in kilometers
        // ** The speed must be clamped by the car controller **
        // speed = target.velocity.magnitude * 3.6f;
        speed = GameManager.instance.GetNextLevelSpeed();

        if (speedLabel != null)
            speedLabel.text = ((int)speed) + "km/hr";
        if (arrow != null)
            arrow.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed/(maxSpeed*4)));
    }

}
