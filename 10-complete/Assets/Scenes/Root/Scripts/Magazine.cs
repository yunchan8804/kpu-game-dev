using UnityEngine;

namespace Scenes.Root
{
    using KPU.Manager;

    public class Magazine : MonoBehaviour, IInteractable
    {
        public string InteractableName => "Magazine";

        public void SwitchInteractable(bool isInteractable)
        {
            EventManager.Emit(isInteractable ? "interaction_active" : "interaction_deActive", this);
        }

        public void Interact()
        {
            print("Magazine added");
        }
    }
}