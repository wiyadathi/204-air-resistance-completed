using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Airplane : MonoBehaviour
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

        //control rotation
        rb.AddTorque(Input.GetAxis("Horizontal") * transform.forward * -1f);

        //Yaw Control (Turning Left/Right)
        //Uses left/right input(Horizontal axis) to apply force against the forward direction
        //Simulates turning resistance (airplane doesn't instantly turn, it resists change)
        rb.AddTorque(Input.GetAxis("Vertical") * transform.right);

    }
}
// **Use Torque for Better Rotation Control**
/*        float yaw = Input.GetAxis("Horizontal") * yawPower;
        float pitch = Input.GetAxis("Vertical") * pitchPower;
        float roll = Input.GetAxis("Roll") * rollPower;  // Add roll control (e.g., A/D keys)

        rb.AddTorque(transform.up * yaw);     // Yaw (turn left/right)
        rb.AddTorque(transform.right * pitch); // Pitch (nose up/down)
        rb.AddTorque(transform.forward * roll); // Roll (barrel roll)*/