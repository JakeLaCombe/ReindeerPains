using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class EnemyMove : IState
{
    private Enemy enemy;
    bool travelingToPatrolPoint = true;
    List<AStarNode> travelingPath;
    PathFinding levelPath;
    public Animator animator;
    public PatrolTypes patrolType;
    public GameObject detectionRadar;
    GameObject level;
    private Vector3 originalPosition;
    private Vector3 patrolDestination;
    private GameObject decoyTarget;

    private float speed = 1.0f;

    enum PathState
    {
        INITIAL,
        GENERATE_PATH,
        TRAVEL_PATH,
        FREEZE,
    }

    PathState currentState;

    private Vector2[] detectionPoints = {
        new Vector2(-1.0f, 0.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(0.0f, -1.0f)
    };

    public EnemyMove(Enemy enemy, PatrolTypes patrolType, Vector3 patrolDestination)
    {
        this.enemy = enemy;
        this.patrolType = patrolType;
        this.patrolDestination = patrolDestination;
        this.currentState = PathState.GENERATE_PATH;
        originalPosition = enemy.transform.position;
    }
    public void Enter()
    {
        travelingToPatrolPoint = true;
        level = GameObject.Find("Level");
        levelPath = level.GetComponentInChildren<PathFinding>();
        animator = enemy.GetComponent<Animator>();
        travelingToPatrolPoint = true;
        detectionRadar = enemy.transform.Find("Detection Radar").gameObject;
    }
    public void Execute()
    {
        if (currentState == PathState.GENERATE_PATH)
        {
            GeneratePath(patrolDestination);
        }

        if (currentState == PathState.TRAVEL_PATH)
        {
            TravelPath();
        }

        GameObject decoy = GameObject.FindGameObjectWithTag("Mocking Bird");

        if (decoy != null && decoy != decoyTarget)
        {
            decoyTarget = decoy;
            travelingToPatrolPoint = true;
            GeneratePath(GameObject.FindGameObjectWithTag("Mocking Bird").transform.position);
        }
    }


    private void GeneratePath(Vector3 destination)
    {
        speed = 1.0f;
        if (travelingToPatrolPoint)
        {
            Vector3 flooredDestination = new Vector3(Mathf.Floor(destination.x) + 0.5f, Mathf.Floor(destination.y) + 0.5f, destination.z);
            travelingPath = levelPath.FindPath(enemy.transform.position, flooredDestination);
            travelingToPatrolPoint = false;
        }
        else
        {
            travelingPath = levelPath.FindPath(enemy.transform.position, originalPosition);
            travelingToPatrolPoint = true;
        }

        currentState = PathState.TRAVEL_PATH;
    }

    public void NewDestination(Vector3 newPosition)
    {
        speed = 8.0f;
        travelingPath = levelPath.FindPath(enemy.transform.position, newPosition);
        travelingToPatrolPoint = true;
        currentState = PathState.TRAVEL_PATH;
    }

    private void TravelPath()
    {
        if (travelingPath.Count > 0)
        {
            Vector3 nextPosition = levelPath.GetWorldCoordinates(travelingPath[0]);
            nextPosition.z = enemy.transform.position.z;
            nextPosition.x += 0.5f;
            nextPosition.y += 0.5f;

            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, nextPosition, Time.deltaTime * 3.0f * speed);
            DetermineAnimation(enemy.transform.position, nextPosition);

            if (Mathf.Abs(enemy.transform.position.x - nextPosition.x) < float.Epsilon && Mathf.Abs(enemy.transform.position.y - nextPosition.y) < float.Epsilon)
            {
                travelingPath.RemoveAt(0);
            }

            animator.SetBool("isRunning", true);
        }
        else if (!travelingToPatrolPoint || patrolType != PatrolTypes.STANDING)
        {
            currentState = PathState.GENERATE_PATH;
            animator.SetBool("isRunning", false);
        }
        else
        {
            Freeze();
        }
    }

    private void DetermineAnimation(Vector3 current, Vector3 target)
    {
        if (current.x != target.x)
        {
            animator.SetBool("isFacingDown", false);
            animator.SetBool("isFacingUp", false);

            Vector3 currentScale = enemy.transform.localScale;

            if (current.x < target.x)
            {
                currentScale.x = Mathf.Abs(currentScale.x);
            }
            else
            {
                currentScale.x = -Mathf.Abs(currentScale.x);
            }

            detectionRadar.transform.localPosition = detectionPoints[1];
            detectionRadar.transform.eulerAngles = new Vector3(0, 0, 0);
            enemy.transform.localScale = currentScale;
        }
        else if (current.y < target.y)
        {
            animator.SetBool("isFacingDown", false);
            animator.SetBool("isFacingUp", true);
            detectionRadar.transform.eulerAngles = new Vector3(0, 0, 90);
            detectionRadar.transform.localPosition = detectionPoints[2];

            Vector3 currentScale = enemy.transform.localScale;
            currentScale.x = Mathf.Abs(currentScale.x);
            enemy.transform.localScale = currentScale;
        }
        else if (current.y > target.y)
        {
            animator.SetBool("isFacingDown", true);
            animator.SetBool("isFacingUp", false);
            detectionRadar.transform.eulerAngles = new Vector3(0, 0, 270);
            detectionRadar.transform.localPosition = detectionPoints[3];

            Vector3 currentScale = enemy.transform.localScale;
            currentScale.x = Mathf.Abs(currentScale.x);
            enemy.transform.localScale = currentScale;
        }
    }

    public void processAction()
    {

    }

    public void CheckCollider()
    {

    }

    public void Exit()
    {

    }

    public void Freeze()
    {
        currentState = PathState.FREEZE;
        animator.SetBool("isRunning", false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Mocking Bird")
        {
            GameObject.Destroy(other.gameObject);
        }

        if (other.tag == "Player")
        {
            enemy.ShootTransition();
            other.gameObject.GetComponent<Player>().Kill();
        }
    }
}
