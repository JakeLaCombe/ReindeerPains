using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerMoveState : IState
{
    Player player;

    private Vector2[] detectionPoints = {
        new Vector2(-1.0f, 0.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(0.0f, 0.5f),
        new Vector2(0.0f, -0.5f)
    };
    public PlayerMoveState(Player player)
    {
        this.player = player;
        Supplies.instance.CacheSupplies();
        Supplies.instance.remainingDays -= 1;
    }
    public void Enter()
    {

    }
    public void Execute()
    {
        float vx = player.rigidBody.velocity.x;
        float vy = player.rigidBody.velocity.y;

        if (player.input.LeftHold())
        {
            vx = -3.0f;
            vy = 0.0f;
            player.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", false);
            player.animator.SetBool("isFacingUp", false);

            player.actionPoint.transform.localPosition = detectionPoints[1];
        }
        else if (player.input.RightHold())
        {
            vx = 3.0f;
            vy = 0.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", false);
            player.animator.SetBool("isFacingUp", false);

            player.actionPoint.transform.localPosition = detectionPoints[1];
        }
        else if (player.input.UpHold())
        {
            vx = 0.0f;
            vy = 3.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", false);
            player.animator.SetBool("isFacingUp", true);

            player.actionPoint.transform.localPosition = detectionPoints[2];
        }
        else if (player.input.DownHold())
        {
            vx = 0.0f;
            vy = -3.0f;
            player.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            player.animator.SetBool("isFacingDown", true);
            player.animator.SetBool("isFacingUp", false);

            player.actionPoint.transform.localPosition = detectionPoints[3];
        }
        else
        {
            vx = 0.0f;
            vy = 0.0f;
        }

        player.rigidBody.velocity = new Vector2(
           vx,
           vy
        );

        player.animator.SetBool("isRunning", vx != 0 || vy != 0);

        if (player.input.PickUp())
        {
            processAction();
        }

        if (player.input.DropDecoy() && Supplies.instance.roosterDecoys > 0)
        {
            MockingBird bird = GameObject.Instantiate(Prefabs.instance.MOCKING_BIRD, player.transform.position, Quaternion.identity);
            bird.Activate();
            Supplies.instance.roosterDecoys -= 1;
            SoundManager.instance.Decoy.Play();

        }

        if (player.input.DropGas() && Supplies.instance.smokeTraps > 0)
        {
            SmokeTrap trap = GameObject.Instantiate(Prefabs.instance.SMOKE_TRAP, player.transform.position, Quaternion.identity);
            trap.Activate();
            Supplies.instance.smokeTraps -= 1;
            SoundManager.instance.Pickup.Play();
        }

        if (player.input.ShootVaccine())
        {
            ProcessVaccination();
        }
    }

    public void processAction()
    {
        if (player.GetTouchingObjects().Count > 0)
        {
            GameObject wall = player.GetTouchingObjects().Find(delegate (GameObject bk)
                {
                    return bk.name == "Wall";
                }
            );

            if (wall != null)
            {
                Vector3Int position = Vector3Int.FloorToInt(player.actionPoint.transform.position);
                position.z = 0;

                TileBase tile = wall.GetComponent<Tilemap>().GetTile(position);

                if (tile != null && (tile.name == "StoreExit" || tile.name == "ChimnyExit"))
                {
                    if (Supplies.instance.remainingDays > 0)
                    {
                        SceneManager.LoadScene("Town");
                    }
                    else
                    {
                        SceneManager.LoadScene("Ending");
                    }
                }
            }

            GameObject materialPickup = player.GetTouchingObjects().Find(delegate (GameObject bk)
               {
                   return bk.GetComponent<MaterialPickup>() != null;
               }
           );

            if (materialPickup != null)
            {
                materialPickup.GetComponent<MaterialPickup>().GrabItem();
                SoundManager.instance.Pickup.Play();
            }

            GameObject swapPill = player.GetTouchingObjects().Find(delegate (GameObject bk)
               {
                   return bk.GetComponent<SwapPill>() != null;
               }
           );

            if (swapPill != null)
            {
                swapPill.GetComponent<SwapPill>().Swap();
            }
        }
    }

    public void ProcessVaccination()
    {
        if (Supplies.instance.vaccines <= 0)
        {
            return;
        }

        if (player.GetTouchingObjects().Count > 0)
        {
            GameObject enemyObject = player.GetTouchingObjects().Find(delegate (GameObject bk)
             {
                 return bk.GetComponent<Enemy>() != null;
             }
         );


            Enemy enemy = enemyObject != null ? enemyObject.GetComponent<Enemy>() : null;

            if (enemy != null && !enemy.HasBeenVaccinated())
            {
                enemy.GetComponent<Enemy>().Vaccinate();
                Supplies.instance.vaccines -= 1;
                Supplies.instance.vaccinatedAdults += 1;
                SoundManager.instance.Vaccinate.Play();
                return;
            }
        }

        if (Supplies.instance.hasShotgun)
        {
            VaccineProjectile projectile = GameObject.Instantiate(Prefabs.instance.VACCINE_PROJECTILE, player.transform.position, Quaternion.identity);
            projectile.LaunchDirection(player.actionPoint.transform.localPosition * player.transform.localScale.x);
            Supplies.instance.vaccines -= 1;
            SoundManager.instance.Shoot.Play();
        }
    }

    public void CheckCollider()
    {

    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // no-op
    }
}
