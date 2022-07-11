using UnityEngine;


public class JumpingState : BaseState
{

    public float jumpForce = 7f;



    public override void Construct()
    {
        motor.verticalVelocity = jumpForce;
        motor.anim?.SetTrigger("Jump");
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
        if (motor.verticalVelocity < 0)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }
    }
}