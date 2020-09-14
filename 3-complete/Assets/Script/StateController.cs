using Script.Manager;
using UnityEngine;

namespace Script
{
    public class StateController: MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.SetState(State.Initializing);
        }
    }
}