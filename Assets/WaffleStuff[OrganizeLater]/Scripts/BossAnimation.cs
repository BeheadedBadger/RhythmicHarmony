using System.Collections;
using System.Threading;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    // 0 = break, 1 = down, 2 = break, 3 = up
    int state = 0;
    readonly Vector2 Y_RANGE = new(-2f, 0.6f);
    [SerializeField][Min(0f)] float moveDuration;
    [SerializeField][Min(0f)] float pauseDuration;
    float endTime;

    private void Update() {
        if (Time.time < endTime) return;
        switch (state)
        {
            case 0:
                endTime = Time.time + pauseDuration;
                state = (state + 1) % 4;
                break;
            case 1:
                StartCoroutine(GoDown());
                break;
            case 2:
                endTime = Time.time + pauseDuration;
                state = (state + 1) % 4;
                break;
            case 3:
                StartCoroutine(GoUp());
                break;
            default:
            break;
        }
        
    }

    private IEnumerator GoUp() {
        float timer = Time.time + moveDuration;
        endTime = Time.time + moveDuration + 0.1f;
        while (Time.time < timer)
        {
            transform.position = new(transform.position.x, Mathf.Lerp(Y_RANGE.x, Y_RANGE.y, 1 - ((timer - Time.time)/ moveDuration)));
            yield return null;
        }
        state = (state + 1) % 4;
    }

    private IEnumerator GoDown() {
        float timer = Time.time + moveDuration;
        endTime = Time.time + moveDuration + 0.1f;
        while (Time.time < timer)
        {
            transform.position = new(transform.position.x, Mathf.Lerp(Y_RANGE.y, Y_RANGE.x, 1 - ((timer- Time.time)/ moveDuration)));
            yield return null;
        }
        state = (state + 1) % 4;
    }
}
