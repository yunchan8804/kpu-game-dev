using UnityEngine;

namespace Scenes.Root.Ui
{
    using System;
    using KPU.Manager;
    using TMPro;
    using UnityEngine.UI;

    public class CursorUi : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private RectTransform rectTransform;
        private IInteractable _interactable;
        private Image _image;
        private TextMeshProUGUI _text;

        public IInteractable Interactable
        {
            set => _interactable = value;
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Start()
        {
            EventManager.On("interaction_active", OnActive);
            EventManager.On("interaction_deActive", OnDeActive);
        }

        private void OnDeActive(object obj)
        {
            Interactable = null;
        }

        private void OnActive(object obj)
        {
            Interactable = obj as IInteractable;
        }

        private void Update()
        {
            if (_interactable != null)
            {
                var t = _interactable as Component;
                var point = camera.WorldToViewportPoint(t.transform.position);
                var pos = new Vector2(rectTransform.rect.width * point.x, rectTransform.rect.height * point.y);

                GetComponent<RectTransform>().anchoredPosition = pos;
                _image.color = Color.white;
                _text.color = Color.white;
                _text.text = _interactable.InteractableName;
            }
            else
            {
                var transparentColor = new Color(1, 1, 1, 0);
                
                _image.color = transparentColor;
                _text.color = transparentColor;
            }
        }
    }
}