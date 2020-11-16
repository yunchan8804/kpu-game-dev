using UnityEngine;

namespace Scenes.Root.Ui
{
    using Data;
    using KPU.Manager;
    using UnityEngine.UI;

    public class RootUi : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private GameObject itemHolderPrefab;
        [SerializeField] private ItemDatabase itemDatabase;
        private RootBox _rootBox;

        private void Start()
        {
            EventManager.On("root_ui_open", OnOpen);
            EventManager.On("root_ui_close", OnClose);
            gameObject.SetActive(false);
        }

        private void OnClose(object obj)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.SetActive(false);
        }

        private void OnOpen(object obj)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameObject.SetActive(true);
            _rootBox = (RootBox)obj;

            foreach (var item in _rootBox.items)
            {
                var foundedItemFromDb = itemDatabase.FindItemData(item);
                if (foundedItemFromDb != null)
                {
                    var go = Instantiate(itemHolderPrefab, gridLayoutGroup.transform);
                    var itemHolder = go.GetComponent<ItemHolderUi>();
                    itemHolder.ItemText = foundedItemFromDb.itemName;
                    itemHolder.IconSprite = foundedItemFromDb.icon;
                }
            }
        }
    }
}