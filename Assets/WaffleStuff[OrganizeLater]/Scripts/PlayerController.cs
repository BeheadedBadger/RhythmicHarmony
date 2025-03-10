using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputSystem_Actions inputActions;
    public enum States { None, Up, Right, Down, Left}
    public States TopState = States.None;
    public States BotState = States.None;
    public float ResetTimer = 0.1f;
    private void Awake() {
        inputActions = new();
        inputActions?.Player.Enable();
        inputActions.Player.Up.performed += OnUpPerformed;
        inputActions.Player.Down.performed += OnDownPerformed;
        inputActions.Player.Right.performed += OnRightPerformed;
        inputActions.Player.Left.performed += OnLeftPerformed;
    }

    private void OnEnable() {
        inputActions?.Player.Enable();
    }

    private void OnDisable() {
        inputActions?.Player.Disable();
    }

    private void OnUpPerformed(InputAction.CallbackContext context)
    {
        TopState = States.Up;
        StartCoroutine(ResetState(TopState));
    }

    private void OnRightPerformed(InputAction.CallbackContext context)
    {
        TopState = States.Right;
        StartCoroutine(ResetState(TopState));
    }

    private void OnLeftPerformed(InputAction.CallbackContext context)
    {
        BotState = States.Left;
        StartCoroutine(ResetState(BotState));
    }

    private void OnDownPerformed(InputAction.CallbackContext context)
    {
        BotState = States.Down;
        StartCoroutine(ResetState(BotState));
    }

    private IEnumerator ResetState(States curState) {
        yield return new WaitForSeconds(ResetTimer);
        switch (curState)
        {
            case States.Up:
                TopState = States.None;
                break;
            case States.Right:
                TopState = States.None;
                break;
            case States.Left:
                BotState = States.None;
                break;
            case States.Down:
                BotState = States.None;
                break;
            default:
                TopState = States.None;
                BotState = States.None;
                break;
        }
    }
}
