using UnityEngine;

namespace Scenes.UI
{
    [RequireComponent(typeof(Canvas))]
    public class NavigationalUiPanel : MonoBehaviour
    {
        public int Index
        {
            set
            {
                var isActive = gameObject.activeInHierarchy;
                _index = value;
                if (_canvas == null) _canvas = GetComponent<Canvas>();
                gameObject.SetActive(true);
                _canvas.overrideSorting = true;
                _canvas.sortingOrder = value;
                gameObject.SetActive(isActive);
            }
        }

        [SerializeField] private string panelName = "unnamed";

        private UiNavigationManager _manager;
        private int _index;
        private Canvas _canvas;

        public string PanelName => panelName;

        public void Init(UiNavigationManager manager)
        {
            _manager = manager;
        }
    }
}