using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Core;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private Joystick joystick;
    private Vector3 inputDir;

    public Vector3 GetJoystickInput()
    {
        inputDir = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        
        return inputDir;
    }

    public Vector3 GetKeyboardInput()
    {
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        return inputDir;
    }

    public float GetJoystickSpeed()
    {
        return inputDir.magnitude;
    }
}
