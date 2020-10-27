namespace Scenes.Inventory
{
    using Data;
    using UnityEngine;

    [CreateAssetMenu(fileName = "quest_item", menuName = "KPU/퀘스트 아이템 데이터 만들기.")]
    public class QuestItem : ItemData
    {
        /// <summary>
        /// 사용 되어야 할 퀘스트의 이름.
        /// </summary>
        public string questName;
    }
}