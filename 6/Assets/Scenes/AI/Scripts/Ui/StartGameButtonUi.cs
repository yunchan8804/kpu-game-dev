using UnityEngine;

namespace Scenes.AI.Ui
{
    using KPU;
    using KPU.Manager;

    public class StartGameButtonUi : MonoBehaviour
    {
        public void GameStart()
        {
            EventManager.Emit("game_started");
        }
    }
}
