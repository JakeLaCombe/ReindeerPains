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
    public Vector3 startingDirection = new Vector3(1.0f, 1.0f, 1.0f);

    private bool isVaccinated;

    void Awake()
    {
        stateMachine = new StateMachine();
        enemyMoveState = new EnemyMove(this, patrolType, patrolDestination, startingDirection);
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
        Supplies.instance.vaccinatedAdults += 1;

        Debug.Log("Vaccinating");

        if (stateMachine.currentState == enemyMoveState)
        {
            enemyMoveState.NewDestination(GameObject.Find("Player").transform.position);
        }
    }

    public void KillPlayer()
    {
        ShootTransition();
        GameObject.FindWithTag("Player").GetComponent<Player>().Kill();
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