using UnityEngine;

namespace Scenes.Skill.Scripts
{
    using System;

    [Serializable]
    public class PlayerSkill
    {
        public string skillName;
        public string skillDescription;
        public float skillCoolTime;
        public int skillUnlockLevel = 1;
        public Sprite skillIconSprite;

        public virtual void Activate()
        {
            
        }
    }
}
