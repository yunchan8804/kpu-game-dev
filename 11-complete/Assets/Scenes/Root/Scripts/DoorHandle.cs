using UnityEngine;

namespace Scenes.Root
{
    using System;
    using DG.Tweening;
    using KPU.Manager;

    public class DoorHandle : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform doorBody;
        public string InteractableName => "Door";

        private Quaternion _initialRotation;
        private bool _isOpened;

        private void Awake()
        {
            _initialRotation = transform.rotation;
            _isOpened = false;
        }

        public void SwitchInteractable(bool isInteractable)
        {
            EventManager.Emit(isInteractable ? "interaction_active" : "interaction_deActive", this);
        }

        public void Interact()
        {
            _isOpened = !_isOpened;
            doorBody.DORotate(_isOpened ? Quaternion.Euler(_initialRotation.eulerAngles + new Vector3(0, -90, 0)).eulerAngles : _initialRotation.eulerAngles, 1).SetEase(Ease.InExpo);
        }
    }
}