using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapManager : MonoBehaviour
{
    [Serializable]
    struct Beat {
        [Min(0)]
        public float TimeToHit;
    }

    [Header("Top Row")]
    [SerializeField] private List<Beat> topRowBeats;
    public List<Sprite> TopSprites;
    public float TopRowSpawnY;
    private List<GameObject> topRowBeatObjs;

    [Header("Bot Row")]
    [SerializeField] private List<Beat> botRowBeats;
    public List<Sprite> BotSprites;
    public float BotRowSpawnY;
    private List<GameObject> botRowBeatObjs;
    
    
    [Header("Settings")]
    public Vector3 SpawnPos;
    [Tooltip("When the beat should be pressed for a perfect score")]
    public Vector3 FinalPos;
    
    [Tooltip("Where the beat will despawn X units after finalPos")]
    public float DespawnOffset;
    // distance from Final Pos and Spawn Pos on X
    private float trackDisance = 0f;
    private float beatSpeed;
    [Tooltip("Time to spawn, seconds before TimeToHit")]
    [Min(0.000000001f)]
    public float SpawnTimeOffset;
    [SerializeField][Min(0f)] private float globalDelay = 0f;
    public GameObject baseBeatObj;
    private AudioSource audioSource;

    [Header("Debug Toggles")]
    public bool DoOrganizeBeatMap = false;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        // Initialize BeatObjs
        OrganizeBeatMap();
        topRowBeatObjs = new();
        botRowBeatObjs = new();
        foreach (var item in topRowBeats)
        {
            topRowBeatObjs.Add(Instantiate(baseBeatObj, transform.GetChild(0))); 
            topRowBeatObjs[^1].SetActive(false);
            
        }
        foreach (var item in botRowBeats)
        {
            botRowBeatObjs.Add(Instantiate(baseBeatObj, transform.GetChild(1))); 
            botRowBeatObjs[^1].SetActive(false);
        }

        trackDisance = FinalPos.x - SpawnPos.x;
        beatSpeed = trackDisance / SpawnTimeOffset;
        StartCoroutine(SpawnBeatMap());
    }

    private void Update() {
        if (DoOrganizeBeatMap) {
            DoOrganizeBeatMap = false;
            OrganizeBeatMap();
        }
    }

    private IEnumerator SpawnBeatMap() {
        yield return new WaitForSeconds(globalDelay);
        audioSource.Play();
    }

    private IEnumerator SpawnRow(List<Beat> beats) {
        int i = 0;
        float time = 0f;
        while (i < beats.Count) {
            if (time >= beats[i].TimeToHit) {
                i++;
                SpawnBeat();
            }
            time += Time.deltaTime;
            yield return null;
        }
    }

    private void SpawnBeat() {
        
    }

    private void OrganizeBeatMap() {
        int CompareBeatTime(Beat a, Beat b) {
            if (a.TimeToHit < b.TimeToHit) return -1;
            else if (a.TimeToHit > b.TimeToHit) return 1;
            else return 0;
        }
        topRowBeats.Sort(CompareBeatTime);
        botRowBeats.Sort(CompareBeatTime);
    }
}
