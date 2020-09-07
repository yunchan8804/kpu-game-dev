using Script.Manager;
using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class GameStatusTextUi : MonoBehaviour
    {
        public TextMeshProUGUI text;

        void Update()
        {
            text.text = GameManager.Instance.State.ToString();
        }
    }
}
