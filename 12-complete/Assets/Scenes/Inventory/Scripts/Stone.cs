using UnityEngine;

namespace Scenes.Inventory
{
    public class Stone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.AddItem("stone");
                gameObject.SetActive(false);
            }
        }
    }
}