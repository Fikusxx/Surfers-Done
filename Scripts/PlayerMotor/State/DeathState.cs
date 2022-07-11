using UnityEngine;


public class DeathState : BaseState
{

    [SerializeField] private Vector3 knockbackForce = new Vector3(0, 6, -4);
    private Vector3 currentKnockback;


    public override void Construct()
    {
        motor.anim?.SetTrigger("Death");
        currentKnockback = knockbackForce;
    }




    public override Vector3 ProcessMotion()
    {
        currentKnockback = new Vector3(
            0,
            currentKnockback.y -= motor.gravity * Time.deltaTime,
            currentKnockback.z += 2f * Time.deltaTime);

        if (currentKnockback.z > 0)
        {
            currentKnockback.z = 0;
            GameManager.Instance.ChangeState(GameManager.Instance.GetComponent<GameStateDeath>());
        }

        return currentKnockback;
    }

    public override void Destruct()
    {
        
    }
}
