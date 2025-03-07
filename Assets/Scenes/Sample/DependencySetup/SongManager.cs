using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelayInSeconds;
    public double marginOfError;
    public int inputDelayInMilliseconds;

    public string fileLocation;
    public float noteTime;
    public float noteSpawnX;
    public float noteDespawnX;

    public static MidiFile midiFile;
    public double songStartTime;

    void Start()
    {
        Instance = this;
        if (Application.streamingAssetsPath.StartsWith("http://") ||
            Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite());
        }
        else
        {
            ReadFromFile();
        }
    }

    private IEnumerator ReadFromWebsite()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midiFile = MidiFile.Read(stream);
                    GetDataFromMidi();
                }
            }
        }
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }

    public void GetDataFromMidi()
    {
        var midiNotes = midiFile.GetNotes();
        List<Melanchall.DryWetMidi.Interaction.Note> notesList =
            new List<Melanchall.DryWetMidi.Interaction.Note>(midiNotes);
        notesList.Sort((a, b) => a.Time.CompareTo(b.Time));

        int laneCount = lanes.Length;

        foreach (var lane in lanes)
        {
            lane.ClearTimestamps();
        }

        int i = 0;
        foreach (var note in notesList)
        {
            var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, midiFile.GetTempoMap());
            double timestamp = metricTimeSpan.Minutes * 60f +
                               metricTimeSpan.Seconds +
                               metricTimeSpan.Milliseconds / 1000f;
            int laneIndex = i % laneCount;
            lanes[laneIndex].AddTimestamp(timestamp);
            i++;
        }

        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    public void StartSong()
    {
        if (noteSpawnX < noteDespawnX)
        {
            float temp = noteSpawnX;
            noteSpawnX = noteDespawnX;
            noteDespawnX = temp;
        }
        
        songStartTime = Time.time;
        audioSource.Play();
    }

    public static double GetAudioSourceTime()
    {
        if (Instance.audioSource != null && Instance.audioSource.isPlaying)
        {
            return Time.time - Instance.songStartTime;
        }
        else
        {
            return 0;
        }
    }
}
