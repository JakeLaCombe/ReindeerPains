using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 patrolDestination;
    private Vector3 originalPosition;
    PathFinding levelPath;
    GameObject level;
    EnemyFSM currentState;
    bool travelingToPatrolPoint;
    List<AStarNode> travelingPath;
    private Animator animator;

    void Start()
    {
        level = GameObject.Find("Level One");
        levelPath = level.GetComponentInChildren<PathFinding>();
        currentState = EnemyFSM.GENERATE_PATH;
        animator = GetComponent<Animator>();
        originalPosition = this.transform.position;
        travelingToPatrolPoint = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyFSM.GENERATE_PATH)
        {
            GeneratePath();
        }

        if (currentState == EnemyFSM.TRAVEL_PATH)
        {
            TravelPath();
        }
    }

    private void GeneratePath()
    {
        if (travelingToPatrolPoint)
        {
            travelingPath = levelPath.FindPath(this.transform.position, patrolDestination);
            travelingToPatrolPoint = false;
        }
        else
        {
            travelingPath = levelPath.FindPath(this.transform.position, originalPosition);
            travelingToPatrolPoint = true;
        }

        currentState = EnemyFSM.TRAVEL_PATH;
    }

    private void TravelPath()
    {
        if (travelingPath.Count > 0)
        {
            Vector3 nextPosition = levelPath.GetWorldCoordinates(travelingPath[0]);
            nextPosition.z = this.transform.position.z;
            nextPosition.x += 0.5f;
            nextPosition.y += 0.5f;

            this.transform.position = Vector3.MoveTowards(this.transform.position, nextPosition, Time.deltaTime * 3.0f);
            DetermineAnimation(this.transform.position, nextPosition);

            if (Mathf.Abs(this.transform.position.x - nextPosition.x) < float.Epsilon && Mathf.Abs(this.transform.position.y - nextPosition.y) < float.Epsilon)
            {
                travelingPath.RemoveAt(0);
            }

            animator.SetBool("isRunning", true);
        }
        else
        {
            currentState = EnemyFSM.GENERATE_PATH;
            animator.SetBool("isRunning", false);
        }
    }

    private void DetermineAnimation(Vector3 current, Vector3 target)
    {
        if (current.x != target.x)
        {
            animator.SetBool("isFacingDown", false);
            animator.SetBool("isFacingUp", false);

            Vector3 currentScale = this.transform.localScale;

            if (current.x < target.x)
            {
                currentScale.x = Mathf.Abs(currentScale.x);
            }
            else
            {
                currentScale.x = -Mathf.Abs(currentScale.x);
            }

            // santaDetection.transform.localPosition = detectionPoints[1];
            // santaDetection.transform.eulerAngles = new Vector3(0, 0, 0);
            this.transform.localScale = currentScale;
        }
        else if (current.y < target.y)
        {
            animator.SetBool("isFacingDown", false);
            animator.SetBool("isFacingUp", true);
            // santaDetection.transform.eulerAngles = new Vector3(0, 0, 90);
            // santaDetection.transform.localPosition = detectionPoints[2];

            Vector3 currentScale = this.transform.localScale;
            currentScale.x = Mathf.Abs(currentScale.x);
            this.transform.localScale = currentScale;
        }
        else if (current.y > target.y)
        {
            animator.SetBool("isFacingDown", true);
            animator.SetBool("isFacingUp", false);
            // santaDetection.transform.eulerAngles = new Vector3(0, 0, 270);
            // santaDetection.transform.localPosition = detectionPoints[3];

            Vector3 currentScale = this.transform.localScale;
            currentScale.x = Mathf.Abs(currentScale.x);
            this.transform.localScale = currentScale;
        }
    }
}

public enum EnemyFSM
{
    INITIAL,
    GENERATE_PATH,
    TRAVEL_PATH,
    FREEZE,
}