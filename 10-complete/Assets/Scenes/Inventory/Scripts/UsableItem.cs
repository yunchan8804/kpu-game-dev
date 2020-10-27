using UnityEngine;

namespace Scenes.Inventory
{
    using Data;

    [CreateAssetMenu(fileName = "usable_item", menuName = "KPU/사용가능 아이템 데이터 생성하기.")]
    public class UsableItem : ItemData
    {
        /// <summary>
        /// 사용했을 때 발동되는 이벤트의 이름.
        /// </summary>
        public string eventName;
    }
}
