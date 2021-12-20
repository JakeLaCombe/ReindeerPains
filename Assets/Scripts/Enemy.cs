using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public PatrolTypes patrolType;
    public StateMachine stateMachine;
    public Vector3 patrolDestination;
    public EnemyMove enemyMoveState;
    public EnemyShoot enemyShoot;
    public EnemySleep enemySleep;

    private bool isVaccinated;

    void Start()
    {
        stateMachine = new StateMachine();
        enemyMoveState = new EnemyMove(this, patrolType, patrolDestination);
        enemyShoot = new EnemyShoot(this);
        enemySleep = new EnemySleep(this);
        stateMachine.ChangeState(enemyMoveState);
        this.transform.Find("Vaccinated").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        stateMachine.currentState.OnTriggerEnter2D(other);
    }

    public void ShootTransition()
    {
        stateMachine.ChangeState(enemyShoot);
    }

    public void SleepTransition()
    {
        stateMachine.ChangeState(enemySleep);
    }

    public void MovePlayer()
    {
        stateMachine.ChangeState(enemyMoveState);
    }

    public void Vaccinate()
    {
        isVaccinated = true;
        this.transform.Find("Vaccinated").gameObject.SetActive(true);
        
        if (stateMachine.currentState == enemyMoveState)
        {
            enemyMoveState.NewDestination(GameObject.Find("Player").transform.position);
        }
    }

    public bool HasBeenVaccinated()
    {
        return isVaccinated;
    }
}

public enum PatrolTypes
{
    STANDING,
    MOVING,
}