using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour, IInputable
{
    public KeyCode UP = KeyCode.UpArrow;
    public KeyCode DOWN = KeyCode.DownArrow;
    public KeyCode LEFT = KeyCode.LeftArrow;
    public KeyCode RIGHT = KeyCode.RightArrow;
    public KeyCode DECOY_DROP = KeyCode.Z;
    public KeyCode VACCINE_SHOOT = KeyCode.X;
    public KeyCode GAS_DROP = KeyCode.C;
    public KeyCode PICKUP = KeyCode.Space;
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

    public bool PickUp()
    {
        return Input.GetKeyDown(PICKUP);
    }

    public bool DropDecoy()
    {
        return Input.GetKeyDown(DECOY_DROP);
    }

    public bool DropGas()
    {
        return Input.GetKeyDown(GAS_DROP);
    }

    public bool ShootVaccine()
    {
        return Input.GetKeyDown(VACCINE_SHOOT);
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
