namespace Scenes.Inventory.Item
{
    using Data;
    using UnityEngine;

    public class CrystalItemData : ItemData
    {
        public void Use()
        {
            Debug.Log("크리스털을 사용하였습니다.");
        }
    }
}