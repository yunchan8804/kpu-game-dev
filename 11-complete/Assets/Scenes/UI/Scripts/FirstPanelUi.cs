using UnityEngine;

namespace Scenes.UI
{
    public class FirstPanelUi : MonoBehaviour
    {
        public void GoToSecond()
        {
            UiNavigationManager.Instance.Active("second", true);
        }

        public void HideAllMenus()
        {
            UiNavigationManager.Instance.HideAll();
            UiNavigationManager.Instance.Active("default", true);
        }
    }
}