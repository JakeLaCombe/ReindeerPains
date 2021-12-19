using System;
using UnityEngine;

public interface IInputable
{
    bool Up();
    bool UpHold();
    bool Down();
    bool DownHold();
    bool Left();
    bool LeftHold();
    bool Right();
    bool RightHold();
    bool PickUp();
    bool DropDecoy();
    bool DropGas();
    bool ShootVaccine();
    bool Pause();
    bool StartButton();
}
