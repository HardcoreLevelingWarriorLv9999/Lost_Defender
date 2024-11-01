using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float Walkspeed = 8f;
    public float sprintSpeed = 14f;
    public float MaxVelocityChange = 10;

    [Space]
    public float aircontrol = 0.5f;

    [Space]
    public float jumpHeight = 30f;

    private Vector2 input;
    private Rigidbody rb;
    private bool sprinting;
    private bool jumping;

    private bool grounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        input = new Vector2(x: Input.GetAxisRaw("Horizontal"), y: Input.GetAxisRaw("Vertical"));
        input.Normalize();

        sprinting = Input.GetButton("sprint");
        jumping = Input.GetButton("jump");
    }

    private void OnTriggerStay(Collider other)
    {
        grounded = true;
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            if (jumping)
            {
                rb . velocity = new Vector3(rb.velocity.x, y: jumpHeight, rb.velocity.z);
            } else if (input.magnitude > 0.5f)
            {
                rb.AddForce(CalculateMovement(sprinting ? sprintSpeed : Walkspeed),ForceMode.VelocityChange);
            }
                else
            {
                var velocity1 = rb.velocity;
                velocity1 = new Vector3(x:velocity1.x * 0.2f * Time.fixedDeltaTime, velocity1.y, z: velocity1.z * 0.2f * Time.fixedDeltaTime);
                rb.velocity = velocity1;
            }
        }else
        {
            if (input.magnitude > 0.5f)
            {
                rb.AddForce(CalculateMovement(sprinting ? sprintSpeed * aircontrol : Walkspeed * aircontrol),ForceMode.VelocityChange);
            }
                else
            {
                var velocity1 = rb.velocity;
                velocity1 = new Vector3(x:velocity1.x * 0.2f * Time.fixedDeltaTime, velocity1.y, z: velocity1.z * 0.2f * Time.fixedDeltaTime);
                rb.velocity = velocity1;
            }
        }

        grounded = false;
    }

    Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, y: 0, z: input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= _speed;

        Vector3 velocity = rb.velocity;


        if(input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(value: velocityChange.x, min: -MaxVelocityChange,MaxVelocityChange);
            velocityChange.z = Mathf.Clamp(value: velocityChange.z, min: -MaxVelocityChange, MaxVelocityChange);

            velocityChange.y = 0;


            return (velocityChange);
        } 
        else
        {
            return new Vector3();
        }
    }
}
