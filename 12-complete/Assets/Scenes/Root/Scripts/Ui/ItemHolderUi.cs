namespace Scenes.Root.Ui
{
    using KPU.Manager;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class ItemHolderUi : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI itemText;

        public Sprite IconSprite
        {
            get => iconImage.sprite;
            set => iconImage.sprite = value;
        }
        public string ItemText
        {
            get => itemText.text;
            set => itemText.text = value;
        }

        public void OnClick()
        {
            EventManager.Emit("item_add");
        }
    }
}