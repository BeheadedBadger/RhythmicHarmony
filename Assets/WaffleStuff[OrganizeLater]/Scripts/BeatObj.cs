using UnityEngine;

public class BeatObj : MonoBehaviour
{
    public BeatRow_SO.Beat.Direction direction;
    void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
