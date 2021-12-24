using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class EnemySleep : IState
{
    private Enemy enemy;
    public Animator animator;
    public GameObject detectionRadar;



    public EnemySleep(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
        animator = enemy.GetComponent<Animator>();
        detectionRadar = enemy.transform.Find("Detection Radar").gameObject;
        animator.SetBool("isSleeping", true);
        enemy.StartCoroutine(MovePlayer());
        SoundManager.instance.TrapSetoff.Play();
    }
    public void Execute()
    {
        animator.SetBool("isSleeping", true);
        detectionRadar.SetActive(false);
    }

    public void Exit()
    {
        animator.SetBool("isSleeping", false);
        detectionRadar.SetActive(true);
    }

    public IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(10.0f);
        enemy.MovePlayer();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // no-op
    }
}
