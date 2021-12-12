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
    public EnemyFSM currentState;
    bool travelingToPatrolPoint;
    List<AStarNode> travelingPath;
    public Animator animator;
    public PatrolTypes patrolType;

    public GameObject detectionRadar;

     private Vector2[] detectionPoints = {
        new Vector2(-1.0f, 0.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(0.0f, -1.0f)
    };

    void Start()
    {
        level = GameObject.Find("Level One");
        levelPath = level.GetComponentInChildren<PathFinding>();
        animator = GetComponent<Animator>();
        originalPosition = this.transform.position;
        travelingToPatrolPoint = true;
        detectionRadar = this.transform.Find("Detection Radar").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyFSM.GENERATE_PATH)
        {
            GeneratePath(patrolDestination);
        }

        if (currentState == EnemyFSM.TRAVEL_PATH)
        {
            TravelPath();
        }

        if (currentState == EnemyFSM.FREEZE)
        {
            if (GameObject.FindGameObjectWithTag("Mocking Bird"))
            {
                GeneratePath(GameObject.FindGameObjectWithTag("Mocking Bird").transform.position);
                patrolDestination = GameObject.FindGameObjectWithTag("Mocking Bird").transform.position;
            }
        }
    }

    public void Freeze()
    {
        currentState = EnemyFSM.FREEZE;
        animator.SetBool("isRunning", false);
    }

    public void Activate()
    {
        currentState = EnemyFSM.GENERATE_PATH;
        animator.SetBool("isRunning", true);
    }

    private void GeneratePath(Vector3 destination)
    {
        if (travelingToPatrolPoint)
        {
            travelingPath = levelPath.FindPath(this.transform.position, destination);
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
        else if (!travelingToPatrolPoint || patrolType != PatrolTypes.STANDING)
        {
            currentState = EnemyFSM.GENERATE_PATH;
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

            Vector3 currentScale = this.transform.localScale;

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
            this.transform.localScale = currentScale;
        }
        else if (current.y < target.y)
        {
            animator.SetBool("isFacingDown", false);
            animator.SetBool("isFacingUp", true);
            detectionRadar.transform.eulerAngles = new Vector3(0, 0, 90);
            detectionRadar.transform.localPosition = detectionPoints[2];

            Vector3 currentScale = this.transform.localScale;
            currentScale.x = Mathf.Abs(currentScale.x);
            this.transform.localScale = currentScale;
        }
        else if (current.y > target.y)
        {
            animator.SetBool("isFacingDown", true);
            animator.SetBool("isFacingUp", false);
            detectionRadar.transform.eulerAngles = new Vector3(0, 0, 270);
            detectionRadar.transform.localPosition = detectionPoints[3];

            Vector3 currentScale = this.transform.localScale;
            currentScale.x = Mathf.Abs(currentScale.x);
            this.transform.localScale = currentScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Mocking Bird")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "Player")
        {
            this.Freeze();
            this.animator.SetBool("isShooting", true);
            other.gameObject.GetComponent<Player>().Kill();
        }
    }

    public void PositionRadar()
    {

    }
}

public enum EnemyFSM
{
    INITIAL,
    GENERATE_PATH,
    TRAVEL_PATH,
    FREEZE,
}

public enum PatrolTypes {
    STANDING,
    MOVING,
}