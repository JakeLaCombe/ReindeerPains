using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class EnemyShoot : IState
{
    private Enemy enemy;
    public Animator animator;


    public EnemyShoot(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
        animator = enemy.GetComponent<Animator>();
    }
    public void Execute()
    {
        animator.SetBool("isShooting", true);
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // no-op
    }
}
