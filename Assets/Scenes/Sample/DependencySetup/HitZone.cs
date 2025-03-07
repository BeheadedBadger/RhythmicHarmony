using UnityEngine;
using System.Collections.Generic;

public class HitZone : MonoBehaviour
{
    public KeyCode input; // The key to register a hit (set this in the Inspector)
    private List<Note> notesInZone = new List<Note>();

    void OnTriggerEnter2D(Collider2D other)
    {
        Note note = other.GetComponent<Note>();
        if (note != null)
        {
            notesInZone.Add(note);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Note note = other.GetComponent<Note>();
        if (note != null && notesInZone.Contains(note))
        {
            notesInZone.Remove(note);
            ScoreManager.Miss();
            Debug.Log("Missed note (exited hit zone)");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(input))
        {
            if (notesInZone.Count > 0)
            {
                Note note = notesInZone[0];
                notesInZone.RemoveAt(0);
                ScoreManager.Hit();
                Debug.Log("Hit note via collision");
                Destroy(note.gameObject);
            }
        }
    }
}
