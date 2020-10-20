using UnityEngine;

namespace Scenes.AI.Ui
{
    public class NavigationalCanvas : MonoBehaviour
    {
        [SerializeField] private string canvasName;

        public string CanvasName => canvasName;
    }
}
