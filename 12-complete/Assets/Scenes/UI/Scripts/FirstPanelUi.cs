using UnityEngine;

namespace Scenes.UI
{
    public class FirstPanelUi : MonoBehaviour
    {
        [SerializeField] private RectTransform skillCanvasTransform;
        public void GoToSecond()
        {
            gameObject.SetActive(false);
            skillCanvasTransform.gameObject.SetActive(true);
        }

        public void HideAllMenus()
        {
            gameObject.SetActive(false);
        }
    }
}