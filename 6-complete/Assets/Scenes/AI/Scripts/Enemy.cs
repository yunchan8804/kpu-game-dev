namespace Scenes.AI
{
    using System;
    using KPU.Manager;
    using UnityEngine;
    using UnityEngine.AI;

    public class Enemy : MonoBehaviour, IDamagable
    {
        private NavMeshAgent _agent;
        private Player _player;
        private float _timer;
        [SerializeField] private Stat stat;

        /// <summary>
        /// Player와 얼마나 가까워야지 공격할 수 있는지 결정.
        /// </summary>
        [SerializeField] private float hitPlayerDistanceOffset = 1;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _player = FindObjectOfType<Player>();
        }

        private void OnEnable()
        {
            stat.AddHp(stat.MaxHp);
        }

        private void Update()
        {
            _agent.SetDestination(_player.transform.position);

            var dir = (_player.transform.position - transform.position);
            if (dir.magnitude <= hitPlayerDistanceOffset && _timer >= stat.AttackRate)
            {
                _player.Damage(stat.AttackPower);
                _player.GetComponent<Rigidbody>()
                    .AddForce(new Vector3(dir.normalized.x, 0, dir.normalized.z) * stat.AttackPower);
                
                var effectGameObject = ObjectPoolManager.Instance.Spawn("effect_hit");
                effectGameObject.SetActive(true);
                effectGameObject.transform.position = transform.position;
                
                _timer = 0f;
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }

        public void Damage(float damageAmount)
        {
            stat.AddHp(-damageAmount);
            if (stat.Hp <= 0) gameObject.SetActive(false);
        }
    }
}