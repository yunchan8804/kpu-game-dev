using UnityEngine;

namespace Scenes.Inventory
{
    using AI;
    using KPU.Manager;

    public class ItemEventManager : MonoBehaviour
    {
        void Start()
        {
            EventManager.On("use_crystal", OnUseCrystal);
        }

        private void OnUseCrystal(object obj)
        {
            var enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.Damage(1);
            }
        }
    }
}