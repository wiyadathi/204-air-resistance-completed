using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneV2 : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float enginePower = 50f;
    [SerializeField] float liftBooster = 0.5f;
    [SerializeField] float drag = 0.003f;
    [SerializeField] float angularDrag = 0.03f;


    [SerializeField] float yawPower = 50f;     // Turning speed
    [SerializeField] float pitchPower = 50f;   // Nose up/down speed
    [SerializeField] float rollPower = 30f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //add Thrust (Engine Power)
        //Pressing Spacebar applies force in the forward direction of the airplane(transform.forward).
        //Simulates engine thrust, making the airplane accelerate forward.
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.forward * enginePower);
        }

        //2. add  Lift Force (Aerodynamics)
        //Uses Vector3.Project to extract the forward speed of the airplain
        //The faster the airplane moves forward, the greater the lift force.
        //Multiplies by liftBooster to apply upward force(transform.up)
        //Simulates airplane lift: Faster speeds create more lift, making 
        //the airplane rise.
        Vector3 lift = Vector3.Project(rb.velocity, transform.forward);
        rb.AddForce(transform.up * lift.magnitude * liftBooster);

        //3. Drag (Air resistance)
        //rb.drag increases as speed increases, simulating air resistance
        //rb.angularDrag increases with speed, making turning harder at high speeds
        //Prevents infinite acceleration and makes controls feel realistic.
        rb.drag = rb.velocity.magnitude * drag;
        rb.angularDrag = rb.velocity.magnitude * angularDrag;

        //new code 
        //Yaw(turn), Pitch(tilt up / down), and Roll(tilt sideways) must be controlled separately.
        //Improving the code makes the airplane behave more realistically!
        float yaw = Input.GetAxis("Horizontal") * yawPower; // Turn left/right
        float pitch = Input.GetAxis("Vertical") * pitchPower; // Nose up/down
        float roll = Input.GetAxis("Roll") * rollPower;  // // Tilting (A/D or custom keys)

        rb.AddTorque(transform.up * yaw);     // Yaw (turn left/right)
        rb.AddTorque(transform.right * pitch); // Pitch (nose up/down)
        rb.AddTorque(transform.forward * roll); // Roll (barrel roll)*/

    }
}


