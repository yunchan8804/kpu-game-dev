namespace Scenes.UI
{
    using AI;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class DefaultPanelUi : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Slider mpSlider;
        [SerializeField] private Slider expSlider;
        [SerializeField] private TextMeshProUGUI moneyTextMesh;

        private GameStateManager _stateManager;

        private void Start()
        {
            _stateManager = GameStateManager.Instance;
        }

        public void ShowMenu()
        {
            UiNavigationManager.Instance.Active("first", true);
        }

        private void Update()
        {
            hpSlider.value = player.Hp / player.MaxHp;
            mpSlider.value = player.Mp / player.MaxMp;
            expSlider.value = float.IsNaN(_stateManager.exp / _stateManager.expToLevelUp) ? 0 : _stateManager.exp / _stateManager.expToLevelUp;
            moneyTextMesh.text = _stateManager.money.ToString();
        }
    }
}