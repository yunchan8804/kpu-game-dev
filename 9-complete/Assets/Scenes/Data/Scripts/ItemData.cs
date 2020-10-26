namespace Scenes.Data
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct ItemData
    {
        public string itemId;
        public string itemName;
        public Sprite icon;
        public string description;
    }
}