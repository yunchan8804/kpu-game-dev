using UnityEngine;

namespace Scenes.Skill.Scripts.Ui
{
    using System;
    using System.Linq;
    using KPU.Manager;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class SkillSlotUi : MonoBehaviour, IDropHandler
    {
        [SerializeField] private PlayerSkillDatabase skillDatabase;
        [SerializeField] private Image iconImage;
        [SerializeField] private Image skillPercentageCircleImage;
        [SerializeField] private KeyCode skillKey = KeyCode.Alpha1;

        private PlayerSkill _playerSkill;
        private string _skillName;
        private float _skillCoolTimer = 0f;

        public string SkillName => _skillName;

        public void OnDrop(PointerEventData eventData)
        {
            _skillName = eventData.pointerDrag.GetComponent<SkillIconUi>().SkillName;
            _playerSkill = skillDatabase.PlayerSkills.FirstOrDefault(_ => _.skillName == _skillName);
            iconImage.sprite = _playerSkill?.skillIconSprite;
            iconImage.color = Color.white;
        }

        private void Awake()
        {
            iconImage.color = new Color(1, 1, 1, 0);
        }

        private void Update()
        {
            if (_playerSkill == null) return;
            
            _skillCoolTimer = Mathf.Clamp(_skillCoolTimer + Time.deltaTime, 0, _playerSkill.skillCoolTime);

            if (_skillCoolTimer >= _playerSkill.skillCoolTime)
            {
                if (Input.GetKeyDown(skillKey))
                {
                    EventManager.Emit($"skill_active_{SkillName}", SkillName);
                    _skillCoolTimer = 0;
                }
            }

            skillPercentageCircleImage.fillAmount = _skillCoolTimer/_playerSkill.skillCoolTime;
        }
    }
}