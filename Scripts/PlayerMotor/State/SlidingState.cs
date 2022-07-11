using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingState : BaseState
{

    public float slideDuration = 1f;

    // Collider logic
    private Vector3 initialCenter;
    private float initialSize;
    private float slideStart;


    public override void Construct()
    {
        motor.anim?.SetTrigger("Slide");
        slideStart = Time.time;

        initialSize = motor.controller.height;
        initialCenter = motor.controller.center;

        motor.controller.height = initialSize / 2;
        motor.controller.center = initialCenter / 2;
    }

    public override void Transition()
    {
        if (InputManager.Instance.SwipeLeft)
        {
            motor.ChangeLane(-1);
        }
        if (InputManager.Instance.SwipeRight)
        {
            motor.ChangeLane(1);
        }

        if (motor.isGrounded == false)
        {
            motor.ChangeState(GetComponent<FallingState>());
        }

        if (InputManager.Instance.SwipeUp)
        {
            motor.ChangeState(GetComponent<JumpingState>());
        }

        if (Time.time - slideStart > slideDuration)
        {
            motor.ChangeState(GetComponent<RunningState>());
        }
    }

    public override Vector3 ProcessMotion()
    {
        Vector3 m = Vector3.zero;

        m.x = motor.SnapToLane();
        m.y = -1f;
        m.z = motor.baseRunSpeed;

        return m;
    }

    public override void Destruct()
    {
        motor.anim?.SetTrigger("Running");
        motor.controller.height = initialSize;
        motor.controller.center = initialCenter;
    }
}
