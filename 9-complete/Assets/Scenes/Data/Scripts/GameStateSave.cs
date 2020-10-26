namespace Scenes.Data
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct GameStateSave
    {
        public Vector3 position;
        public Quaternion rotation;
    }
}