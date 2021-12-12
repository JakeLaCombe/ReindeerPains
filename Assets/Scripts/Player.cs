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
    public GameObject objectFinder;

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



    // private void ProcessAction()
    // {
    //     if (touchingChristmasTree != null && !touchingChristmasTree.HasGifts())
    //     {
    //         touchingChristmasTree.SetGifts();
    //         SoundManager.instance.setGifts.Play();
    //     }
    //     else
    //     {
    //         PathFinding tilemap = GameManager.Instance.currentLevel.GetComponentInChildren<PathFinding>();
    //         TileBase tile = tilemap.GetTile(objectFinder.transform.position);

    //         if (tile != null)
    //         {
    //             if (tile.name == "back_mid_section_2")
    //             {
    //                 GameManager.Instance.CheckLevelComplete();
    //             }
    //         }
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
      
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       
    }

 
}
