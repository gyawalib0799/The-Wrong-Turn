using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxiMovement : MonoBehaviour
{
    public float speed;     

    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementVector = this.transform.up;

        this.rigid.AddForce(-movementVector * 1.0f);

    }
}
