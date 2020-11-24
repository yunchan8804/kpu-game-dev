namespace Scenes.Inventory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using KPU;
    using KPU.Manager;
    using Ui;
    using UnityEngine;

    public class InventoryManager : SingletonBehaviour<InventoryManager>
    {
        [SerializeField] private Canvas inventoryCanvas;
        [SerializeField] private GameObject slotUiPrefab;
        [SerializeField] private RectTransform slotUiParentTransform;
        [SerializeField] private ItemDatabase itemDatabase;

        private Dictionary<string, List<ItemData>> _items;

        public List<string> Items
        {
            get => _items
                .SelectMany(item => item.Value)
                .Select(item => item.itemName)
                .ToList();
        }

        private void Awake()
        {
            _items = new Dictionary<string, List<ItemData>>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                inventoryCanvas.gameObject.SetActive(!inventoryCanvas.gameObject.activeInHierarchy);
            }
        }

        public void AddItem(string itemName)
        {
            var founded = itemDatabase.ItemDatas.FirstOrDefault(item => item.itemName == itemName);
            if (founded == null) throw new Exception("해당 아이템은 데이터베이스에 존재하지 않습니다.");

            if (!_items.ContainsKey(itemName))
                _items.Add(itemName, new List<ItemData>());

            _items[itemName].Add(founded);

            // Slot Gameobject가 존재 하는지 확인함.
            var slotUis = slotUiParentTransform.GetComponentsInChildren<ItemSlotUi>();
            var foundedSlot = slotUis.FirstOrDefault(item => item.ItemNameText == itemName);

            ItemSlotUi itemSlot;

            if (foundedSlot == null)
            {
                var go = Instantiate(slotUiPrefab, slotUiParentTransform);
                itemSlot = go.GetComponent<ItemSlotUi>();
            }
            else
            {
                itemSlot = foundedSlot;
            }

            itemSlot.IconImage = founded.icon;
            itemSlot.ItemNameText = founded.itemName;
            itemSlot.ItemAmountText = _items[itemName].Count.ToString();
        }

        public void UseItem(string itemName)
        {
            if (!_items.ContainsKey(itemName)) throw new Exception("해당 아이템이 인벤토리에 없습니다.");

            var item = _items[itemName][0];
            if (item is UsableItem)
            {
                var usableItem = item as UsableItem;
                EventManager.Emit(usableItem.eventName);

                _items[itemName].Remove(item);
            }
            else if (item is QuestItem)
            {
            }

            var slotUis = slotUiParentTransform.GetComponentsInChildren<ItemSlotUi>();
            var foundedSlot = slotUis.FirstOrDefault(i => i.ItemNameText == itemName);
            foundedSlot.ItemAmountText = _items[itemName].Count.ToString();

            if (_items[itemName].Count <= 0)
            {
                _items.Remove(itemName);
                Destroy(foundedSlot.gameObject);
            }
        }
    }
}