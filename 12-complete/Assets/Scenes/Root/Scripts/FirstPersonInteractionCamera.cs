using UnityEngine;

namespace Scenes.Root
{
    using System.Collections.Generic;
    using System.Linq;
    using KPU.Manager;

    public class FirstPersonInteractionCamera : MonoBehaviour
    {
        [SerializeField] private Camera playercamera;
        [SerializeField] private float interactAngle = 0.5f;

        private MouseLookScript _mouseLookScript;
        private GunScript _gunScript; 

        /// <summary>
        /// 인터랙션이 가능한지 여부
        /// </summary>
        private bool _isInteractPossible;

        private IInteractable _possibleInteractable;

        private List<IInteractable> _interactables;

        private void Awake()
        {
            _interactables = new List<IInteractable>();
            _mouseLookScript = GetComponent<MouseLookScript>();
        }

        private void Start()
        {
            _isInteractPossible = true;
            
            EventManager.On("root_ui_open", OnOpen);
            EventManager.On("root_ui_close", OnClose);
        }

        private void OnClose(object obj)
        {
            if (_gunScript == null) _gunScript = FindObjectOfType<GunScript>();
            
            _gunScript.enabled = true;
            _mouseLookScript.enabled = true;
        }

        private void OnOpen(object obj)
        {
            if (_gunScript == null) _gunScript = FindObjectOfType<GunScript>();
            
            _gunScript.enabled = false;
            _mouseLookScript.enabled = false;
        }

        private void Update()
        {
            if (!_isInteractPossible || _interactables == null || _interactables.Count <= 0)
            {
                if (_possibleInteractable == null) return;

                _possibleInteractable.SwitchInteractable(false);
                _possibleInteractable = null;

                return;
            }

            if (_interactables.Count == 1) _possibleInteractable = _interactables[0];
            else
            {
                _possibleInteractable = _interactables.Aggregate((i1, i2) =>
                {
                    var io1 = i1 as Component;
                    var io2 = i2 as Component;
                    var tf = playercamera.transform.forward;
                    var before = Vector3.Dot(tf, (io1.transform.position - transform.position).normalized);
                    var after = Vector3.Dot(tf, (io2.transform.position - transform.position).normalized);
                    return before > after ? i1 : i2;
                });
            }

            if (Vector3.Dot(playercamera.transform.forward,
                    ((Component)_possibleInteractable).transform.position - transform.position) >
                interactAngle)
            {
                Physics.Raycast(playercamera.transform.position,
                    ((Component)_possibleInteractable).transform.position - playercamera.transform.position,
                    out var hit);

                if (hit.transform == (_possibleInteractable as Component)?.transform)
                {
                    // 찾은 것만 제외하고 다 인터렉션 표시 삭제.
                    foreach (var interactable in _interactables)
                        interactable.SwitchInteractable(false);
                    _possibleInteractable.SwitchInteractable(true);

                    if (Input.GetKeyDown(KeyCode.E)) _possibleInteractable.Interact();
                }
                else _possibleInteractable.SwitchInteractable(false);
            }
            else
            {
                _possibleInteractable.SwitchInteractable(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var interactable = other.GetComponent<IInteractable>();
            if (interactable != null) _interactables.Add(interactable);
        }

        private void OnTriggerExit(Collider other)
        {
            var interactable = other.GetComponent<IInteractable>();
            if (interactable != null && _interactables.Contains(interactable)) _interactables.Remove(interactable);
        }
    }
}