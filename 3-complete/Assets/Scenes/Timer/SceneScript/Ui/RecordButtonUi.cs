using Script.Manager;
using UnityEngine;

namespace Script.Time.Ui
{
    public class RecordButtonUi : MonoBehaviour
    {
        public void Click()
        {
            EventManager.Emit("timer_record", TimeManager.Instance.Time);
        }
    }
}