using UnityEngine;

namespace Scenes.Inventory.Ui
{
    using System;
    using Data;
    using TMPro;
    using UnityEngine.UI;

    public class ItemSlotUi : MonoBehaviour
    {
        public ItemData ItemData;
        public int amount;

        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI itemAmountText;

        public void Use()
        {
            InventoryManager.Instance.UseItem(ItemData.itemName);
        }

        private void Update()
        {
            if (ItemData == null) return;
            iconImage.sprite = ItemData.icon;
            itemAmountText.text = amount.ToString();
        }
    }
}