using UnityEngine;
using System;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;

    void Start()
    {
        timeInstantiated = SongManager.GetAudioSourceTime();
        transform.localPosition = new Vector3(SongManager.Instance.noteSpawnX, 0f, 0f);
    }
void Update()
{
    double currentTime = SongManager.GetAudioSourceTime();
    double timeSinceInstantiated = currentTime - timeInstantiated;
    float t = (float)(timeSinceInstantiated / SongManager.Instance.noteTime);
    Debug.Log("currentTime: " + currentTime + ", timeInstantiated: " + timeInstantiated + ", t: " + t);
    
    if (t > 1)
    {
        Destroy(gameObject);
    }
    else
    {
        Vector3 startPos = new Vector3(SongManager.Instance.noteSpawnX, 0f, 0f);
        Vector3 endPos = new Vector3(SongManager.Instance.noteDespawnX, 0f, 0f);
        transform.localPosition = Vector3.Lerp(startPos, endPos, t);
    }
}

}
