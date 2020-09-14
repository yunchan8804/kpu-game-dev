using Script.Manager;
using UnityEngine;

namespace Script.Time.Ui
{
    public class ResetButtonUi : MonoBehaviour
    {
        public void Click()
        {
            EventManager.Emit("timer_reset", null);
            TimeManager.Instance.ResetTime();
        }
    }
}
