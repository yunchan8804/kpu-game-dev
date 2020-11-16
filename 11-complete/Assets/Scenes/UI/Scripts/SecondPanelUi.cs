using UnityEngine;

namespace Scenes.UI
{
    public class SecondPanelUi : MonoBehaviour
    {
        public void GoToFirst()
        {
            UiNavigationManager.Instance.Active("first", true);
            UiNavigationManager.Instance.Active("second", false);
        }
    }
}
