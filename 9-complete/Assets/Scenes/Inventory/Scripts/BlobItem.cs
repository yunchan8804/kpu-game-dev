using UnityEngine;

namespace Scenes.Inventory
{
    using System;

    public class BlobItem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            InventoryManager.Instance.AddItem("blob");
            gameObject.SetActive(false);
        }
    }
}
