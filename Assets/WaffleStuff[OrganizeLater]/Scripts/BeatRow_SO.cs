using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeatRow", menuName = "ScriptableObjects/BeatRow", order = 1)]
public class BeatRow_SO : ScriptableObject
{
    [Serializable]
    public struct Beat {
        [Header("Time to hit")]
        [Range(0f, 3f)]
        public int Min;
        [Range(0f, 60f)]
        public int Sec;
        public enum Direction { Up, Down, Left, Right }
        public Direction Dir;
    }

    public List<Beat> Beats;
}
