using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerMoveState: IState
{
    Player player;

     private Vector2[] detectionPoints = {
        new Vector2(-1.0f, 0.0f),
        new Vector2(0.25f, -0.25f),
        new Vector2(0.0f, 0.5f),
        new Vector2(0.0f, -1.25f)
    };
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

            player.actionPoint.transform.localPosition = detectionPoints[1];
        } else if (player.input.RightHold()) {
            vx = 3.0f;
            vy = 0.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", false);
            player.animator.SetBool("isFacingUp", false);

            player.actionPoint.transform.localPosition = detectionPoints[1];
        }
        else if (player.input.UpHold()) {
            vx = 0.0f;
            vy = 3.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", false);
            player.animator.SetBool("isFacingUp", true);

            player.actionPoint.transform.localPosition = detectionPoints[2];
        } else if (player.input.DownHold()) {
            vx = 0.0f;
            vy = -3.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", true);
            player.animator.SetBool("isFacingUp", false);

            player.actionPoint.transform.localPosition = detectionPoints[3];
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
            if (player.GetTouchingObject() != null)
            {
                if (player.GetTouchingObject().name == "Wall")
                {
                    Vector3Int position = Vector3Int.FloorToInt(player.actionPoint.transform.position);
                    position.z = 0;

                    TileBase tile = player.GetTouchingObject().GetComponent<Tilemap>().GetTile(position);


                    if (tile != null && tile.name == "StoreExit")
                    {
                        SceneManager.LoadScene("Town");
                    }
                }
            }
        }

        if (player.input.SecondaryAction())
        {
            MockingBird bird = GameObject.Instantiate(Prefabs.instance.MOCKING_BIRD, player.transform.position, Quaternion.identity);
            bird.Activate();
        }
    }

    public void CheckCollider()
    {

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