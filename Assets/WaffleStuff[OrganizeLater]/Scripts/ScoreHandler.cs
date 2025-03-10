using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public int Score {get; private set;} = 0;
    private float totalBeats = 0;

    [SerializeField] BeatRow_SO topRow;
    [SerializeField] BeatRow_SO botRow;
    [SerializeField] GameObject SSobj;
    [SerializeField] GameObject Sobj;
    [SerializeField] GameObject Aobj;
    [SerializeField] GameObject Bobj;
    [SerializeField] GameObject Cobj;
    private void Start() {
        totalBeats = topRow.Beats.Count + botRow.Beats.Count;
    }

    public void IncreaseScore() {
        Score++;
    }

    public void ProcessScore() {
        float percent = Score / totalBeats;
        if (percent > 0.95) SSobj.SetActive(true);
        else if (percent > 0.90) Sobj.SetActive(true);
        else if (percent > 0.75) Aobj.SetActive(true);
        else if (percent > 0.60) Bobj.SetActive(true);
        else Cobj.SetActive(true);
    }
}
