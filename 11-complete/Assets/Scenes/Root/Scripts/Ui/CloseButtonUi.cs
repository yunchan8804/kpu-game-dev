using UnityEngine;

namespace Scenes.Root.Ui
{
    using KPU.Manager;

    public class CloseButtonUi : MonoBehaviour
    {

        [SerializeField] private RectTransform rootUiBody;
        public void Close()
        {
            rootUiBody.gameObject.SetActive(false);
            EventManager.Emit("root_ui_close");
        }
    }
}
