using UnityEngine;

namespace Scenes.Skill.Scripts
{
    using System.Collections.Generic;

    [CreateAssetMenu(fileName = "filename", menuName = "KPU/스킬데이터베이스 만들기")]
    public class PlayerSkillDatabase : ScriptableObject
    {
        public List<PlayerSkill> PlayerSkills;
    }
}
