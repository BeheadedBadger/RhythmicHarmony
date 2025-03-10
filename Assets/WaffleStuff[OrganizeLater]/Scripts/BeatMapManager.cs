using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class BeatMapManager : MonoBehaviour
{
    

    [Header("Top Row")]
    [SerializeField] private BeatRow_SO topRowBeats;
    private List<GameObject> topRowBeatObjs;

    [Header("Bot Row")]
    [SerializeField] private BeatRow_SO botRowBeats;
    private List<GameObject> botRowBeatObjs;
    
    [Header("Sprites")]
    public List<Sprite> UpSprites;
    public List<Sprite> RightSprites;
    public List<Sprite> LeftSprites;
    public List<Sprite> DownSprites;

    [Header("Settings")]
    public Transform SpawnTransform;
    [Tooltip("When the beat should be pressed for a perfect score")]
    public Transform FinalTransform;
    
    [Tooltip("Where the beat will despawn X units after finalPos")]
    public float DespawnOffset;
    // distance from Final Pos and Spawn Pos on X
    private float trackDisance = 0f;
    private float beatSpeed;
    [Tooltip("Time to spawn, seconds before TimeToHit")]
    [Min(0.000000001f)]
    public float SpawnTimeOffset;
    [SerializeField][Min(0f)] private float globalDelay = 0f;
    [SerializeField][Min(0f)] private float finishDelay = 1f;
    public GameObject baseBeatObj;
    private Vector3 spawnPos;
    private Vector3 finalPos;

    [Header("Components")]
    private AudioSource audioSource;

    [Header("Score")]
    private int totalBeats;


    [Header("Debug Toggles")]
    [Tooltip("Only works if script is in ExecuteInEditMode")]
    public bool DoOrganizeBeatMap = false;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Start() {
        // Initialize BeatObjs
        topRowBeatObjs = new();
        botRowBeatObjs = new();
        foreach (var item in topRowBeats.Beats)
        {
            topRowBeatObjs.Add(Instantiate(baseBeatObj, transform.GetChild(0)));
            topRowBeatObjs[^1].GetComponent<BeatObj>().direction = item.Dir;
            topRowBeatObjs[^1].SetActive(false);
        }
        foreach (var item in botRowBeats.Beats)
        {
            botRowBeatObjs.Add(Instantiate(baseBeatObj, transform.GetChild(1))); 
            botRowBeatObjs[^1].GetComponent<BeatObj>().direction = item.Dir;
            botRowBeatObjs[^1].SetActive(false);
        }

        // Initialize values
        spawnPos = SpawnTransform.position;
        finalPos = FinalTransform.position;
        trackDisance = finalPos.x - spawnPos.x;
        beatSpeed = trackDisance / SpawnTimeOffset;

        totalBeats = topRowBeats.Beats.Count + botRowBeats.Beats.Count;
        StartCoroutine(SpawnBeatMap());
    }

    private void Update() {
        // Debug
        if (DoOrganizeBeatMap) {
            DoOrganizeBeatMap = false;
            OrganizeBeatMap();
        }
    }

    private IEnumerator SpawnBeatMap() {
        yield return new WaitForSeconds(globalDelay);
        audioSource.Play();
        StartCoroutine(SpawnRow(topRowBeats, true));
        StartCoroutine(SpawnRow(botRowBeats, false));
    }

    private IEnumerator SpawnRow(BeatRow_SO row, bool isTop) {
        Sprite GetSprite(BeatRow_SO.Beat.Direction dir) {
            return dir switch
            {
                (BeatRow_SO.Beat.Direction.Up) => UpSprites[Random.Range(0, UpSprites.Count)],
                (BeatRow_SO.Beat.Direction.Down) => DownSprites[Random.Range(0, DownSprites.Count)],
                (BeatRow_SO.Beat.Direction.Right) => UpSprites[Random.Range(0, RightSprites.Count)],
                (BeatRow_SO.Beat.Direction.Left) => LeftSprites[Random.Range(0, LeftSprites.Count)],
                _ => UpSprites[Random.Range(0, UpSprites.Count)],
            };
        }

        int i = 0;
        float time = 0f;
        while (i < row.Beats.Count) {
            if (time >= GetBeatTimeToHit(row.Beats[i]) - SpawnTimeOffset) {
                GameObject g = (isTop) ? topRowBeatObjs[i] : botRowBeatObjs[i];
                g.GetComponent<SpriteRenderer>().sprite = GetSprite(row.Beats[i].Dir);
                g.SetActive(true);
                g.GetComponent<Rigidbody2D>().linearVelocityX = beatSpeed;
                i++;
            }
            time += Time.deltaTime;
            yield return null;
        }
    }

    // Help Functions
    private void OrganizeBeatMap() {
        int CompareBeatTime(BeatRow_SO.Beat a, BeatRow_SO.Beat b) {
            if (GetBeatTimeToHit(a) < GetBeatTimeToHit(b)) return -1;
            else if (GetBeatTimeToHit(a) > GetBeatTimeToHit(b)) return 1;
            else return 0;
        }
        topRowBeats.Beats.Sort(CompareBeatTime);
        botRowBeats.Beats.Sort(CompareBeatTime);
    }

    private float GetBeatTimeToHit(BeatRow_SO.Beat beat) {
        return beat.Min * 60 + beat.Sec;
    }

    private IEnumerator ProcessEnd() {
        yield return new WaitForSeconds(audioSource.clip.length + globalDelay + finishDelay);

    }
}
