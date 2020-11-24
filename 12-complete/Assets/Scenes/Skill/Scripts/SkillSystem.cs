namespace Scenes.Skill.Scripts
{
    using System;
    using System.Collections.Generic;
    using AI;
    using KPU;
    using UnityEngine;

    public class SkillSystem : SingletonBehaviour<SkillSystem>
    {
        public List<PlayerSkill> playerSkill;

        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                
            }
                
        }
    }
}
