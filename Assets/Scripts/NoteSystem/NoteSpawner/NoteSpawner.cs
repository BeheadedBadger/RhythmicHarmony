using NUnit.Framework;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] System.Collections.Generic.List<GameObject> Notes;
    [SerializeField] int spawnEverySeconds;
    [SerializeField] int spawnDelaySeconds;
    [SerializeField] float noteSpeed;
    private DateTime spawnTime;
    private System.Collections.Generic.List<GameObject> instantiatedNotes;

    void Start()
    {
        spawnTime = DateTime.Now.AddSeconds(spawnDelaySeconds);
        spawnTime.AddSeconds(spawnDelaySeconds);
        instantiatedNotes = new();
    }

    void Update()
    {
        DateTime now = DateTime.Now;

        if (instantiatedNotes != null && instantiatedNotes.Count > 0)
        {
            foreach (GameObject instantiatedNote in instantiatedNotes)
            {
                if (instantiatedNote != null)
                {
                    float speed = noteSpeed * Time.deltaTime;
                    instantiatedNote.transform.position = new Vector2(instantiatedNote.transform.position.x - speed, instantiatedNote.transform.position.y);
                    if (instantiatedNote.transform.localPosition.x < -22)
                    {
                        Destroy(instantiatedNote);
                    }
                }
            }
        }

        if (now >= this.spawnTime)
        {
            this.spawnTime = this.spawnTime.AddSeconds(spawnEverySeconds);
            GameObject instantiatedNote = Instantiate(Notes[Random.Range(0, Notes.Count)], this.transform);
            instantiatedNotes.Add(instantiatedNote);
        }
    }
}
