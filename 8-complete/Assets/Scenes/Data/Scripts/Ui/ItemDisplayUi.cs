using UnityEngine;

namespace Scenes.Data.Ui
{
    using System;
    using TMPro;
    using UnityEngine.UI;

    public class ItemDisplayUi : MonoBehaviour
    {
        private ItemData _itemData;
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private Image itemIcon;

        public void SetItemData(ItemData itemData)
        {
            _itemData = itemData;
        } 

        private void Update()
        {
            itemNameText.text = _itemData.itemName;
            itemIcon.sprite = _itemData.icon;

        }
    }
}
