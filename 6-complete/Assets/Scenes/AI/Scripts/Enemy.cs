namespace Scenes.AI
{
    using System;
    using System.Collections;
    using KPU.Manager;
    using UnityEngine;
    using UnityEngine.AI;

    public class Enemy : MonoBehaviour, IDamagable
    {
        private NavMeshAgent _agent;
        private Player _player;
        private float _timer;
        private EnemyState _state;
        [SerializeField] private Stat stat;
        [SerializeField] private float sightAngle = 0.2f;
        [SerializeField] private float sightLength = 2f;
        [SerializeField] private LayerMask layerToCast ;

        /// <summary>
        /// Player와 얼마나 가까워야지 공격할 수 있는지 결정.
        /// </summary>
        [SerializeField] private float hitPlayerDistanceOffset = 1;

        private Coroutine _lifeRoutine;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _player = FindObjectOfType<Player>();
        }

        private void OnEnable()
        {
            _state = EnemyState.Idle;
            stat.AddHp(stat.MaxHp);
            _lifeRoutine = StartCoroutine(LifeRoutine());
        }

        private void OnDisable()
        {
            StopCoroutine(_lifeRoutine);
            _lifeRoutine = null;
        }

        private IEnumerator LifeRoutine()
        {
            while (_state != EnemyState.Dead)
            {
                while (_state == EnemyState.Idle)
                {
                    _agent.isStopped = true;
                    _state = EnemyState.Finding;
                    yield return null;
                }

                while (_state == EnemyState.Finding)
                {
                    _agent.isStopped = false;
                    var angle = Vector3.Dot((_player.transform.position - transform.position).normalized,
                        transform.forward);

                    if (angle > sightAngle)
                    {
                        if (Physics.Raycast(transform.position,
                            (_player.transform.position - transform.position).normalized, out var racastHit,
                            sightLength, layerToCast))
                        {
                            print(racastHit.collider.tag);
                            if (racastHit.collider.CompareTag("Player"))
                            {
                                _state = EnemyState.Chasing;
                                break;
                            }
                        }
                    }

                    yield return null;
                }

                while (_state == EnemyState.Chasing)
                {
                    var position = _player.transform.position;
                    _agent.SetDestination(position);

                    var dir = (position - transform.position);
                    if (dir.magnitude <= hitPlayerDistanceOffset)
                    {
                        _state = EnemyState.Attacking;
                        break;
                    }

                    yield return null;
                }

                while (_state == EnemyState.Attacking)
                {
                    if ((_player.transform.position - transform.position).magnitude <= hitPlayerDistanceOffset)
                    {
                        if (_timer >= stat.AttackRate)
                        {
                            _player.Damage(stat.AttackPower);

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
                    else
                    {
                        _state = EnemyState.Idle;
                    }

                    yield return null;
                }

                yield return null;
            }

            // 죽음.
        }


        public void Damage(float damageAmount)
        {
            stat.AddHp(-damageAmount);
            if (stat.Hp <= 0)
            {
                _state = EnemyState.Dead;

                var deathEffectGameObject = ObjectPoolManager.Instance.Spawn("effect_enemy_death");
                deathEffectGameObject.transform.position = transform.position;
                deathEffectGameObject.SetActive(true);

                gameObject.SetActive(false);
            }
        }
    }
}