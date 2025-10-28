using UnityEngine;

public class MovingState : PlayerState
{
    private Rigidbody2D rb;

    public override void EnterState(PlayerController player)
    {
        player.HandleMovement();
    }

    public override void UpdateState(PlayerController player)
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.1f && Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.1f)
        {
            player.ChangeState(new IdleState());
        }

        /*if (Input.GetKeyDown(KeyCode.G))
        {
            player.Fire();
        }*/
    }
   

    public override void ExitState(PlayerController player) { }

    public override string GetStateName() => "Moving";

    
}
