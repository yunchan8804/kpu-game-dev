using KPU;

namespace Scenes.UI
{
    using System;
    using KPU.Manager;

    public class GameStateManager : SingletonBehaviour<GameStateManager>
    {
        public int level = 1;
        public int money;
        public float exp;
        public float expToLevelUp;
        
        private void Start()
        {
            EventManager.On("game_started", OnGameStarted);
        }

        private void OnGameStarted(object obj)
        {
            Init();
        }

        private void Update()
        {
            if (exp > expToLevelUp)
            {
                exp = 0;
                level++;
            }
        }

        private void Init()
        {
            level = 1;
            money = 0;
            exp = 0f;
            expToLevelUp = 10f;
        }
    }
}