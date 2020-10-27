namespace Scenes.UI
{
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
            if (_damagable == null) return;

            _slider.value = _damagable.Hp / _damagable.MaxHp;
            var width = _canvas.pixelRect.width;
            var height = _canvas.pixelRect.height;
            var viewportPoint = _camera.WorldToViewportPoint(_damagableTransform.position);
            var scale = _canvas.scaleFactor;

            _sliderRectTransform.anchoredPosition =
                new Vector2(width * viewportPoint.x / scale + uiOffset.x,
                    height * viewportPoint.y / scale + uiOffset.y);
        }
    }
}