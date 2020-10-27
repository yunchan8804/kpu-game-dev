namespace Scenes.Data
{
    using System;
    using UnityEngine;

    [Serializable]
    [CreateAssetMenu(fileName = "item", menuName = "KPU/create item")]
    public class ItemData : ScriptableObject
    {
        public string itemId;
        public string itemName;
        public Sprite icon;
        public string description;
        public string useEventName;
    }
}