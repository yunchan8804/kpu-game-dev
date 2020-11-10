using UnityEngine;

namespace Scenes.Root.Ui
{
    public class CloseButtonUi : MonoBehaviour
    {

        [SerializeField] private RectTransform rootUiBody;
        public void Close()
        {
            rootUiBody.gameObject.SetActive(false);
        }
    }
}
