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
        [SerializeField] private ItemSlotUi itemSlotUiPrefab;
        [SerializeField] private ItemDatabase itemDatabase;
        [SerializeField] private Transform groupTargetTransform;

        private Dictionary<string, List<ItemData>> _items;

        private void Start()
        {
            EventManager.On("toggle_inventory", OpenInventory);
            _items = new Dictionary<string, List<ItemData>>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I)) EventManager.Emit("toggle_inventory");
        }

        private void OpenInventory(object obj)
        {
            // Toggle Ui
            inventoryCanvas.gameObject.SetActive(!inventoryCanvas.gameObject.activeInHierarchy);
        }

        public void AddItem(string itemName)
        {
            var foundedItem = itemDatabase.ItemDatas.FirstOrDefault(item => item.itemName == itemName);
            ItemSlotUi slotUi;

            if (foundedItem == null) throw new Exception("해당 아이템의 정보는 존재하지 않습니다.");

            if (!_items.ContainsKey(itemName))
            {
                _items.Add(itemName, new List<ItemData>());
                var go = Instantiate(itemSlotUiPrefab.gameObject, groupTargetTransform);
                slotUi = go.GetComponent<ItemSlotUi>();
            }
            else
            {
                var itemSlots = groupTargetTransform.GetComponentsInChildren<ItemSlotUi>();
                slotUi = itemSlots.FirstOrDefault(itemSlot => itemSlot.ItemData.itemName == itemName) ??
                         Instantiate(itemSlotUiPrefab.gameObject, groupTargetTransform).GetComponent<ItemSlotUi>();
            }

            if (slotUi == null) throw new Exception("해당 UI가 없습니다.");

            _items[itemName].Add(foundedItem);

            slotUi.ItemData = foundedItem;
            slotUi.amount = _items[itemName].Count;
        }

        public void UseItem(string itemName)
        {
            if (!_items.ContainsKey(itemName)) throw new Exception("해당 아이템은 존재하지 않습니다.");
            if (_items[itemName].Count <= 0) throw new Exception("아이템이 없습니다.");

            EventManager.Emit(_items[itemName][0].useEventName);

            _items[itemName].Remove(_items[itemName][0]);

            var itemSlots = groupTargetTransform.GetComponentsInChildren<ItemSlotUi>();
            var slotUi = itemSlots.FirstOrDefault(itemSlot => itemSlot.ItemData.itemName == itemName);
            if (!(slotUi is null)) slotUi.amount = _items[itemName].Count;
            if (slotUi.amount <= 0) Destroy(slotUi.gameObject);
        }
    }
}