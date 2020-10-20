namespace Scenes.AI.Ui
{
    using UnityEngine;
    using UnityEngine.UI;

    public class DefaultCanvasUi : NavigationalCanvas
    {
        [SerializeField] private Slider playerHpSlider;
        [SerializeField] private Player player;

        private void Start()
        {
            playerHpSlider.maxValue = player.Stat.MaxHp;
        }

        private void Update()
        {
            playerHpSlider.value = player.Stat.Hp;
        }

        public void GoNextCanvas()
        {
            NavigationalCanvasManager.Instance.Show("second");
        }
    }
}