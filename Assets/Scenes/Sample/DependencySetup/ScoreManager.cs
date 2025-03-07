using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int comboScore = 0;

    void Start()
    {
        Instance = this;
        comboScore = 0;
    }

    public static void Hit()
    {
        Instance.comboScore++;
        Debug.Log("Hit! Combo Score: " + Instance.comboScore);
    }

    public static void Miss()
    {
        Instance.comboScore = 0;
        Debug.Log("Miss! Combo Score reset.");
    }
}
