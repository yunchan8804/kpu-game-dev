namespace Scenes.AI
{
    using Inventory;
    using UnityEngine;

    public class Crystal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            InventoryManager.Instance.AddItem("crystal");
            gameObject.SetActive(false);
        }
    }
}