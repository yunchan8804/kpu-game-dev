using KPU;

namespace Scenes.UI
{
    using System.Collections.Generic;
    using System.Linq;

    public class UiNavigationManager : SingletonBehaviour<UiNavigationManager>
    {
        private List<NavigationalUiPanel> _managingPanels;
        private int _lastIndex;
        private NavigationalUiPanel _currentPanel;

        private void Start()
        {
            _managingPanels = GetComponentsInChildren<NavigationalUiPanel>(true).ToList();
            for (var i = 0; i < _managingPanels.Count; i++)
            {
                var panel = _managingPanels[i];
                panel.Index = i;

                if (panel.gameObject.activeInHierarchy) _currentPanel = panel;
            }
        }

        public NavigationalUiPanel Active(string panelName, bool isActive)
        {
            var foundedPanel = _managingPanels.FirstOrDefault(_ => _.PanelName == panelName);
            if (foundedPanel == null) return null;

            ReArrangeIndex(foundedPanel);

            foundedPanel.gameObject.SetActive(isActive);
            return foundedPanel;
        }

        private void ReArrangeIndex(NavigationalUiPanel panel)
        {
            // changing index
            _managingPanels.Remove(panel);
            _managingPanels.Add(panel);
            for (var i = 0; i < _managingPanels.Count; i++) _managingPanels[i].Index = i;
        }

        public void HideExceptCurrent()
        {
            _managingPanels.ForEach(panel => panel.gameObject.SetActive(false));
            ReArrangeIndex(_currentPanel);
            
            _currentPanel.gameObject.SetActive(true);
        }

        public void HideAll()
        {
            _managingPanels.ForEach(panel => panel.gameObject.SetActive(false));
        }
    }
}