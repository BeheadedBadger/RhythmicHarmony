using System;
using System.Collections.Generic;
using UnityEngine;

public class BetaMapManager : MonoBehaviour
{
    [Serializable]
    public struct Row {
        public List<Beat> Beats;
        [Tooltip("Where beats will spawn")]
        public Vector3 SpawnPos;
        [Tooltip("When the beat should be clicked for a perfect score")]
        public Vector3 DinaPos;
        [Tooltip("Where the beat will despawn X units after finalPos")]
        public float DespawnOffset;
    }
    [SerializeField] private Row topRow;
    [SerializeField] private Row botRow;

}
