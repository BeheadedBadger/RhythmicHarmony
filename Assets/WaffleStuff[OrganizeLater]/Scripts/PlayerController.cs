using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    InputSystem_Actions inputActions;
    public enum States { None, Up, Right, Down, Left}
    public States TopState {get; private set;} = States.None;
    public States BotState {get; private set;} = States.None;
    public float ResetTimer = 0.1f;

    [Header("Colliders")]
    public Collider2D TopCol;
    public Collider2D MidCol;
    public Collider2D BotCol;

    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] BeatMapManager beatMapManager;
    [SerializeField] ScoreHandler scoreHandler;

    private void Awake() {
        inputActions = new();
        inputActions?.Player.Enable();
        inputActions.Player.Up.performed += OnUpPerformed;
        inputActions.Player.Down.performed += OnDownPerformed;
        inputActions.Player.Right.performed += OnRightPerformed;
        inputActions.Player.Left.performed += OnLeftPerformed;

        animator = transform.GetChild(0).GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        CheckTopOverlap(BeatRow_SO.Beat.Direction.Up);
        ProcessState();
        StartCoroutine(ResetState(TopState));
    }

    private void OnRightPerformed(InputAction.CallbackContext context)
    {
        TopState = States.Right;
        ResetAniamtorTriggers();
        animator.SetTrigger("Right");
        CheckTopOverlap(BeatRow_SO.Beat.Direction.Right);
        ProcessState();
        StartCoroutine(ResetState(TopState));
    }

    private void OnLeftPerformed(InputAction.CallbackContext context)
    {
        BotState = States.Left;
        ResetAniamtorTriggers();
        animator.SetTrigger("Left");
        CheckBotOverlap(BeatRow_SO.Beat.Direction.Left);
        ProcessState();
        StartCoroutine(ResetState(BotState));
    }

    private void OnDownPerformed(InputAction.CallbackContext context)
    {
        BotState = States.Down;
        ResetAniamtorTriggers();
        animator.SetTrigger("Down");
        CheckBotOverlap(BeatRow_SO.Beat.Direction.Down);
        ProcessState();
        StartCoroutine(ResetState(BotState));
    }

    private void ProcessState() {
        if (TopState != States.None && BotState != States.None) {
            transform.position = new(transform.position.x, MidCol.transform.position.y);
        }
        else if (TopState != States.None) {
            transform.position = new(transform.position.x, TopCol.transform.position.y);
        }
        else {
            transform.position = new(transform.position.x, BotCol.transform.position.y);
        }
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
        transform.position = new(transform.position.x, BotCol.transform.position.y);
    }

    private void CheckTopOverlap(BeatRow_SO.Beat.Direction dir) {
        List<Collider2D> results = new();
        TopCol.Overlap(results);
        foreach (var item in results)
        {
            if (item.GetComponent<BeatObj>().direction == dir) {
                audioSource.Stop();
                audioSource.Play();
                scoreHandler.IncreaseScore();
            }
            item.gameObject.SetActive(false);
        }
    }

    private void CheckBotOverlap(BeatRow_SO.Beat.Direction dir) {
        List<Collider2D> results = new();
        BotCol.Overlap(results);
        foreach (var item in results)
        {
            if (item.GetComponent<BeatObj>().direction == dir) {
                audioSource.Stop();
                audioSource.Play();
                scoreHandler.IncreaseScore();
            }
            item.gameObject.SetActive(false);
        }
    }

    private void ResetAniamtorTriggers() {
        animator.ResetTrigger("Up");
        animator.ResetTrigger("Down");
        animator.ResetTrigger("Left");
        animator.ResetTrigger("Right");
    }
}
