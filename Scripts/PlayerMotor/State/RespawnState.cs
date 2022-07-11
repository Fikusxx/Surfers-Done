using UnityEngine;


public class RespawnState : BaseState
{

    [SerializeField] private float verticalDistance = 25f;
    [SerializeField] private float immunityTime = 1f;

    private float startTime;


    public override void Construct()
    {
        startTime = Time.time;

        motor.controller.enabled = false;
        motor.transform.position = new Vector3(0, verticalDistance, motor.transform.transform.position.z);
        motor.controller.enabled = true;

        motor.anim.SetTrigger("Respawn");

        motor.currentLane = 0;
        motor.verticalVelocity = 0f;

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

        if (motor.isGrounded && Time.time - startTime > immunityTime)
        {
            motor.ChangeState(GetComponent<RunningState>());
        }
    }

    public override void Destruct()
    {
        GameManager.Instance.ChangeCamera(GameCameras.Game);
    }
}
