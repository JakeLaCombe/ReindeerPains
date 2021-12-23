using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDeadState : IState
{
    Player player;
    Coroutine loadMainLevel;
    public PlayerDeadState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        GameObject.Find("Theme").GetComponent<AudioSource>().Stop();
    }
    public void Execute()
    {
        this.player.rigidBody.velocity = new Vector2(0.0f, 0.0f);
        this.player.animator.SetBool("isDead", true);

        if (this.player.animator.GetCurrentAnimatorStateInfo(0).length <=
           this.player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            Supplies.instance.RestoreFromCache();
            loadMainLevel = this.player.StartCoroutine(RestartTown());
        }
    }
    public void Exit()
    {

    }

    public IEnumerator RestartTown()
    {
        yield return new WaitForSeconds(5.0f);
        if (Supplies.instance.remainingDays > 0)
        {
            SceneManager.LoadScene("Town");
        }
        else
        {
            SceneManager.LoadScene("Ending");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }
}
