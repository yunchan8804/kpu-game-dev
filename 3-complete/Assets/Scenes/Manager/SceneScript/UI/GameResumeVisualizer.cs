using Script.Manager;
using UnityEngine;

namespace Script.UI
{
    public class GameResumeVisualizer : MonoBehaviour
    {
        private void Start()
        {
            EventManager.On("game_started", Show);
            EventManager.On("game_ended", Hide);
            
            gameObject.SetActive(false);
        }

        private void Hide(object obj) => gameObject.SetActive(false);
        private void Show(object obj) => gameObject.SetActive(true);
    }
}
