namespace Scenes.Data
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "spawn_data", menuName = "KPU/create wave data")]
    public class SpawnWaveData : ScriptableObject
    {
        [SerializeField] private List<WaveData> waveDataset;
        [SerializeField] private float waveInterval;

        public float WaveInterval => waveInterval;
        public List<WaveData> WaveDataset => waveDataset;
    }
}