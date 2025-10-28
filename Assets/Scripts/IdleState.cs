using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{

    private Rigidbody2D rb;
    public override void EnterState(PlayerController player)
    {
        /*float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        rb.velocity = 0f;*/
        player.Update();
    }

    public override void UpdateState(PlayerController player)
    {
        // Check if player is trying to move
        float hinput = Input.GetAxisRaw("Horizontal");
        float vinput = Input.GetAxisRaw("Vertical");

        if (hinput != 0f || vinput != 0f)
        {
            // Player wants to move! Change state
            player.ChangeState(new MovingState());
        }
    }

    

    public override void ExitState(PlayerController player) { }

    public override string GetStateName() => "Idle";
}


