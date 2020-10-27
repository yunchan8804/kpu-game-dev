using UnityEngine;

namespace Scenes.Data
{
    using System.Collections.Generic;

    [CreateAssetMenu(fileName = "item_database", menuName = "KPU/create item database")]
    public class ItemDatabase : ScriptableObject
    {
        public List<ItemData> ItemDatas;
    }
}
