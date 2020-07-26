using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    public float movementForce = 500f;
    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey("right"))
            rb.AddForce(movementForce * Time.deltaTime, 0, 0);
        if (Input.GetKey("left"))
            rb.AddForce(-movementForce * Time.deltaTime, 0, 0);
        if (Input.GetKey("up"))
            rb.AddForce(0, 0, movementForce * Time.deltaTime);
        if (Input.GetKey("down"))
            rb.AddForce(0, 0, -movementForce * Time.deltaTime);

    }
}
