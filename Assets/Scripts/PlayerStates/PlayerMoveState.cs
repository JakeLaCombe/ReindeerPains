using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMoveState: IState
{
    Player player;
    public PlayerMoveState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
       
    }
    public void Execute()
    {
        float vx = player.rigidBody.velocity.x;
        float vy =  player.rigidBody.velocity.y;

        if (player.input.LeftHold()) {
            vx = -3.0f;
            vy = 0.0f;
            player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", false);
            player.animator.SetBool("isFacingUp", false);
        } else if (player.input.RightHold()) {
            vx = 3.0f;
            vy = 0.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", false);
            player.animator.SetBool("isFacingUp", false);
        }
        else if (player.input.UpHold()) {
            vx = 0.0f;
            vy = 3.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", false);
            player.animator.SetBool("isFacingUp", true);

        } else if (player.input.DownHold()) {
            vx = 0.0f;
            vy = -3.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", true);
            player.animator.SetBool("isFacingUp", false);
        } else {
            vx = 0.0f;
            vy = 0.0f;
        }

        player.rigidBody.velocity = new Vector2(
           vx,
           vy
        );

        player.animator.SetBool("isRunning", vx != 0 || vy != 0);

        if (player.input.Action())
        {
            MockingBird bird = GameObject.Instantiate(Prefabs.instance.MOCKING_BIRD, player.transform.position, Quaternion.identity);
            bird.Activate();
        }
    }
    public void Exit()
    {

    }
}

/**


public void Move()
    {
       
    }

*/