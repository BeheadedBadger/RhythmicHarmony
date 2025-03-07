using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public GameObject notePrefab;
    private List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();

    private int spawnIndex = 0;

    public void AddTimestamp(double timestamp)
    {
        timeStamps.Add(timestamp);
    }
    
    public void ClearTimestamps()
    {
        timeStamps.Clear();
        spawnIndex = 0;
    }

    void Update()
    {
        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                var noteObj = Instantiate(notePrefab, transform);
                Note note = noteObj.GetComponent<Note>();
                note.assignedTime = (float)timeStamps[spawnIndex];
                notes.Add(note);
                spawnIndex++;
            }
        }
    }
}
