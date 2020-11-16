namespace Scenes.UI
{
    using System;
    using AI;
    using UnityEngine;
    using UnityEngine.UI;

    public class HealthBarUi : MonoBehaviour
    {
        private IDamagable _damagable;
        private Transform _damagableTransform;
        private Slider _slider;
        private RectTransform _sliderRectTransform;
        private Camera _camera;
        private Canvas _canvas;

        [SerializeField] private Vector2 uiOffset = new Vector2(0, 20);

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _sliderRectTransform = _slider.GetComponent<RectTransform>();
            _camera = FindObjectOfType<Camera>();
            _canvas = UiNavigationManager.Instance.GetComponent<Canvas>();
        }

        public void Initialize(IDamagable d, Transform t)
        {
            _damagable = d;
            _damagableTransform = t;
        }

        private void Update()
        {
            if (_damagable != null)
            {
                _slider.value = _damagable.Hp / _damagable.MaxHp;
                var c = _camera.WorldToScreenPoint(_damagableTransform.position);
                var r = _canvas.pixelRect;
                _sliderRectTransform.anchoredPosition = new Vector2(c.x + uiOffset.x, c.y + uiOffset.y);
            }
        }
    }
}