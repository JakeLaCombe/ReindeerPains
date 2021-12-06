using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour, IInputable
{
    public KeyCode UP = KeyCode.UpArrow;
    public KeyCode DOWN = KeyCode.DownArrow;
    public KeyCode LEFT = KeyCode.LeftArrow;
    public KeyCode RIGHT = KeyCode.RightArrow;
    public KeyCode ACTION = KeyCode.C;
    public KeyCode PAUSE = KeyCode.Return;
    public KeyCode SECONDARY_ACTION = KeyCode.X;

    public bool Up()
    {
        return Input.GetKeyDown(UP);
    }

    public bool UpHold()
    {
        return Input.GetKey(UP);
    }

    public bool Down()
    {
        return Input.GetKeyDown(DOWN);
    }

    public bool DownHold()
    {
        return Input.GetKey(DOWN);
    }

    public bool Left()
    {
        return Input.GetKeyDown(LEFT);
    }

    public bool LeftHold()
    {
        return Input.GetKey(LEFT);
    }

    public bool Right()
    {
        return Input.GetKeyDown(RIGHT);
    }

    public bool RightHold()
    {
        return Input.GetKey(RIGHT);
    }

    public bool Action()
    {
        return Input.GetKeyDown(ACTION);
    }

    public bool ActionHold()
    {
        return Input.GetKey(ACTION);
    }


    public bool Pause()
    {
        return Input.GetKeyDown(PAUSE);
    }

    public bool StartButton()
    {
        return Input.GetKeyDown(PAUSE);
    }

    public bool SecondaryAction()
    {
        return Input.GetKeyDown(SECONDARY_ACTION);
    }

    public bool SecondaryActionHold()
    {
        return Input.GetKey(SECONDARY_ACTION);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
