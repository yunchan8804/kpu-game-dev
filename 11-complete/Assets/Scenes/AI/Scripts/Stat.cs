namespace Scenes.AI
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct Stat
    {
        [SerializeField] private float _maxHp;
        [SerializeField] private float _maxMp;
        [SerializeField] private float _shootSpeed;
        [SerializeField] private float _attackRate;
        [SerializeField] private float _attackPower;

        private float _hp;
        private float _mp;

        public Stat(float maxHp = 10, float maxMp = 10, float shootSpeed = 40f, float attackRate = 0.1f,
            float attackPower = 0.1f)
        {
            _maxHp = maxHp;
            _hp = maxHp;
            _maxMp = maxMp;
            _mp = maxMp;
            _shootSpeed = shootSpeed;
            _attackRate = attackRate;
            _attackPower = attackPower;
        }

        public float MaxHp => _maxHp;

        public float Hp => _hp;

        public float MaxMp => _maxMp;

        public float Mp => _mp;
        
        public float ShootSpeed => _shootSpeed;

        public float AttackRate => _attackRate;

        public float AttackPower => _attackPower;

        public void AddHp(float hpAmount)
        {
            _hp = Mathf.Clamp(_hp + hpAmount, 0, _maxHp);
        }
    }
}