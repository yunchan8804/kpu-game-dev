using UnityEngine;

namespace Scenes.Skill.Scripts.Ui
{
    using System;
    using System.Linq;
    using AI;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class SkillIconUi : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Image skillIcon;
        [SerializeField] private string skillName;
        [SerializeField] private PlayerSkillDatabase playerSkillDatabase;

        private PlayerSkill _playerSkill;

        private RectTransform _rectTransform;
        private Vector2 _initialPosition;
        private CanvasGroup _canvasGroup;
        private Player _player;

        public string SkillName => skillName;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            _player = FindObjectOfType<Player>();
            _playerSkill = playerSkillDatabase.PlayerSkills.FirstOrDefault(_ => _.skillName == skillName);
            skillIcon.sprite = _playerSkill?.skillIconSprite;
        }

        private void Update()
        {
            skillIcon.color = _playerSkill.skillUnlockLevel <= _player.Stat.Level
                ? new Color(1, 1, 1, 1)
                : new Color(1, 1, 1, 0.5f);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_playerSkill.skillUnlockLevel > _player.Stat.Level) return;

            _initialPosition = _rectTransform.anchoredPosition;
            _canvasGroup.alpha = 0.5f;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_playerSkill.skillUnlockLevel > _player.Stat.Level) return;

            _rectTransform.anchoredPosition = _initialPosition;
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_playerSkill.skillUnlockLevel > _player.Stat.Level) return;

            _rectTransform.anchoredPosition += eventData.delta;
        }
    }
}