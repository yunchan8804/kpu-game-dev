namespace Scenes.Data
{
    using System;

    [Serializable]
    public struct WaveData
    {
        public string spawnTargetName;
        public int count;
        public float spawnInterval;
    }
}