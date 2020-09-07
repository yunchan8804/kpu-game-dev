using System.Globalization;
using Script.Manager;
using Script.Tower;
using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class GameStatusPanelUi : MonoBehaviour
    {
        public TowerController towerController;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI moneyText;
        public TextMeshProUGUI scoreText;

        void Start()
        {
            EventManager.On("game_started", Show);
            EventManager.On("game_resumed", Show);
            EventManager.On("game_ended", Hide);

            Hide(null);
        }

        private void Update()
        {
            healthText.text = towerController.Health.ToString(CultureInfo.InvariantCulture);
            moneyText.text = GameScoreManager.Instance.money.ToString(CultureInfo.InvariantCulture);
            scoreText.text = GameScoreManager.Instance.score.ToString(CultureInfo.InvariantCulture);
        }

        private void Hide(object obj) => gameObject.SetActive(false);
        private void Show(object obj) => gameObject.SetActive(true);
    }
}