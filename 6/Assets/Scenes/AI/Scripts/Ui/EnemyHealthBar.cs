using UnityEngine;

namespace Scenes.AI.Ui
{
    using System;
    using UnityEngine.UI;

    public class EnemyHealthBar : MonoBehaviour
    {
        private Slider _slider;
        private Enemy _enemy;
        private Camera _camera;
        private RectTransform _rectTransform;
        private Canvas _canvas;
        private Vector2 _offset;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _camera = FindObjectOfType<Camera>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Initialize(Enemy targetEnemy, Canvas targetCanvas, Vector2 offset)
        {
            _enemy = targetEnemy;
            _slider.maxValue = _enemy.Stat.MaxHp;
            _canvas = targetCanvas;
            _offset = offset;
        }

        private void Update()
        {
            if (_enemy != null)
            {
                _slider.value = _enemy.Stat.Hp;

                var viewportPoint = _camera.WorldToViewportPoint(_enemy.transform.position);
                var canvasHeight = _canvas.pixelRect.height;
                var canvasWidth = _canvas.pixelRect.width;

                _rectTransform.anchoredPosition =
                    new Vector2(canvasWidth * viewportPoint.x + _offset.x, canvasHeight * viewportPoint.y + _offset.y);
            }
        }
    }
}