using UnityEngine;

namespace Scenes.Inventory.Ui
{
    using TMPro;
    using UnityEngine.UI;

    public class ItemSlotUi : MonoBehaviour
    {
        /// <summary>
        /// 아이템 아이콘 이미지
        /// </summary>
        [SerializeField] private Image iconImage;

        /// <summary>
        /// 아이템 이름 텍스트
        /// </summary>
        [SerializeField] private TextMeshProUGUI itemNameText;

        /// <summary>
        /// 아이템 수량 텍스트
        /// </summary>
        [SerializeField] private TextMeshProUGUI itemAmountText;

        public Sprite IconImage
        {
            get => iconImage.sprite;
            set => iconImage.sprite = value;
        }

        public string ItemNameText
        {
            get => itemNameText.text;
            set => itemNameText.text = value;
        }

        public string ItemAmountText
        {
            get => itemAmountText.text;
            set => itemAmountText.text = value;
        }

        public void Use()
        {
            InventoryManager.Instance.UseItem(ItemNameText);
        }
    }
}