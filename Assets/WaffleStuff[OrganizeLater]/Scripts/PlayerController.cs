using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    InputSystem_Actions inputActions;
    public enum States { None, Up, Right, Down, Left}
    public States TopState {get; private set;} = States.None;
    public States BotState {get; private set;} = States.None;
    public float ResetTimer = 0.1f;

    [Header("Pos")]
    public Vector3 TopPos;
    public Vector3 MidPos;
    public Vector3 BotPos;

     private Animator animator;

    private void Awake() {
        inputActions = new();
        inputActions?.Player.Enable();
        inputActions.Player.Up.performed += OnUpPerformed;
        inputActions.Player.Down.performed += OnDownPerformed;
        inputActions.Player.Right.performed += OnRightPerformed;
        inputActions.Player.Left.performed += OnLeftPerformed;
        animator = GetComponentInChildren<Animator>();
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
        ResetAniamtorTriggers();
        animator.SetTrigger("Up");
        ProcessState();
        StartCoroutine(ResetState(TopState));
    }

    private void OnRightPerformed(InputAction.CallbackContext context)
    {
        TopState = States.Right;
        ResetAniamtorTriggers();
        animator.SetTrigger("Right");
        ProcessState();
        StartCoroutine(ResetState(TopState));
    }

    private void OnLeftPerformed(InputAction.CallbackContext context)
    {
        BotState = States.Left;
        ResetAniamtorTriggers();
        animator.SetTrigger("Left");
        ProcessState();
        StartCoroutine(ResetState(BotState));
    }

    private void OnDownPerformed(InputAction.CallbackContext context)
    {
        BotState = States.Down;
        ResetAniamtorTriggers();
        animator.SetTrigger("Down");
        ProcessState();
        StartCoroutine(ResetState(BotState));
    }

    private void ProcessState() {
        if (TopState != States.None && BotState != States.None) transform.position = MidPos;
        else if (TopState != States.None) transform.position = TopPos;
        else transform.position = BotPos;
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
        // ProcessState();
    }

    private void ResetAniamtorTriggers() {
        animator.ResetTrigger("Up");
        animator.ResetTrigger("Down");
        animator.ResetTrigger("Left");
        animator.ResetTrigger("Right");
    }
}
