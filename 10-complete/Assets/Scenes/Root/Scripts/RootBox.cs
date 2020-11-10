namespace Scenes.Root
{
    using System;
    using System.Collections.Generic;
    using Data;
    using KPU.Manager;
    using UnityEngine;
    
    public class RootBox : MonoBehaviour, IInteractable
    {
        private bool _isOpened;
        public List<string> items;

        public string InteractableName => "Root Box";

        public void SwitchInteractable(bool isInteractable)
        {
            EventManager.Emit(isInteractable ? "interaction_active" : "interaction_deActive", this);
        }

        public void Interact()
        {
            EventManager.Emit("root_ui_open", this);
        }
    }
}