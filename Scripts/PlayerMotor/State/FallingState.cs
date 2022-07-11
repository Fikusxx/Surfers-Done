using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : BaseState
{


    public override void Construct()
    {
        motor.anim?.SetTrigger("Fall");
    }

    public override Vector3 ProcessMotion()
    {
        // Apply gravity
        motor.ApplyGravity();

        // Create return Vector3
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = motor.verticalVelocity;
        m.z = motor.baseRunSpeed;

        return m;
    }

    public override void Transition()
    {
        if (motor.isGrounded)
        {
            motor.ChangeState(GetComponent<RunningState>());
        }
    }
}
