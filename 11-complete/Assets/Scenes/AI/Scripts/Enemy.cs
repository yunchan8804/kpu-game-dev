namespace Scenes.AI
{
    using System.Collections;
    using KPU.Manager;
    using UI;
    using UnityEngine;
    using UnityEngine.AI;

    public class Enemy : MonoBehaviour, IDamagable
    {
        private NavMeshAgent _agent;
        private Player _player;
        private HealthBarUi _healthBar;
        private Coroutine _lifeRoutine;
        private EnemyState _state;
        private float _timer;
        private Vector3 _targetPosition;

        [SerializeField] private Stat stat;
        [SerializeField] private float sightAngle = 0.2f;
        [SerializeField] private float sightLength = 2f;
        [SerializeField] private LayerMask layerToCast;
        [SerializeField] private float hitPlayerDistanceOffset = 1;

        public float Hp => stat.Hp;
        public float MaxHp => stat.MaxHp;


        #region Unity Life Cycle

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

            if (_healthBar == null)
            {
                _healthBar = ObjectPoolManager.Instance.Spawn("healthbar").GetComponent<HealthBarUi>();
                _healthBar.Initialize(this, transform);
                _healthBar.transform.parent = FindObjectOfType<Canvas>().transform;
            }
            else _healthBar.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            if (_lifeRoutine != null) StopCoroutine(_lifeRoutine);
            if (_healthBar != null) _healthBar.gameObject.SetActive(false);

            _lifeRoutine = null;
        }

        #endregion

        #region FSM

        private IEnumerator LifeRoutine()
        {
            while (_state != EnemyState.Dead)
            {
                if (_state == EnemyState.Idle) Idle();
                else if (_state == EnemyState.Finding) Find();
                else if (_state == EnemyState.Chasing) Chasing();
                else if (_state == EnemyState.Attacking) Attack();
                yield return null;
            }

            Death();
        }

        private void Death()
        {
            // effect.
            var deathEffectGameObject = ObjectPoolManager.Instance.Spawn("effect_enemy_death");
            deathEffectGameObject.transform.position = transform.position;
            deathEffectGameObject.SetActive(true);

            // money raise
            GameStateManager.Instance.money += 1;
            GameStateManager.Instance.exp += 1f;

            gameObject.SetActive(false);
        }

        private void Attack()
        {
            if ((_player.transform.position - transform.position).magnitude <= hitPlayerDistanceOffset &&
                _timer >= stat.AttackRate)
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

        private void Chasing()
        {
            var position = _player.transform.position;
            _agent.SetDestination(position);

            var dir = (position - transform.position);
            if (dir.magnitude <= hitPlayerDistanceOffset) _state = EnemyState.Attacking;
        }

        private void Find()
        {
            _agent.isStopped = false;

            print($"Distance: {Vector3.Distance(transform.position, _targetPosition)}, TargetPosition: {_targetPosition}");
            
            if (_targetPosition == Vector3.zero || Vector3.Distance(transform.position, _targetPosition) < _agent.stoppingDistance)
            {
                NavMesh.SamplePosition(new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y,
                    transform.position.z + Random.Range(-2, 2)), out var hit, 2f, 0);
                if(hit.hit)
                    _targetPosition = hit.position;
                
                _agent.SetDestination(_targetPosition);
            }

            var angle = Vector3.Dot((_player.transform.position - transform.position).normalized,
                transform.forward);

            if (angle > sightAngle && Physics.Raycast(transform.position,
                (_player.transform.position - transform.position).normalized, out var racastHit,
                sightLength, layerToCast) && racastHit.collider.CompareTag("Player"))
                _state = EnemyState.Chasing;
        }

        private void Idle()
        {
            _agent.isStopped = true;
            _state = EnemyState.Finding;
        }

        #endregion

        #region API

        public void Damage(float damageAmount)
        {
            stat.AddHp(-damageAmount);
            if (stat.Hp <= 0)
            {
                _state = EnemyState.Dead;
            }
        }

        #endregion
    }
}