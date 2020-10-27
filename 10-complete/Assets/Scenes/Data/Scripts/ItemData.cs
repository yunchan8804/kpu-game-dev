namespace Scenes.Data
{
    using System;
    using UnityEngine;

    [Serializable]
    public abstract class ItemData : ScriptableObject
    {
        public string itemId;
        public string itemName;
        public Sprite icon;
        public string description;
    }
}