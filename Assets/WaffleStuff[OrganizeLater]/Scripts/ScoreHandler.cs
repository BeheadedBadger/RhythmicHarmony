using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public int Score {get; private set;} = 0;
    public void IncreaseScore() {
        Score++;
    }
}
