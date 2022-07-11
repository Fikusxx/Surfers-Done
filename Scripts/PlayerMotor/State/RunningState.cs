using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : BaseState
{

    public override void Construct()
    {
        motor.anim.SetTrigger("Running");
        motor.verticalVelocity = 0f;
    }


    public override Vector3 ProcessMotion()
    {
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = -1f;
        m.z = motor.baseRunSpeed;

        return m;
    }

    public override void Transition()
    {
        if (InputManager.Instance.SwipeLeft)
        {
            // Change lane, go left
            motor.ChangeLane(-1);
        }

        if (InputManager.Instance.SwipeRight)
        {
            // Change lane, go right
            motor.ChangeLane(1);
        }

        if (InputManager.Instance.SwipeUp && motor.isGrounded)
        {
            // Change to jumping state
            motor.ChangeState(GetComponent<JumpingState>());
        }

        if (motor.isGrounded == false)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }

        if (InputManager.Instance.SwipeDown)
        {
            motor.ChangeState(GetComponent<SlidingState>());
        }
    }
}
