using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class EnemyMove : IState
{
    private Enemy enemy;
    List<AStarNode> travelingPath;
    PathFinding levelPath;
    public Animator animator;
    public PatrolTypes initialPatrolType;
    public PatrolTypes currentPatrolType;
    public GameObject detectionRadar;
    GameObject level;
    private Vector3 originalPosition;
    private Vector3 patrolDestination;
    private GameObject decoyTarget;
    private Vector3 startingDirection;
    private float speed = 1.0f;

    enum PathState
    {
        INITIAL,
        GENERATE_PATH,
        TRAVEL_PATH,
        FREEZE,
    }

    enum TargetDestination
    {
        PATROL_DESTINATION,
        ORIGINAL_LOCATION,
    }

    TargetDestination currentDestination = TargetDestination.PATROL_DESTINATION;

    private Vector2[] detectionPoints = {
        new Vector2(-1.0f, 0.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(0.0f, -1.0f)
    };

    public EnemyMove(Enemy enemy, PatrolTypes patrolType, Vector3 patrolDestination, Vector3 startingDirection)
    {
        this.enemy = enemy;
        this.initialPatrolType = patrolType;
        this.currentPatrolType = this.initialPatrolType;
        this.patrolDestination = patrolDestination;
        this.originalPosition = enemy.transform.position;
        this.startingDirection = startingDirection;
    }
    public void Enter()
    {
        level = GameObject.Find("Level");
        levelPath = level.GetComponentInChildren<PathFinding>();
        animator = enemy.GetComponent<Animator>();
        detectionRadar = enemy.transform.Find("Detection Radar").gameObject;
        travelingPath = new List<AStarNode>();
        currentDestination = TargetDestination.ORIGINAL_LOCATION;
        decoyTarget = null;
        DetermineAnimation(Vector3.zero, startingDirection);
    }
    public void Execute()
    {
        if (currentPatrolType != PatrolTypes.STANDING)
        {
            TravelPath();
        }

        GameObject decoy = GameObject.FindGameObjectWithTag("Mocking Bird");

        if (decoy != null && decoy != decoyTarget)
        {
            List<AStarNode> nextPath = CalculatePath(GameObject.FindGameObjectWithTag("Mocking Bird").transform.position);

            if (nextPath.Count < 20)
            {
                decoyTarget = decoy;
                currentDestination = TargetDestination.PATROL_DESTINATION;
                currentPatrolType = PatrolTypes.MOVING;
                travelingPath = nextPath;
            }
        }
    }


    private void GeneratePath(Vector3 destination)
    {
        Vector3 flooredDestination = new Vector3(Mathf.Floor(destination.x) + 0.5f, Mathf.Floor(destination.y) + 0.5f, destination.z);
        travelingPath = levelPath.FindPath(enemy.transform.position, flooredDestination);
    }

    private List<AStarNode> CalculatePath(Vector3 destination)
    {
        Vector3 flooredDestination = new Vector3(Mathf.Floor(destination.x) + 0.5f, Mathf.Floor(destination.y) + 0.5f, destination.z);
        return levelPath.FindPath(enemy.transform.position, flooredDestination);
    }

    public void NewDestination(Vector3 newPosition)
    {
        speed = 8.0f;
        Vector3 flooredPosition = new Vector3(Mathf.Floor(newPosition.x) + 0.5f, Mathf.Floor(newPosition.y) + 0.5f, newPosition.z);
        travelingPath = levelPath.FindPath(enemy.transform.position, flooredPosition);
        currentDestination = TargetDestination.PATROL_DESTINATION;
    }

    private void TravelPath()
    {
        if (travelingPath.Count > 0)
        {
            Debug.Log("Traveling");
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
        else if (currentDestination == TargetDestination.ORIGINAL_LOCATION && initialPatrolType != PatrolTypes.STANDING)
        {
            speed = 1.0f;
            GeneratePath(patrolDestination);
            currentDestination = TargetDestination.PATROL_DESTINATION;
            animator.SetBool("isRunning", false);
            DetermineAnimation(Vector3.zero, startingDirection);
        }
        else if (currentDestination == TargetDestination.PATROL_DESTINATION)
        {
            speed = 1.0f;
            GeneratePath(originalPosition);
            currentPatrolType = PatrolTypes.MOVING;
            currentDestination = TargetDestination.ORIGINAL_LOCATION;
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
        currentPatrolType = PatrolTypes.STANDING;
        animator.SetBool("isRunning", false);
        enemy.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
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
