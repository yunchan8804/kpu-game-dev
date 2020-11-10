using UnityEngine;

namespace Scenes.Data
{
    using System.Collections.Generic;
    using System.Linq;

    [CreateAssetMenu(fileName = "item", menuName = "KPU/create item")]
    public class ItemDatabase : ScriptableObject
    {
        public List<ItemData> ItemDatas;

        public ItemData FindItemData(string itemName)
        {
            return ItemDatas.FirstOrDefault(i => i.itemName == itemName);
        }
    }
}
