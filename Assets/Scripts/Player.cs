using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine;
    public PlayerMoveState playerMoveState;
    public PlayerDeadState playerDeadState;


    public Rigidbody2D rigidBody;
    public Animator animator;
    public IInputable input;
    public SpriteRenderer spriteRenderer;
    public ActionPoint actionPoint;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        input = this.gameObject.GetComponent<IInputable>();

        stateMachine = new StateMachine();
        playerMoveState = new PlayerMoveState(this);
        playerDeadState = new PlayerDeadState(this);
        stateMachine.ChangeState(playerMoveState);

        actionPoint = this.transform.Find("ActionPoint").GetComponent<ActionPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    public void Kill()
    {
        stateMachine.ChangeState(playerDeadState);
    }

    public List<GameObject> GetTouchingObjects()
    {
        return actionPoint.getActiveObjects();
    }

}
