namespace Scenes.AI
{
    using UnityEngine;

    public class Crystal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            print(other);
            gameObject.SetActive(false);
        }
    }
}