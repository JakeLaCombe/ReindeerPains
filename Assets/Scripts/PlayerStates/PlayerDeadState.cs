using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerDeadState : IState
{
    Player player;
    public PlayerDeadState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {

    }
    public void Execute()
    {
        this.player.rigidBody.velocity = new Vector2(0.0f, 0.0f);
        this.player.animator.SetBool("isDead", true);
    }
    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}
